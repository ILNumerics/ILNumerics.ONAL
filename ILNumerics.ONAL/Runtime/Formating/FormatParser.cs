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
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {

    /// <summary>
    /// Parser for Fortran format expression strings. Use this with <see cref=" FormatItemList"/> and <see cref="FormatItem"/> to iterate the format item list in a Fortran format string.
    /// </summary>
    /// <example><![CDATA[
    /// foreach (var item in FormatParser.Parse("(1x,A, /4(2I3), 2E15 .4E2, 'H')")) {
    ///     Console.WriteLine($"FormatItem: {item}");
    ///     if (item.Reverted) break; 
    /// }
    /// Output: 
    /// FormatItem: 1X
    /// FormatItem: 1A
    /// FormatItem: 1/
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1I3
    /// FormatItem: 1E15.4E2
    /// FormatItem: 1E15.4E2
    /// FormatItem: 'H'
    /// FormatItem: 1/]]></example>
    public class FormatParser {

        public const char EOF = '\0'; 

        internal string code;
        private int m_curPos = -1;
        public FormatToken Current { get; set; }
        public FormatToken Next { get; set; }

        public FormatParser(string content) {
            code = content.Replace($"{(char)31}","") + EOF;
            Current = new FormatToken();
            Next = new FormatToken();
            Advance();
        }
        private void swap() {
            var dummy = Current;
            Current = Next;
            Next = dummy;
        }
        public FormatToken Advance() {


            // skip whitespaces - but only if we start on whitespace
            while (m_curPos < 0 || code[m_curPos] == ' ') {
                m_curPos++;
            }
            bool negFound = false;
            if (code[m_curPos] == EOF) {
                goto TokenEOF;
            }
            var start = m_curPos;
            object tok = null;

            while (code[m_curPos] != EOF) {

                switch (code[m_curPos]) {
                    case '.':
                        tok = '.';
                        m_curPos++;
                        goto TokenEnd;

                    case '\'':
                    case '"':
                        var delim = code[m_curPos];
                        nextLocation(delim, ref m_curPos);
                        if (code[m_curPos] != delim) {
                            throw new ArgumentException($"End of format within character constant.");
                        }
                        tok = code.Substring(start + 1, Math.Max(0, m_curPos - start - 1));
                        m_curPos++;
                        goto TokenEnd;

                    case '/':
                        tok = EditDescriptorManner.Slash;
                        m_curPos++;
                        goto TokenEnd;

                    case '-':
                        if (negFound) {
                            throw new ArgumentException($"Invalid '-' character at position {m_curPos} in format string: '{code}'.");
                        }
                        negFound = true;
                        m_curPos++;
                        continue;

                    case ',':
                        tok = ',';
                        m_curPos++;
                        goto TokenEnd;

                    case '(':
                    case ')':
                        // parentheses.Push(new Tuple<int, int>(m_curPos, -1));
                        tok = code[m_curPos];
                        m_curPos++;
                        goto TokenEnd;

                    case ':':
                        tok = EditDescriptorManner.Colon;
                        m_curPos++;
                        goto TokenEnd;

                    case '*':
                        tok = code[m_curPos];
                        m_curPos++;
                        goto TokenEnd;

                    case '\n':
                    case '\r':
                    case (char)31:
                        tok = EOF;
                        m_curPos++; 
                        if (code[m_curPos] == '\n') {
                            m_curPos++; 
                        }
                        goto TokenEnd; 

                    case char c when char.IsLetter(c):
                        try {
                            if (char.IsLetter(NextChar())) {
                                tok = Enum.Parse(typeof(EditDescriptorManner), $"{code[m_curPos]}{NextChar()}", ignoreCase: true);
                                m_curPos++;
                            } else {
                                tok = Enum.Parse(typeof(EditDescriptorManner), $"{code[m_curPos]}", ignoreCase: true);
                            }
                            m_curPos++;
                            goto TokenEnd;

                        } catch (ArgumentException argExc) {
                            throw new ArgumentException($"Invalid edit specifier at position '{m_curPos}' in format: ({code}).", argExc);
                        }

                    case char c when char.IsWhiteSpace(c):
                        if (start != m_curPos) {
                            goto TokenEnd;
                        }
                        m_curPos++;
                        continue;

                    case char c when char.IsDigit(c):
                        var int_literal = new List<char>() { c };
                        while (char.IsDigit(NextChar())) {
                            m_curPos++;
                            int_literal.Add(code[m_curPos]);
                        }
                        tok = int.Parse(new string(int_literal.ToArray()).Replace(" ", "")) * (negFound ? -1 : 1);
                        if (!negFound) {
                            int hPos = nextCharLow(out char nextChar);
                            if (nextChar == 'h') {
                                // hollerith (?) constant
                                var cStart = hPos + 1;
                                var cEnd = cStart + (int)tok;
                                tok = code.Substring(cStart, (int)tok);
                                m_curPos = cEnd - 1;
                            }
                        }
                        negFound = false;
                        m_curPos++;
                        goto TokenEnd;

                    default:
                        throw new ArgumentException($"Invalid character '{code[m_curPos]}' found at position {m_curPos} in format specifier: '{code}'.");
                }
            }
            void setToken(string v, int pos, object val) {

                if (negFound) { //  must have been cleared in int-literal part!    && !(val is Int32)) {
                    throw new ArgumentException($"Invalid '-' character near position {m_curPos} in format string '{code}'.");
                }

                swap();
                Next.Start = pos;
                Next.Token = v;
                Next.Value = val;
            }

            char NextChar(bool acceptWhitespace = true) {
                var a = m_curPos + 1;
                while (code[a] != EOF && (acceptWhitespace && code[a] == ' ')) a++;
                return code[a];
            }
            void nextLocation(char delimiter, ref int pos) {
                while (code[++pos] != EOF && code[pos] != delimiter) ;
                if (code[pos + 1] != EOF && code[pos + 1] == delimiter) {
                    pos++;
                    nextLocation(delimiter, ref pos);
                }
            }

        TokenEnd:
            setToken(code.Substring(start, m_curPos - start), start, tok);
            return Current;
        TokenEOF:
            setToken($"{EOF}", code.Length - 1, EOF);
            return Current;
        }

        private int nextCharLow(out char nextChar) {
            int pos = m_curPos;
            if (code[pos] != EOF)
                while (code[++pos] != EOF && code[pos] == ' ') ;
            nextChar = Char.ToLower(code[pos]);
            return pos;
        }
        public static FormatItemList Parse(string format) {
            var lexer = new FormatParser(format);
            lexer.Advance();
            return ParseItemList(lexer);
        }
        internal static FormatItemList ParseItemList(FormatParser lexer, int repeat = 1) {

            // special case: *
            if (lexer.Current.Token == "*" && object.Equals(lexer.Next.Value, EOF)) {
                return new FormatItemList(new List<FormatItem>() { new DataEditDescriptor(1, EditDescriptorManner.Asterisc, 0, 0, 0, 0) }); 
            }

            if (lexer.Current.Token != "(") {
                throw new ArgumentException($"Invalid format syntax: format string must beging with '('. Found: '{lexer.Current.Token}'.");
            }

            Stack<FormatItemList> levels = new Stack<FormatItemList>(); 

            while (true) { // lexer.Current.Token != $"{EOF}") {

                switch (lexer.Current.Value) {

                    case EOF:
                        throw new ArgumentException($"Unexpected end of format string. Missing ')' in format: '{lexer.code}'.");

                    case int rep:

                        if (object.Equals(lexer.Next.Value, '(')) {
                            lexer.Advance();
                            levels.Push(new FormatItemList(null, rep));
                            lexer.Advance(); 

                        } else if (lexer.Next.Value is EditDescriptorManner) {
                            lexer.Advance();
                            switch (lexer.Current.Value) {

                                case EditDescriptorManner.I:
                                case EditDescriptorManner.B:
                                case EditDescriptorManner.O:
                                case EditDescriptorManner.Z:
                                case EditDescriptorManner.F:
                                case EditDescriptorManner.E:
                                case EditDescriptorManner.EN:
                                case EditDescriptorManner.ES:
                                case EditDescriptorManner.G:
                                case EditDescriptorManner.L:
                                case EditDescriptorManner.A:
                                case EditDescriptorManner.D:
                                    levels.Peek().List.Add(parseDataEditDescriptor(lexer, rep));
                                    break;

                                case EditDescriptorManner.Slash:
                                    levels.Peek().List.Add(new ControlEditDescriptor(EditDescriptorManner.Slash, repeat: rep));
                                    lexer.Advance();
                                    break;

                                case EditDescriptorManner.P:
                                    levels.Peek().List.Add(new ControlEditDescriptor(EditDescriptorManner.P, k: rep));
                                    lexer.Advance();
                                    break;

                                case EditDescriptorManner.X:
                                    levels.Peek().List.Add(new PositionEditDescriptor(EditDescriptorManner.X, rep));
                                    lexer.Advance();
                                    break;

                                case EditDescriptorManner edm:
                                    throw new ArgumentException($"Invalid integer literal before: '{edm}' at position {lexer.Current.Start} in format string: '{lexer.code}'.");

                            }
                        } else {
                            throw new ArgumentException($"Invalid format after '{lexer.Current.Value}': {lexer.Next.Value} is not recognized. Position {lexer.Current.Start} in format: '{lexer.code}'.");
                        }
                        break;

                    case string s:
                        levels.Peek().List.Add(new CharacterEditDescriptor(s));
                        lexer.Advance();
                        break;

                    case EditDescriptorManner edm:

                        switch (edm) {
                            case EditDescriptorManner.I:
                            case EditDescriptorManner.B:
                            case EditDescriptorManner.O:
                            case EditDescriptorManner.Z:
                            case EditDescriptorManner.F:
                            case EditDescriptorManner.E:
                            case EditDescriptorManner.EN:
                            case EditDescriptorManner.ES:
                            case EditDescriptorManner.G:
                            case EditDescriptorManner.L:
                            case EditDescriptorManner.A:
                            case EditDescriptorManner.D:
                                levels.Peek().List.Add(parseDataEditDescriptor(lexer));
                                break;
                            case EditDescriptorManner.T:
                            case EditDescriptorManner.TL:
                            case EditDescriptorManner.TR:
                                levels.Peek().List.Add(new PositionEditDescriptor(edm, (int)lexer.Current.Value));
                                lexer.Advance();
                                break;

                            case EditDescriptorManner.Slash:
                            case EditDescriptorManner.Colon:
                                levels.Peek().List.Add(new ControlEditDescriptor(edm));
                                lexer.Advance();
                                break;

                            case EditDescriptorManner.S:
                            case EditDescriptorManner.SP:
                            case EditDescriptorManner.SS:
                                // sign-edit-desc
                                levels.Peek().List.Add(new ControlEditDescriptor(edm));
                                lexer.Advance();
                                break;

                            case EditDescriptorManner.BN:
                            case EditDescriptorManner.BZ:
                                // blank-interp-edit-desc
                                levels.Peek().List.Add(new ControlEditDescriptor(edm));
                                lexer.Advance();
                                break;

                            default:
                                throw new ArgumentException($"Invalid format '{edm}' at position {lexer.Current.Start} in format '{lexer.code}'.");
                        }
                        break;

                    case '(':
                        lexer.Advance();
                        levels.Push(new FormatItemList(null, 1)); 
                        break; 

                    case ')':
                        lexer.Advance();
                        if (levels.Count > 1) {
                            var last = levels.Pop();
                            levels.Peek().List.Add(last);
                        } else if (levels.Count == 0) {
                            // not sure if this is possible at all?!?
                            throw new ArgumentException($"Invalid format: unexpected ')' without matching '(' at position {lexer.Current.Start} in format '{lexer.code}'.");
                        } else {
                            // main exit
                            var ret = levels.Pop();
                            // ensure integrity: wire-up all items 
                            ret.Parent = ret;
                            return ret;
                        }
                        break; 

                    default:
                        throw new ArgumentException($"Unrecognized format: '{lexer.Current.Value}' at position {lexer.Current.Start} in format string: '{lexer.code}'.");
                }

                // handle delimiter
                /* 
                 * Constraint: The comma used to separate format-items in a format-item-list may be omitted as follows:
                (1) Between a P edit descriptor and an immediately following F, E, EN, ES, D, or G edit descriptor
                (10.6.5)
                (2) Before a slash edit descriptor when the optional repeat specification is not present (10.6.2)
                (3) After a slash edit descriptor
                (4) Before or after a colon edit descriptor (10.6.3)
                */
                if (object.Equals(lexer.Current.Value, ',')) {
                    lexer.Advance(); 
                }
            }
        }

        // lexer is expected to stay on the _leading_ manner specifier 'I', 'E', 'ES', ... 
        private static FormatItem parseDataEditDescriptor(FormatParser lexer, int rep = 1) {

            EditDescriptorManner manner = (EditDescriptorManner)lexer.Current.Value;
            int? w = null;
            int? d = null, e = null, m = null;

            switch (manner) {
                case EditDescriptorManner.I:
                case EditDescriptorManner.B:
                case EditDescriptorManner.O:
                case EditDescriptorManner.Z:
                    // Z w [ . m ]
                    w = (int)lexer.Advance().Value;
                    lexer.Advance(); // .
                    if ((char)lexer.Current.Value == '.') {
                        m = (int)lexer.Advance().Value;
                        lexer.Advance();
                    }
                    break;

                case EditDescriptorManner.D:
                case EditDescriptorManner.F:
                    // F w . d
                    w = (int)lexer.Advance().Value;
                    lexer.Advance(); // .
                    d = (int)lexer.Advance().Value;
                    lexer.Advance();
                    break;

                case EditDescriptorManner.E:
                case EditDescriptorManner.EN:
                case EditDescriptorManner.ES:
                case EditDescriptorManner.G:
                    // G w . d [ E e ]
                    w = (int)lexer.Advance().Value;
                    lexer.Advance(); // .
                    d = (int)lexer.Advance().Value;
                    if (lexer.Advance().Value.Equals(EditDescriptorManner.E)) {
                        e = (int)lexer.Advance().Value;
                        lexer.Advance();
                    }
                    break;

                case EditDescriptorManner.L:
                    // L w
                    w = (int)lexer.Advance().Value;
                    lexer.Advance(); // .
                    break;

                case EditDescriptorManner.A:
                    lexer.Advance();
                    if (lexer.Current.Value is Int32) {
                        w = (int)lexer.Current.Value;
                        lexer.Advance(); // .
                    } else {
                        w = null;
                    }
                    break;
            }
            if (e < 0 || (w < 0 && manner != EditDescriptorManner.A)) {
                throw new ArgumentException($"Invalid length specified for edit descriptor format. Length (e and w) must be positive, except for manner: 'A'. Spec: F90, 10.2.1.");
            }
            return new DataEditDescriptor(rep, manner, w, d, e, m);
        }
    }
}
