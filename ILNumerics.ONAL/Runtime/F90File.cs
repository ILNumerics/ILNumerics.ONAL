// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#pragma warning disable CS1591

namespace ILNumerics.Core.Runtime {
    /// <summary>
    /// Stream reader for list directed input tokens / values from FORTRAN data files. Return r, *, c, valid value separators and end marks as strings / null. See FORTRAN 90 spec: 10.8.
    /// </summary>
    public class F90File : Stream, IEnumerable<object> {

        private const string REGEXP_F_Format = @"^([+-]?((?<f>\d+\.\d*)|(?<f>\d*\.\d+)|(?<f>[0-9]+)))(?<e>([+-]\d+)|([EeDd][+-]?\d+))?$";
        private const string REGEXP_F_Fixup  = @"(?<base>[+-]?(\d*\.?\d*)|(\d+))(?<marker>[a-zA-Z]*)(?<exponent>[\+-]?\d+)?";

        public enum AdvanceMode {
            Formatted,
            ListDirected,
            Undefined
        }

        public enum Kind {
            Separator,
            IntNumber,
            RealNumber, 
            Logical,
            Character,
            Asterisc, 
            Slash,
            EndOfFile,
            EndOfLine,
            Null,
            Parenthesis
        }

        public class Token {
            public object Value { get; set; } = "";
            public string Text { get; set; } = "";
            public Kind Kind { get; set; } = Kind.Null;
            public long Position { get; set; } = 0; 
            public void Clear() {
                Value = null;
                Text = "";
                Kind = Kind.Character;
                Position = -1; 
            }
        }
        public struct BufferByte {
            public char Value;
            public long Position;
            public BufferByte(char value, long position) { this.Value = value; this.Position = position; }
        }

        public AdvanceMode CurrentAdvanceMode {
            get {
                return m_currentAdvanceMode; 
            }
            private set {
                if (value != m_currentAdvanceMode) {
                    switch (value) {
                        case AdvanceMode.ListDirected:
                            SkipToNextRecord(start: Current.Position);
                            break;
                        case AdvanceMode.Formatted:
                            SkipToNextRecord(start: Current.Position);
                            break;
                        default: 
                            break; 
                    }
                    m_currentAdvanceMode = value; 
                }
            }
        }

        BufferedStream Stream { get; set; }  
        public long OrigPosition { get; set; }
        public Token Current { get; set; } = new Token(); 
        public Token Next { get; set; } = new Token();

        private BufferByte? _nextCharRead;
        private List<BufferByte> _collect = new List<BufferByte>();
        private AdvanceMode m_currentAdvanceMode = AdvanceMode.Undefined;

        BufferByte? NextCharRead {
            get {
                if (_nextCharRead.HasValue) {
                    var ret = _nextCharRead.GetValueOrDefault();
                    _nextCharRead = null;
                    return ret; 
                } else {
                    return null; 
                }
            }
        }

        public override bool CanRead => Stream.CanRead;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanWrite => Stream.CanWrite;

        public override long Length => Stream.Length;

        public override long Position { get => Stream.Position; set => Stream.Position = value; }

        public F90File(Stream inputStream) {
            if (inputStream is BufferedStream) {
                Stream = inputStream as BufferedStream;
            } else {
                Stream = new BufferedStream(inputStream);
            }
            //if (inputStream.CanSeek == false) {
            //    throw new NotSupportedException($"List directed input requires a seekable stream. Consider converting to MemoryStream.");
            //}
        }
        ~F90File() {
            
            Stream?.Close(); 
        }

        private void swap() {
            var dummy = Current;
            Current = Next;
            Next = dummy; 
        }
        private unsafe BufferByte readNextASCII() {

            // not yet prepared for non-ascii input
            long pos = this.Stream.Position; 
            byte val = (byte)this.Stream.ReadByte();
            if (val == 255) return new BufferByte('\0', pos);   // -1
            var ret = Encoding.ASCII.GetString(&val, 1); 
            return new BufferByte(ret[0], pos); 
        }
        
        /// <summary>
        ///  Reads w characters from the current stream position, but stops at record boundary. Use for formatted input.
        /// </summary>
        /// <param name="w">Number of characters to read.</param>
        /// <returns>String with w characters read (or less if EOF or EOR occured).</returns>
        public string ReadRaw(long w) {
            if (CurrentAdvanceMode != AdvanceMode.Formatted) {
                // skips to begin of next record: 
                CurrentAdvanceMode = AdvanceMode.Formatted;
            }
            if (w <= 0) return "";
            BufferByte ccur = default; 
            long pos = -1;
            var sb = new StringBuilder(); 
            for (long i = 0; i < w; i++) {
                ccur = readNextASCII();
                if (i == 0) {
                    pos = ccur.Position;  
                }
                if (ccur.Value == '\0') {
                    throw new FEndOfFileException(-1); 
                } else if (ccur.Value == '\r' || ccur.Value == '\n') {
                    break; 
                } 
                sb.Append(ccur.Value);
            }
            Current.Kind = Kind.Character;
            Current.Position = pos;
            Current.Text = sb.ToString();
            Current.Value = Current.Text; 
            return Current.Text; 
        }

        /// <summary>
        /// Positions the stream to point to the character right after the current record.
        /// </summary>
        /// <param name="start">[Optional] Current position in the record. Default: end of current token + 1.</param>
        public void SkipToNextRecord(long start = -1) {
            if (start < 0) {
                if (Current.Value != null && Current.Value.ToString().Length > 0)
                    start = Math.Max(Current.Position + Current.Value.ToString().Length, 0);
                else
                    start = Stream.Position; // TODO ?! 
            }
            Stream.Seek(start, SeekOrigin.Begin);
            // detect if we are already directly after line break or start of file -> done. 
            if (Stream.Position == 0) {
                goto MarkEnd;
            } else {
                Stream.Seek(-1, SeekOrigin.Current);
                var c0 = readNextASCII();
                if (c0.Value == '\r' || c0.Value == '\n' || c0.Value == '\0') {
                    goto MarkEnd;
                }
            }

            var c = readNextASCII(); 
            while (c.Value != '\r' && c.Value != '\n' && c.Value != '\0') {
                // no FEndOfFileException here?!? Only when reading OVER EOF. 
                c = readNextASCII();
            }
            if (c.Value == '\r' && readNextASCII().Value != '\n') {
                Stream.Seek(c.Position + 1, SeekOrigin.Begin); 
            }
        MarkEnd: 
            _nextCharRead = null;
            Current.Clear();
            Next.Clear();
            // Stream is now positioned right after the next EOL (\n | \r[\n]).
        }

        /// <summary>
        /// Read the next token in list directed input mode.
        /// </summary>
        /// <returns></returns>
        public string ReadToken() {

            if (CurrentAdvanceMode == AdvanceMode.Formatted) {
                CurrentAdvanceMode = AdvanceMode.ListDirected; // skips to next record
                return ReadToken();
            }

            // if this is called after repositioning commands, Current and Next buffers are still empty. 
            if (Next.Position < 0) {
                Next.Position = Stream.Position; 
                ReadToken(); 
            }

            /* potential values in list directed input: 
             * 1) c - a character or numeric constant. 
             *        A character constant is either ' or " delimited and may span multiple records (without breaking within escaped "" or ''), OR 
             *        does not contain ' ', ',' or '/' and is delimited within the same record by either one of those separators. Latter must not start with r* nor ' nor ". 
             *        A numeric constant is in the F editing format (10.5.1.2.1): [digits|0][.]digits[[ED+-]digits] (<- likely non exact) 
             * 2) r*c With r being an unsigned integer - the repeat factor for c. 
             * 3) r*  Repeating null value r times. 
             * 
             * This function separately returns r, * or c. 
             * Note that the end of a record has the effect of a blank, except when it appears within a character constant. (see: F90 spec, 10.8)
            */

            _collect.Clear();
            Kind? kind = null; 
            
            var ccur = NextCharRead ?? readNextASCII();
            long pos = ccur.Position;

            if (ccur.Value == '\0') {
                kind = Kind.EndOfFile;
                goto TokenEnd; 
            } else if (ccur.Value == ' ' && (Next.Kind == Kind.Asterisc)) {
                // insert NULL value  
                _nextCharRead = ccur;
                kind = Kind.Null;
                goto TokenEnd;
            }

            while (ccur.Value == ' ') {
                ccur = readNextASCII(); 
            }
            pos = ccur.Position; 
            while (true) {
                switch (ccur.Value) {
                    case ',':
                    case '/':
                    case ';':
                    case '\\':
                    case ' ':
                    case '\0':  // EOF mark (not NULL value!)
                        if (_collect.Count == 0) {
                            // detects if NULL is required. 'Next' means: last!
                            if (ccur.Value != '\0') {
                                if (Next.Kind == Kind.Separator || Next.Kind == Kind.Asterisc || Next.Kind == Kind.EndOfLine) {
                                    _nextCharRead = ccur;
                                    kind = Kind.Null;
                                    goto TokenEnd;
                                }
                                _collect.Add(ccur);
                            }
                            kind = ccur.Value == '\0' ? Kind.EndOfFile : (ccur.Value == '/' || ccur.Value == '\\' ? Kind.Slash : Kind.Separator);
                        } else {
                            _nextCharRead = ccur; 
                        }
                        goto TokenEnd;
                    case '\r':
                    case '\n':
                        if (_collect.Count == 0) {
                            if (ccur.Value == '\r') {
                                ccur = readNextASCII();
                                if (ccur.Value != '\n') {
                                    _nextCharRead = ccur; 
                                }
                                // windows line break -> skip \r
                            }
                            _collect.Add(new BufferByte('\n', ccur.Position));
                            kind = Kind.EndOfLine;
                        } else {
                            _nextCharRead = ccur; 
                        }
                        goto TokenEnd;

                    case '(':
                    case ')': 
                        if (_collect.Count == 0) {
                            _collect.Add(ccur);
                            kind = Kind.Parenthesis;
                        } else {
                            _nextCharRead = ccur; 
                        }
                        goto TokenEnd; 

                    case '"':
                    case '\'':
                        // begin delimited character constant. store into _current and proceed (do not yet return)
                        void nextLocation(char v) {
                            do {
                                do {
                                    _collect.Add(ccur);
                                    ccur = readNextASCII();
                                    if (ccur.Value == '\0') throw new EndOfStreamException($"End of file within character constant.");
                                } while (ccur.Value != v);
                                _collect.Add(ccur);
                                ccur = readNextASCII();
                            } while (ccur.Value == v);
                        }
                        kind = Kind.Character;
                        if (_collect.Count == 0) { 
                            nextLocation(ccur.Value);
                            // now on next char _after_ constant
                            _nextCharRead = ccur;
                            goto TokenEnd; 
                        }
                        _collect.Add(ccur);
                        break;
                    case Char c when char.IsDigit(c):
                    case '.':
                    case 'E':
                    case 'e':
                    case 'D':
                    case 'd':
                    case '+':
                    case '-':
                        // a number (F format) can be: 
                        // [+-]?[1+ digits, may contain at most 1 .][exp]  
                        // where exp is:
                        // * [+-][int]
                        // * [EeDd][+-]?[int]
                        if (_collect.Count == 0) {
                            _collect.Add(ccur);
                            kind = Kind.IntNumber;  //temporary

                            if (ccur.Value == '.') {
                                // test for logical
                                var next = readNextASCII();
                                if (char.ToLower(next.Value) == 'f' || char.ToLower(next.Value) == 't') {
                                    kind = Kind.Logical;
                                    _collect.Add(next);
                                } else {
                                    ccur = next;
                                    continue; 
                                }
                            }
                        } else {
                            _collect.Add(ccur); 
                        }
                        break;

                    case '*':
                        // repeat factor 
                        if (_collect.Count == 0) {
                            kind = Kind.Asterisc;
                            _collect.Add(ccur);
                        } else {
                            // 52*  But check against -52*, which is a non-delimited character string!
                            if (_collect.TrueForAll(c => char.IsDigit(c.Value))) {
                                _nextCharRead = ccur; 
                            } else {
                                kind = Kind.Character;
                                _collect.Add(ccur);
                                break; 
                            }
                        }
                        goto TokenEnd;

                    default:
                        // within non-delimited character constant or subsequent letters for logical constant
                        if (!kind.HasValue) {
                            kind = Kind.Character;
                        }
                        _collect.Add(ccur);
                        break; 
                }
                ccur = readNextASCII();
            }

        TokenEnd:
            swap();
            if (Current.Kind == Kind.EndOfFile) {
                throw new FEndOfFileException(-1); 
            }
            var val = new string(_collect.Select(c => c.Value).ToArray());
            if (kind == Kind.IntNumber) {
                var match = Regex.Match(val, @"^([\+-]?\d+)$");
                if (match.Success) {
                    Next.Kind = Kind.IntNumber; // redundant 
                    Next.Text = val;
                    Next.Value = long.Parse(val); 
                } else {
                    match = Regex.Match(val, REGEXP_F_Format);
                    if (match.Success) {
                        Next.Kind = Kind.RealNumber;
                        // make sure the value can be parsed by double.Parse(): 
                        match = Regex.Match(val, REGEXP_F_Fixup);
                        var parsable = $"{match.Groups["base"].Value}"; 
                        if (!string.IsNullOrEmpty(match.Groups["exponent"].Value)) {
                            parsable += $"E{match.Groups["exponent"].Value}"; 
                        }
                        Next.Text = val;
                        Next.Value = double.Parse(parsable); 
                    } else {
                        Next.Kind = Kind.Character;
                        Next.Text = val;
                        Next.Value = val; 
                    }
                } 
            } else {
                Next.Kind = kind.GetValueOrDefault(); 
                Next.Text = val;
                Next.Value = val; 
            }
            Next.Position = pos; 
            return Current.Text; 
        }

        public override void Write(byte[] array, int offset, int count) {
            Stream.Write(array, offset, count); 
            Stream.Flush(); 
        }
        public void Write(string val) {
            var bytes = Encoding.ASCII.GetBytes(val);
            Write(bytes, 0, bytes.Length);
        }
        public override void Close() {
            Stream.Close(); 
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator(); 
        }
        /// <summary>
        /// Create an enumerator iterating over the values within the file. Implements special handling required for FORTRAN list directed input.
        /// </summary>
        /// <returns>Values enumerator.</returns>
        public IEnumerator<object> GetEnumerator() {

            object ret = null;
            // on entry, list directed input skips the current record and advances to the beginning of the next record. 
            CurrentAdvanceMode = AdvanceMode.Formatted;  // triggers record skip to next

            object readValue() {
                switch (Current.Kind) {
                    case Kind.IntNumber:
                        return long.Parse(Current.Text); 

                    case Kind.RealNumber:
                        return (double)Current.Value; 
                        
                    case Kind.Logical: {
                            var val = false;
                            if (Current.Text.ToLower().StartsWith(".f") || Current.Text.ToLower().StartsWith("f")) {
                                val = false;
                            } else if (Current.Text.ToLower().StartsWith(".t") || Current.Text.ToLower().StartsWith("t")) {
                                val = true;
                            } else {
                                throw new ArgumentException($"Invalid data for logical value: '{Current.Text}'.");
                            }
                            return val; 
                        }
                    case Kind.Character:
                        return Current.Text; 
                        
                    case Kind.Null:
                        return null; 

                    default:
                        throw new ArgumentException($"Unknown data found: '{Current.Text}'."); 
                        
                }
            }

            while (true) {
                ReadToken(); 
                switch (Current.Kind) {
                    case Kind.Separator:
                        break;

                    case Kind.IntNumber: {
                            var val = int.Parse(Current.Text);
                            if (Next.Kind == Kind.Asterisc && val > 0) {
                                // r* form 
                                ReadToken(); ReadToken();
                                ret = readValue();
                                for (int i = 0; i < val; i++) {
                                    yield return ret; 
                                }
                            } else {
                                yield return val; 
                            }
                        }
                        break;

                    case Kind.RealNumber:
                    case Kind.Logical:
                    case Kind.Character:
                        ret = readValue();
                        yield return ret; 
                        break;

                    case Kind.Parenthesis: 
                        if (Current.Text != "(") {
                            throw new FormatException($"Unmatched ). Expected: '('."); 
                        }
                        ReadToken(); 
                        if (Current.Kind != Kind.IntNumber && Current.Kind != Kind.RealNumber) {
                            throw new FormatException($"Invalid data in complex number defintion (real part). Expected: real number. Found: '{Current.Text}'."); 
                        }
                        double real = (double)readValue();
                        ReadToken();
                        if (Current.Text != ",") throw new FormatException($"Missing ',' separator between real and imaginary part of complex data.");
                        ReadToken();
                        if (Current.Kind != Kind.IntNumber && Current.Kind != Kind.RealNumber) {
                            throw new FormatException($"Invalid data in complex number defintion (imaginary part). Expected: real number. Found: '{Current.Text}'.");
                        }
                        double imag = (double)readValue();
                        ReadToken();
                        if (Current.Text != ")") throw new FormatException($"Missing ) after complex data definition.");
                        yield return new complex(real, imag);
                        break;

                    case Kind.Asterisc:
                        throw new InvalidProgramException($"Unexpected * within data stream."); 

                    case Kind.Slash:
                        // return null for ever (we don't know how many objects are still in the queue!) 
                        while (true) {
                            yield return null;
                        }

                    case Kind.EndOfLine:
                        break; 
                    case Kind.EndOfFile:
                        yield break; 

                    case Kind.Null:
                        yield return null;
                        break; 

                    default:
                        break;
                }
            }
        }

        public override void Flush() {
            //throw new NotImplementedException();
            Stream?.Flush(); 
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

    }
}
