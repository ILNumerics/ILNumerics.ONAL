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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

/*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="after">
            inCls1
        </source>
        <destination><![CDATA[Storage<sbyte>]]></destination>
        <destination><![CDATA[Storage<Int64>]]></destination>
        <destination><![CDATA[Storage<UInt64>]]></destination>
        <destination><![CDATA[Storage<byte>]]></destination>
        <destination><![CDATA[Storage<char>]]></destination>
        <destination><![CDATA[Storage<float>]]></destination>
        <destination><![CDATA[Storage<Int16>]]></destination>
        <destination><![CDATA[Storage<Int32>]]></destination>
        <destination><![CDATA[Storage<UInt16>]]></destination>
        <destination><![CDATA[Storage<uint>]]></destination>
        <destination><![CDATA[Storage<complex>]]></destination>
        <destination><![CDATA[Storage<fcomplex>]]></destination>
    </type>
    <type>
        <source locate="after">
            inCls2
        </source>
        <destination><![CDATA[Storage<sbyte>]]></destination>
        <destination><![CDATA[Storage<Int64>]]></destination>
        <destination><![CDATA[Storage<UInt64>]]></destination>
        <destination><![CDATA[Storage<byte>]]></destination>
        <destination><![CDATA[Storage<char>]]></destination>
        <destination><![CDATA[Storage<float>]]></destination>
        <destination><![CDATA[Storage<Int16>]]></destination>
        <destination><![CDATA[Storage<Int32>]]></destination>
        <destination><![CDATA[Storage<UInt16>]]></destination>
        <destination><![CDATA[Storage<uint>]]></destination>
        <destination><![CDATA[Storage<complex>]]></destination>
        <destination><![CDATA[Storage<fcomplex>]]></destination>
    </type>
    <type>
        <source locate="after">
            inArr1
        </source>
        <destination>sbyte</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>byte</destination>
        <destination>char</destination>
        <destination>float</destination>
        <destination>Int16</destination>
        <destination>Int32</destination>
        <destination>UInt16</destination>
        <destination>UInt32</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after">
            inArr2
        </source>
        <destination>sbyte</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>byte</destination>
        <destination>char</destination>
        <destination>float</destination>
        <destination>Int16</destination>
        <destination>Int32</destination>
        <destination>UInt16</destination>
        <destination>UInt32</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after">
            outCls1
        </source>
        <destination><![CDATA[Array<sbyte>]]></destination>
        <destination><![CDATA[Array<Int64>]]></destination>
        <destination><![CDATA[Array<UInt64>]]></destination>
        <destination><![CDATA[Array<byte>]]></destination>
        <destination><![CDATA[Array<char>]]></destination>
        <destination><![CDATA[Array<float>]]></destination>
        <destination><![CDATA[Array<Int16>]]></destination>
        <destination><![CDATA[Array<Int32>]]></destination>
        <destination><![CDATA[Array<UInt16>]]></destination>
        <destination><![CDATA[Array<UInt32>]]></destination>
        <destination><![CDATA[Array<complex>]]></destination>
        <destination><![CDATA[Array<fcomplex>]]></destination>
    </type>
    <type>
        <source locate="after">
            outArr1
        </source>
        <destination>sbyte</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>byte</destination>
        <destination>char</destination>
        <destination>float</destination>
        <destination>Int16</destination>
        <destination>Int32</destination>
        <destination>UInt16</destination>
        <destination>UInt32</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after">
            HCGetElemCmplx
        </source>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination>.real</destination>
        <destination>.real</destination>
    </type>
    <type>
        <source locate="after">
            HCMatFileType
        </source>
        <destination>MatFileType.miINT8</destination>
        <destination>MatFileType.miINT64</destination>
        <destination>MatFileType.miUINT64</destination>
        <destination>MatFileType.miUINT8</destination>
        <destination>MatFileType.miUINT16</destination>
        <destination>MatFileType.miSINGLE</destination>
        <destination>MatFileType.miINT16</destination>
        <destination>MatFileType.miINT32</destination>
        <destination>MatFileType.miUINT16</destination>
        <destination>MatFileType.miUINT32</destination>
        <destination>MatFileType.miDOUBLE</destination>
        <destination>MatFileType.miSINGLE</destination>
    </type>
    <type>
        <source locate="after">
            outCast
        </source>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination>(UInt16)</destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
    </type>
 </hycalper>
 */

namespace ILNumerics {

    /// <summary>
    /// Matlab(R) .mat file class for reading from / writing to *.mat files.
    /// </summary>
    /// <remarks>This class reads and writes Matlab .mat files version 6.
    /// All numeric array types are supported. The reading and writing of 
    /// Matlab cell arrays is not supported.</remarks>
    /// <example><code><![CDATA[
    /// // MatFile should be used in an 'using' block, 
    /// // cleaning up its resources automatically.
    /// using (MatFile mat = new MatFile()) {
    ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
    ///     mat.Write("file.mat");
    /// }
    /// 
    /// // reading back using ILMath.loadArray<T>(...)
    /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
    /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
    /// 
    /// // reading back using MatFile
    /// using (var back = new MatFile("file.mat")) {
    ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
    /// 
    ///     // ... or usign cell methods: 
    ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
    /// 
    ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
    ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
    /// }
    /// ]]></code></example>
    /// <seealso cref="Cell"/>
    public sealed class MatFile : IDisposable {

        #region attributes 
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string m_filename = "(unknown)"; 
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Cell m_data = cell();
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string headerFromFile;
        #endregion
                      
        #region constants
        /// <summary>
        /// Inner types for MATLAB data elements
        /// </summary>
        public enum MatFileType : int {
            /// <summary>
            /// unknown 
            /// </summary>
            miUNKNOWN = 0,
            /// <summary>
            /// Int8
            /// </summary>
            miINT8 = 1,
            /// <summary>
            /// UInt8
            /// </summary>
            miUINT8 = 2,
            /// <summary>
            /// Int16
            /// </summary>
            miINT16 = 3,
            /// <summary>
            /// UInt16
            /// </summary>
            miUINT16 = 4,
            /// <summary>
            /// int32
            /// </summary>
            miINT32 = 5,
            /// <summary>
            /// UInt32
            /// </summary>
            miUINT32 = 6,
            /// <summary>
            /// float
            /// </summary>
            miSINGLE = 7,
            /// <summary>
            /// double
            /// </summary>
            miDOUBLE = 9,
            /// <summary>
            /// Int64
            /// </summary>
            miINT64 = 12,
            /// <summary>
            /// UInt64
            /// </summary>
            miUINT64 = 13,
            /// <summary>
            /// matrix type (general)
            /// </summary>
            miMATRIX = 14,
            /// <summary>
            /// compressed
            /// </summary>
            miCOMPRESSED = 15,
            /// <summary>
            /// utf8 encoded
            /// </summary>
            miUTF8 = 16,
            /// <summary>
            /// utf16 encoded
            /// </summary>
            miUTF16 = 17,
            /// <summary>
            /// utf32 encoded
            /// </summary>
            miUTF32 = 18
        }
        /// <summary>
        /// Types for matrix chunks
        /// </summary>
        public enum MatFileArrayClass {
            /// <summary>
            /// cell
            /// </summary>
            mxCELL_CLASS = 1,
            /// <summary>
            /// struct
            /// </summary>
            mxSTRUCT_CLASS = 2,
            /// <summary>
            /// object
            /// </summary>
            mxOBJECT_CLASS = 3,
            /// <summary>
            /// char
            /// </summary>
            mxCHAR_CLASS = 4,
            /// <summary>
            /// sparse
            /// </summary>
            mxSPARSE_CLASS = 5,
            /// <summary>
            /// double
            /// </summary>
            mxDOUBLE_CLASS = 6,
            /// <summary>
            /// float
            /// </summary>
            mxSINGLE_CLASS = 7,
            /// <summary>
            /// Int8
            /// </summary>
            mxINT8_CLASS = 8,
            /// <summary>
            /// UInt8
            /// </summary>
            mxUINT8_CLASS = 9,
            /// <summary>
            /// Int16
            /// </summary>
            mxINT16_CLASS = 10,
            /// <summary>
            /// UInt16
            /// </summary>
            mxUINT16_CLASS = 11,
            /// <summary>
            /// Int32
            /// </summary>
            mxINT32_CLASS = 12,
            /// <summary>
            /// UInt32
            /// </summary>
            mxUINT32_CLASS = 13,
            /// <summary>
            /// Int32
            /// </summary>
            mxINT64_CLASS = 14,
            /// <summary>
            /// UInt32
            /// </summary>
            mxUINT64_CLASS = 15
        }

        /// <summary>
        /// List of keywords which Matlab disallows for variable names
        /// </summary>
        public static readonly string[] ReservedKeywords = new string[] {
            "break",
            "case"  ,
            "catch"  ,
            "continue",
            "else"    ,
            "elseif"  ,
            "end"     ,
            "for"     ,
            "function",
            "global"  ,
            "if"      ,
            "otherwise",
            "persistent",
            "return"    ,
            "switch"    ,
            "try"       ,
            "while"                
        };

        private static int miSIZE_INT32    = 4;
        private static int miSIZE_INT16    = 2;
        private static int miSIZE_INT8     = 1;
        private static int miSIZE_UINT32   = 4;
        private static int miSIZE_UINT16   = 2;
        private static int miSIZE_UINT8    = 1;
        private static int miSIZE_DOUBLE   = 8;
        private static int miSIZE_SINGLE   = 4;
        private static int miSIZE_UTF32    = 4;
        private static int miSIZE_INT64    = 8;
        private static int miSIZE_UINT64   = 8;

#pragma warning disable CS0414
        /* Matlab Array Types (Classes) */
        private static int mxUNKNOWN_CLASS = 0;
        private static int mxCELL_CLASS    = 1;
        private static int mxSTRUCT_CLASS  = 2;
        private static int mxOBJECT_CLASS  = 3;
        private static int mxCHAR_CLASS    = 4;
        private static int mxSPARSE_CLASS  = 5;
        private static int mxDOUBLE_CLASS  = 6;
        private static int mxSINGLE_CLASS  = 7;
        private static int mxINT8_CLASS    = 8;
        private static int mxUINT8_CLASS   = 9;
        private static int mxINT16_CLASS   = 10;
        private static int mxUINT16_CLASS  = 11;
        private static int mxINT32_CLASS   = 12;
        private static int mxUINT32_CLASS  = 13;
        private static int mxINT64_CLASS   = 14;
        private static int mxUINT64_CLASS  = 15;
        private static int mxFUNCTION_CLASS= 16;
        private static int mxOPAQUE_CLASS  = 17;
        
        private static int mtFLAG_COMPLEX  = 0x0800;
        private static int mtFLAG_GLOBAL   = 0x0400;
        private static int mtFLAG_LOGICAL  = 0x0200;
        private static int mtFLAG_TYPE     = 0xff;
#pragma warning restore CS0414
        #endregion

        #region private helper
        /// <summary>
        /// size of single elements stored in Matlab's *.mat files
        /// </summary>
        /// <param name="type">one of Matlab's inner element types</param>
        /// <returns>size in bytes </returns>
        private static int sizeOf(MatFileType type)
        {
            switch ( type )
            {
                case MatFileType.miINT8:
                    return miSIZE_INT8;
                case MatFileType.miUINT8:
                    return miSIZE_UINT8;
                case MatFileType.miINT16:
                    return miSIZE_INT16;
                case MatFileType.miUINT16:
                    return miSIZE_UINT16;
                case MatFileType.miINT32:
                    return miSIZE_INT32;
                case MatFileType.miUINT32:
                    return miSIZE_UINT32;
                case MatFileType.miINT64:
                    return miSIZE_INT64; 
                case MatFileType.miUINT64:
                    return miSIZE_UINT64; 
                case MatFileType.miDOUBLE:
                    return miSIZE_DOUBLE;
                case MatFileType.miSINGLE:
                    return miSIZE_SINGLE;
                case MatFileType.miUTF32:
                    return miSIZE_UTF32;
                default:
                    throw new ArgumentException("Invalid MatFileType specified: " + type.ToString()); 
            }
        }
        private BaseArray read_compressed(BinaryReader br, int len) {
            throw new NotImplementedException("Compressed matfile format is not supported! Use '-v6' option in Matlab to create the matfile or consider using ILNumerics.IO.HDF5."); 
            //long startpos = br.BaseStream.Position; 
            ////ZOutputStream zstream = new ZOutputStream(br.BaseStream); 
            //GZipStream str = new GZipStream(br.BaseStream,CompressionMode.Decompress); 
            //BinaryReader bread = new BinaryReader(str);
            //MatFileType dataType = (MatFileType)Enum.Parse(typeof(MatFileType), bread.ReadInt32().ToString()); 
            //int elementLength = bread.ReadInt32();
            //BaseArray ret = null; 
            //if (dataType == MatFileType.miMATRIX) {
            //    ret = read_miMATRIX(bread);     
            //}
            //return ret; 
        }
        private void read_header(BinaryReader br) {
            headerFromFile = br.ReadBytes(116).ToString();
            // skip subsystem data 
            br.ReadBytes(8);
            // version 
            int version = br.ReadInt16();
            if (br.ReadChar() != 'I' || br.ReadByte() != 'M')
                throw new Exception("This file eventually was written on a machine, which is not compatible " +
                    " to this one due to an endian type issue!");
        }
        /// <summary>
        /// read ONE array (arbitrary dimensions/type) from MAT file 
        /// </summary>
        /// <param name="br">binary reader initialized and pointing to the beginning of the subarray element.</param>
        /// <param name="name">[Output] the array name as read from the file.</param>
        /// <returns>BaseArray of size and type originally stored into the mat file.</returns>
        
        private unsafe BaseArray read_miMATRIX(BinaryReader br, out string name) {
            long entryPositionInStream = br.BaseStream.Position;
            bool complex = false;
            bool logical = false;
            int mxClass = 0;
            int[] dimensions = new int[0];
            MatFileType storageType = MatFileType.miUNKNOWN;
            int nrElements = 1;
            // read array flags 
            Int32 readInt = br.ReadInt32();
            if (readInt != 6)
                throw new Exception("found invalid datatype in array flag! currently only 'mxArray' types are supported!");
            readInt = br.ReadInt32();
            if (readInt != 8)
                throw new Exception("unexpected array flag length. expected: 8 /found: " + readInt);
            readInt = br.ReadInt32();
            complex = (readInt & mtFLAG_COMPLEX) != 0;
            logical = (readInt & mtFLAG_LOGICAL) != 0;
            mxClass = readInt & 0x00ff;
            // unknown
            br.ReadInt32();
            // Read dimensions array 
            readInt = br.ReadInt32();
            if (readInt != 5)
                throw new Exception("found invalid datatype in dimension flag!");
            readInt = br.ReadInt32();
            if (readInt < 2)
                throw new Exception("Invalid number of dimensions found: " + readInt);
            if (readInt > 7 * 4) {
                throw new NotSupportedException($"Too many dimensions: the maximum number of dimensions supported is: {Size.MaxNumberOfDimensions}.");
            }
            dimensions = new int[(int)readInt / 4];
            for (int i = 0; i < dimensions.Length; i++) {
                dimensions[i] = br.ReadInt32();
                nrElements *= dimensions[i];
            }
            // padidng if needed
            if ((dimensions.Length % 2) != 0)
                br.ReadInt32();
            // read Name - check for small data element format 
            readInt = br.ReadInt32();
            int nrSmallBytes = (int)((readInt & 0xffff0000) >> 16);
            if (nrSmallBytes != 0) {
                // process small element format 
                if ((readInt & 0xffff) != 1)
                    throw new Exception("Invalid datype for (compressed) name element found: " + (readInt & 0x00ff));
                StringBuilder nameBuild = new StringBuilder();
                nameBuild.Append(br.ReadChars(nrSmallBytes));
                // padding if needed
                while (nrSmallBytes < 4) {
                    br.ReadByte();
                    nrSmallBytes++;
                }
                name = nameBuild.ToString();
            } else {
                // process 'long' format 
                if (readInt != 1)
                    throw new Exception("Invalid datype for name element found: " + readInt);
                readInt = br.ReadInt32();
                StringBuilder nameBuild = new StringBuilder();
                nameBuild.Append(br.ReadChars(readInt));
                while (readInt % 8 != 0) {
                    readInt++;
                    br.ReadByte();
                }
                name = nameBuild.ToString();
            }
            // read data flags + check if small format
            readInt = br.ReadInt32();
            nrSmallBytes = (Int16)((readInt & 0xffff0000) >> 16);
            MemoryHandle realData = null;
            MemoryHandle imagData = null;
            long len;
            if (nrSmallBytes != 0 && nrElements <= 4) {
                // small data element format for scalars only! 
                // process small format -> real part 
                storageType = (MatFileType)(readInt & 0xffff);
                len = nrSmallBytes ;
                readElementGeneric(br, storageType, out realData, len, 4);
            } else {
                // read regular data : real part
                storageType = (MatFileType)Enum.Parse(typeof(MatFileType), readInt.ToString());
                len = br.ReadInt32() ;
                readElementGeneric(br, storageType, out realData, len);
            }

            // read imag part + check if small format
            if (complex) {
                readInt = br.ReadInt32();
                nrSmallBytes = (Int16)((readInt & 0xffff0000) >> 16);
                if (nrSmallBytes != 0 && nrElements <= 4) {
                    // process small format -> imag part 
                    storageType = (MatFileType)(readInt & 0xffff);
                    len = nrSmallBytes ;
                    readElementGeneric(br, storageType, out imagData, len, 4);
                } else {
                    // read regular data : image part
                    storageType = (MatFileType)Enum.Parse(typeof(MatFileType), readInt.ToString()); ;
                    len = br.ReadInt32() ;
                    nrSmallBytes = (int)len;
                    readElementGeneric(br, storageType, out imagData, len);
                }
            }
            // convert to original data type 
            if (complex) {
                if (mxClass == mxSINGLE_CLASS) {

                    Storage<fcomplex> retS = Storage<fcomplex>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1; 
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i]; 
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0; 

                    var rMem = Convert2SingleArray(realData, storageType, nrElements);
                    var iMem = Convert2SingleArray(imagData, storageType, nrElements);
                    float* rP = (float*)rMem.Pointer, iP = (float*)iMem.Pointer;
                    retS.Handles[0] = New<fcomplex>((ulong)nrElements);
                    fcomplex* oP = (fcomplex*)retS.Handles[0].Pointer; 

                    for (int i = 0; i < nrElements; i++) {
                        oP[i] = new fcomplex(rP[i], iP[i]);
                    }
                    rMem?.Cache(DeviceManager.GetDevice(0).MemoryPool); 
                    iMem?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray; 

                } else {
                    System.Diagnostics.Debug.Assert(mxClass == mxDOUBLE_CLASS); 

                    Storage<complex> retS = Storage<complex>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    var rMem = Convert2DoubleArray(realData, storageType, nrElements);
                    var iMem = Convert2DoubleArray(imagData, storageType, nrElements);
                    double* rP = (double*)rMem.Pointer, iP = (double*)iMem.Pointer;
                    retS.Handles[0] = New<complex>((ulong)nrElements);
                    complex* oP = (complex*)retS.Handles[0].Pointer;

                    for (int i = 0; i < nrElements; i++) {
                        oP[i] = new complex(rP[i], iP[i]);
                    }
                    rMem?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    iMem?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;
                }
            } else if (logical) {
                LogicalStorage retL = LogicalStorage.Create();
                // set size: always column major
                var bsd = retL.S.GetBSD(true);
                long stride = 1;
                for (int i = 0; i < dimensions.Length; i++) {
                    bsd[3 + i] = dimensions[i];
                    bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                    stride *= dimensions[i];
                }
                bsd[0] = dimensions.Length;
                bsd[1] = stride;
                bsd[2] = 0;
                if (storageType != MatFileType.miINT8) {
                    retL.Handles[0] = Convert2ByteArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                } else {
                    retL.Handles[0] = realData;
                }
                imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                return retL.RetArray;

            } else {
                if (false) {
                    #region HYCALPER LOOPSTART
                    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            mxDOUBLE_CLASS
        </source>
            <destination>mxCHAR_CLASS</destination>
            <destination>mxSINGLE_CLASS</destination>
            <destination>mxINT8_CLASS</destination>
            <destination>mxUINT8_CLASS</destination>
            <destination>mxINT16_CLASS</destination>
            <destination>mxUINT16_CLASS</destination>
            <destination>mxINT32_CLASS</destination>
            <destination>mxUINT32_CLASS</destination>
            <destination>mxINT64_CLASS</destination>
            <destination>mxUINT64_CLASS</destination>
    </type>
    <type>
        <source locate="here">
            double
        </source>
        <destination>char</destination>
        <destination>float</destination>
        <destination>sbyte</destination>
        <destination>byte</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
    </type>
    <type>
        <source locate="here">
            Convert2DoubleArray
        </source>
        <destination>Convert2CharArray</destination>
        <destination>Convert2SingleArray</destination>
        <destination>Convert2SByteArray</destination>
        <destination>Convert2ByteArray</destination>
        <destination>Convert2Int16Array</destination>
        <destination>Convert2UInt16Array</destination>
        <destination>Convert2Int32Array</destination>
        <destination>Convert2UInt32Array</destination>
        <destination>Convert2Int64Array</destination>
        <destination>Convert2UInt64Array</destination>
    </type>
    <type>
        <source locate="after">
            pattern5
        </source>
        <destination><![CDATA[Array<char>]]></destination>
        <destination><![CDATA[Array<float>]]></destination>
        <destination><![CDATA[Array<sbyte>]]></destination>
        <destination><![CDATA[Array<byte>]]></destination>
        <destination><![CDATA[Array<Int16>]]></destination>
        <destination><![CDATA[Array<UInt16>]]></destination>
        <destination><![CDATA[Array<Int32>]]></destination>
        <destination><![CDATA[Array<UInt32>]]></destination>
        <destination><![CDATA[Array<Int64>]]></destination>
        <destination><![CDATA[Array<UInt64>]]></destination>
    </type>
    </hycalper>
    */
                } else if (mxClass == mxDOUBLE_CLASS) {

                    Storage<double> retS = Storage<double>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <double> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2DoubleArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
                   
                } else if (mxClass == mxUINT64_CLASS) {

                    Storage<UInt64> retS = Storage<UInt64>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <UInt64> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2UInt64Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxINT64_CLASS) {

                    Storage<Int64> retS = Storage<Int64>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <Int64> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2Int64Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxUINT32_CLASS) {

                    Storage<UInt32> retS = Storage<UInt32>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <UInt32> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2UInt32Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxINT32_CLASS) {

                    Storage<Int32> retS = Storage<Int32>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <Int32> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2Int32Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxUINT16_CLASS) {

                    Storage<UInt16> retS = Storage<UInt16>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <UInt16> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2UInt16Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxINT16_CLASS) {

                    Storage<Int16> retS = Storage<Int16>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <Int16> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2Int16Array(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxUINT8_CLASS) {

                    Storage<byte> retS = Storage<byte>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <byte> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2ByteArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxINT8_CLASS) {

                    Storage<sbyte> retS = Storage<sbyte>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <sbyte> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2SByteArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxSINGLE_CLASS) {

                    Storage<float> retS = Storage<float>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <float> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2SingleArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;

                   
                } else if (mxClass == mxCHAR_CLASS) {

                    Storage<char> retS = Storage<char>.Create();
                    // set size: always column major
                    var bsd = retS.S.GetBSD(true);
                    long stride = 1;
                    for (int i = 0; i < dimensions.Length; i++) {
                        bsd[3 + i] = dimensions[i];
                        bsd[3 + dimensions.Length + i] = (dimensions[i] == 1 ? 0 : stride);
                        stride *= dimensions[i];
                    }
                    bsd[0] = dimensions.Length;
                    bsd[1] = stride;
                    bsd[2] = 0;

                    //System.Diagnostics.Debug.WriteLine($"MatFile: Reading subdata element. Position: {entryPositionInStream}. <char> {retS.S.ToString()}");

                    retS.Handles[0] = Convert2CharArray(realData, storageType, nrElements);
                    realData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    imagData?.Cache(DeviceManager.GetDevice(0).MemoryPool);
                    return retS.RetArray;


#endregion HYCALPER AUTO GENERATED CODE
               } else {
                    throw new Exception("element data type is not supported");
                }
            }
        }

        #region HYCALPER LOOPSTART Conversion for all numeric types
        /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            Convert2DoubleArray
        </source>
            <destination>Convert2SingleArray</destination>
            <destination>Convert2CharArray</destination>
            <destination>Convert2Int16Array</destination>
            <destination>Convert2Int32Array</destination>
            <destination>Convert2Int64Array</destination>
            <destination>Convert2UInt16Array</destination>
            <destination>Convert2UInt32Array</destination>
            <destination>Convert2UInt64Array</destination>
            <destination>Convert2ByteArray</destination>
            <destination>Convert2SByteArray</destination>
    </type>
    <type>
        <source locate="here">
            double
        </source>
            <destination>float</destination>
            <destination>char</destination>
            <destination>Int16</destination>
            <destination>Int32</destination>
            <destination>Int64</destination>
            <destination>UInt16</destination>
            <destination>UInt32</destination>
            <destination>UInt64</destination>
            <destination>byte</destination>
            <destination>sbyte</destination>
    </type>
    </hycalper>
    */
        
        private unsafe MemoryHandle Convert2DoubleArray(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<double>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)pOut + i) = (double)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(double).Name}'.");

            }
            return ret;
        }
        #endregion HYCALPER LOOPEND Conversion for all numeric types
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        private unsafe MemoryHandle Convert2SByteArray(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<sbyte>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)pOut + i) = (sbyte)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(sbyte).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2ByteArray(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<byte>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)pOut + i) = (byte)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(byte).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2UInt64Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<UInt64>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt64*)pOut + i) = (UInt64)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(UInt64).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2UInt32Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<UInt32>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt32*)pOut + i) = (UInt32)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(UInt32).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2UInt16Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<UInt16>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((UInt16*)pOut + i) = (UInt16)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(UInt16).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2Int64Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<Int64>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int64*)pOut + i) = (Int64)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(Int64).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2Int32Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<Int32>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int32*)pOut + i) = (Int32)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(Int32).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2Int16Array(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<Int16>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((Int16*)pOut + i) = (Int16)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(Int16).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2CharArray(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<char>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((char*)pOut + i) = (char)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(char).Name}'.");

            }
            return ret;
        }
       
        
        private unsafe MemoryHandle Convert2SingleArray(MemoryHandle input, MatFileType storageType, long elemCount) {
            // keep track of type matches ! No checks will be made! 

            MemoryHandle ret = New<float>((ulong)elemCount);
            void* pOut = (void*)ret.Pointer;
            void* pIn = (void*)input.Pointer; 
            switch (storageType) {
                case MatFileType.miINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((sbyte*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT8:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((byte*)pIn + i));
                    }
                    break;

                case MatFileType.miINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((short*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT16:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((ushort*)pIn + i));
                    }
                    break;

                case MatFileType.miINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((int*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT32:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((uint*)pIn + i));
                    }
                    break;

                case MatFileType.miINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((long*)pIn + i));
                    }
                    break;

                case MatFileType.miUINT64:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((ulong*)pIn + i));
                    }
                    break;

                case MatFileType.miSINGLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((float*)pIn + i));
                    }
                    break;

                case MatFileType.miDOUBLE:
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)pOut + i) = (float)(*((Double*)pIn + i));
                    }
                    break;

                default:
                    throw new InvalidCastException($"cannot convert from element type '{storageType}' to '{typeof(float).Name}'.");

            }
            return ret;
        }

#endregion HYCALPER AUTO GENERATED CODE

        private byte[] Convert2Logical(System.Array input, out int numNonzero) {
            // keep track of type matches ! No checks will be made! 
            numNonzero = 0;
            byte[] ret = new byte[input.Length];
            switch (input.GetType().Name.ToLower()) {
                case "char[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((char)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "uint64[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((UInt64)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "uint32[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((UInt32)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "uint16[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((UInt16)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "int64[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((Int64)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "int32[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((Int32)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "int16[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((Int16)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "single[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((float)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "double[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((double)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "byte[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((byte)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;

                case "sbyte[]":
                    for (int i = 0; i < input.Length; i++) {
                        if ((sbyte)input.GetValue(i) == 0) {
                            ret[i] = 0;
                        } else {
                            ret[i] = 1;
                            numNonzero++;
                        }
                    }
                    break;
                default:
                    throw new InvalidCastException("cannot convert from '" + input.GetType().Name + "'!");
            }
            return ret;
        }
        
        /// <summary>
        /// read array of supported matlab data types 
        /// </summary>
        /// <param name="br">binary reader, opened and correctly positioned</param>
        /// <param name="storageType">actual storage type</param>
        /// <param name="realData">output: on return, the array read</param>
        /// <param name="len">input: number of _bytes_ to read.</param>
        /// <param name="paddBytes">padding border, the stream will be read to the next border of length 'paddBytes'.</param>
        
        private unsafe static void readElementGeneric(BinaryReader br, MatFileType storageType, out MemoryHandle realData, long len, int paddBytes = 8) {
            void* p;
            long elemCount; 
            switch (storageType) {
                #region HYCALPER LOOPSTART
                /*!HC:TYPELIST:
                <hycalper>
                <type>
                    <source locate="here">
                        MatFileType.miINT8
                    </source>
                    <destination>MatFileType.miUINT8</destination>
                    <destination>MatFileType.miINT16</destination>
                    <destination>MatFileType.miUINT16</destination>
                    <destination>MatFileType.miINT32</destination>
                    <destination>MatFileType.miUINT32</destination>
                    <destination>MatFileType.miINT64</destination>
                    <destination>MatFileType.miUINT64</destination>
                    <destination>MatFileType.miSINGLE</destination>
                    <destination>MatFileType.miDOUBLE</destination>
                </type>
                <type>
                    <source locate="here">
                        br.ReadSByte()
                    </source>
                    <destination>br.ReadByte()</destination>
                    <destination>br.ReadInt16()</destination>
                    <destination>br.ReadUInt16()</destination>
                    <destination>br.ReadInt32()</destination>
                    <destination>br.ReadUInt32()</destination>
                    <destination>br.ReadInt64()</destination>
                    <destination>br.ReadUInt64()</destination>
                    <destination>br.ReadSingle()</destination>
                    <destination>br.ReadDouble()</destination>
                </type>
                <type>
                    <source locate="here">
                        sbyte
                    </source>
                    <destination>byte</destination>
                    <destination>short</destination>
                    <destination>ushort</destination>
                    <destination>int</destination>
                    <destination>uint</destination>
                    <destination>long</destination>
                    <destination>ulong</destination>
                    <destination>float</destination>
                    <destination>double</destination>
                </type>
</hycalper>
                */


                case MatFileType.miINT8:
                    elemCount = len / sizeOf(MatFileType.miINT8);  
                    realData = New<sbyte>((ulong)elemCount);
                    p = (sbyte*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((sbyte*)p + i) = br.ReadSByte();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
                #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
               


                case MatFileType.miDOUBLE:
                    elemCount = len / sizeOf(MatFileType.miDOUBLE);  
                    realData = New<double>((ulong)elemCount);
                    p = (double*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((double*)p + i) = br.ReadDouble();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miSINGLE:
                    elemCount = len / sizeOf(MatFileType.miSINGLE);  
                    realData = New<float>((ulong)elemCount);
                    p = (float*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((float*)p + i) = br.ReadSingle();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miUINT64:
                    elemCount = len / sizeOf(MatFileType.miUINT64);  
                    realData = New<ulong>((ulong)elemCount);
                    p = (ulong*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((ulong*)p + i) = br.ReadUInt64();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miINT64:
                    elemCount = len / sizeOf(MatFileType.miINT64);  
                    realData = New<long>((ulong)elemCount);
                    p = (long*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((long*)p + i) = br.ReadInt64();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miUINT32:
                    elemCount = len / sizeOf(MatFileType.miUINT32);  
                    realData = New<uint>((ulong)elemCount);
                    p = (uint*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((uint*)p + i) = br.ReadUInt32();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miINT32:
                    elemCount = len / sizeOf(MatFileType.miINT32);  
                    realData = New<int>((ulong)elemCount);
                    p = (int*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((int*)p + i) = br.ReadInt32();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miUINT16:
                    elemCount = len / sizeOf(MatFileType.miUINT16);  
                    realData = New<ushort>((ulong)elemCount);
                    p = (ushort*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((ushort*)p + i) = br.ReadUInt16();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miINT16:
                    elemCount = len / sizeOf(MatFileType.miINT16);  
                    realData = New<short>((ulong)elemCount);
                    p = (short*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((short*)p + i) = br.ReadInt16();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;
               


                case MatFileType.miUINT8:
                    elemCount = len / sizeOf(MatFileType.miUINT8);  
                    realData = New<byte>((ulong)elemCount);
                    p = (byte*)realData.Pointer; 
                    for (int i = 0; i < elemCount; i++) {
                        *((byte*)p + i) = br.ReadByte();
                    }
                    while (len % paddBytes != 0) {
                        br.ReadByte();
                        len++;
                    }
                    break;

#endregion HYCALPER AUTO GENERATED CODE

                case MatFileType.miMATRIX:
                    throw new NotSupportedException("matrix data type not expected as inner datatype!");
                case MatFileType.miCOMPRESSED:
                    throw new NotSupportedException("Compressed matrix are not supported (yet)! Consider using ILNumerics.IO.HDF5.");
                case MatFileType.miUTF8:
                    var bytes = br.ReadBytes((int)len);
                    len = UTF8Encoding.UTF8.GetCharCount(bytes);
                    realData = New<char>((ulong)len);
                    p = (char*)realData.Pointer;
                    fixed (byte* bP = bytes) {
                        UTF8Encoding.UTF8.GetChars(bP, bytes.Length, (char*)p, (int)len);
                    }
                    while ((len * sizeOf(storageType) % paddBytes) != 0) {
                        len++;
                        br.ReadInt16();
                    }
                    break;
                case MatFileType.miUTF16:
                    throw new NotSupportedException("UTF16 data type is not supported! Consider using ILNumerics.IO.HDF5.");
                case MatFileType.miUTF32:
                    throw new NotSupportedException("UTF32 data type is not supported! Consider using ILNumerics.IO.HDF5.");
                default:
                    throw new Exception($"Unknown element data type found: '{storageType}'. Cancelling...");
            }
        }
        private void write(BinaryWriter fileout) {

            // write MAT-header
            StringBuilder headerLine = new StringBuilder($"vers. 5.0 MAT-FILE Creator: ILNumerics, {DateTime.Now.Year}");
            while (headerLine.Length < 123)
                headerLine.Append(' ');
            fileout.Write(headerLine.ToString());
            fileout.Write((short)0x0100);
            fileout.Write((short)0x4d49);

            foreach (string name in Keys) {
                BaseArray arr = this[name]; 
                if (arr is BaseArray<double>) {
                    WriteStorage((arr as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<float>) {
                    WriteStorage((arr as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<sbyte>) {
                    WriteStorage((arr as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<byte>) {
                    WriteStorage((arr as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<short>) {
                    WriteStorage((arr as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<ushort>) {
                    WriteStorage((arr as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<int>) {
                    WriteStorage((arr as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<uint>) {
                    WriteStorage((arr as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<long>) {
                    WriteStorage((arr as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<ulong>) {
                    WriteStorage((arr as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<complex>) {
                    WriteStorage((arr as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<fcomplex>) {
                    WriteStorage((arr as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>)?.Storage, fileout, name);
                } else if (arr is BaseArray<bool>) {
                    WriteStorage((arr as ConcreteArray<bool, Logical, InLogical,OutLogical,Logical,LogicalStorage>)?.Storage, fileout, name);
                } else {
                    throw new NotSupportedException($"Unsupported element type: {arr?.ToString() ?? arr?.GetType().GetGenericArguments()[0].Name}.");
                }
                // assuming internals: cell always returns RetT.
                //arr.Release(); 
            }
            fileout.Close();
        }

        
        private unsafe void WriteStorage<T, LocalT, InT, OutT, RetT, StorageT>(BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, BinaryWriter fileout, string name)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: false);
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"MatFile: Creating subdata element. Position: {fileout.BaseStream.Position}. <{typeof(T).Name}> {storage.S.ToString()}");

#endif 

            // determine overall length
            int[] dimensionSubelement = createDimensionSubelement(storage.S);
            byte[] nameElememData; int nameElemType;
            createNameSubelement(name, out nameElemType, out nameElememData);
            // write subarray header               
            // mxarray subelement type
            fileout.Write((int)MatFileType.miMATRIX);
            // determine length of single array element
            int elemLen = getElementLength(storage);
            //System.Diagnostics.Debug.Assert(arr.IsMatrix,"TODO: n-dim. arrays are not implemented yet!"); 
            // overall length of subarray container 
            int allLength = 16 // array flags 
                            + dimensionSubelement.Length * 4   // dimension element
                            + nameElememData.Length + 8; // name element 
            int dataSubelemLen = elemLen * (int)storage.Size.NumberOfElements + 8;
            // recognize padding! 
            if (dataSubelemLen % 8 != 0) {
                dataSubelemLen += (8 - ((elemLen * (int)storage.Size.NumberOfElements) % 8));
            }

            allLength += dataSubelemLen;
            if (storage is Storage<complex> || storage is Storage<fcomplex>) {
                allLength += dataSubelemLen;
            }
            fileout.Write(allLength);
            // subelement: array flags 
            // miUInt32 , length: 8
            fileout.Write((int)MatFileType.miUINT32);
            fileout.Write(8);
            // write array flags  
            int flag = getElementClass(storage);

            if (storage is Storage<complex> || storage is Storage<fcomplex>) {
                flag |= mtFLAG_COMPLEX;
            }
            if (storage is LogicalStorage) {
                flag |= mtFLAG_LOGICAL;
            }
            fileout.Write(flag);
            fileout.Write(0);      // this is later used for sparse arrays 
                                   // write dimensions tag     
            for (int i = 0; i < dimensionSubelement.Length; i++)
                fileout.Write(dimensionSubelement[i]);
            // write array name      
            fileout.Write((int)MatFileType.miINT8);
            fileout.Write(name.Length);
            fileout.Write(nameElememData);
            // write matrix elements     
            allLength = (int)storage.Size.NumberOfElements;
            if (false) {
                #region HYCALPER LOOPSTART
                /*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>sbyte</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>byte</destination>
        <destination>char</destination>
        <destination>float</destination>
        <destination>Int16</destination>
        <destination>Int32</destination>
        <destination>UInt16</destination>
        <destination>UInt32</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="here">
            MatFileType.miDOUBLE
        </source>
        <destination>MatFileType.miINT8</destination>
        <destination>MatFileType.miINT64</destination>
        <destination>MatFileType.miUINT64</destination>
        <destination>MatFileType.miUINT8</destination>
        <destination>MatFileType.miUINT16</destination>
        <destination>MatFileType.miSINGLE</destination>
        <destination>MatFileType.miINT16</destination>
        <destination>MatFileType.miINT32</destination>
        <destination>MatFileType.miUINT16</destination>
        <destination>MatFileType.miUINT32</destination>
        <destination>MatFileType.miDOUBLE</destination>
        <destination>MatFileType.miSINGLE</destination>
    </type>
    <type>
        <source locate="after">
            HCGetElemCmplx
        </source>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination>.real</destination>
        <destination>.real</destination>
    </type>
 </hycalper>
 */

            } else if (storage is Storage<double>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miDOUBLE);
                fileout.Write(allLength * elemLen);

                double* arrP = (double*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write(/*!HC:outCast*/ //
                        arrP[i] /*!HC:HCGetElemCmplx*/ // 
                    );
                }
                #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
               

            } else if (storage is Storage<fcomplex>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miSINGLE);
                fileout.Write(allLength * elemLen);

                fcomplex* arrP = (fcomplex*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]  .real 
                    );
                }
               

            } else if (storage is Storage<complex>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miDOUBLE);
                fileout.Write(allLength * elemLen);

                complex* arrP = (complex*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]  .real 
                    );
                }
               

            } else if (storage is Storage<UInt32>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miUINT32);
                fileout.Write(allLength * elemLen);

                UInt32* arrP = (UInt32*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<UInt16>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miUINT16);
                fileout.Write(allLength * elemLen);

                UInt16* arrP = (UInt16*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<Int32>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miINT32);
                fileout.Write(allLength * elemLen);

                Int32* arrP = (Int32*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<Int16>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miINT16);
                fileout.Write(allLength * elemLen);

                Int16* arrP = (Int16*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<float>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miSINGLE);
                fileout.Write(allLength * elemLen);

                float* arrP = (float*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<char>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miUINT16);
                fileout.Write(allLength * elemLen);

                char* arrP = (char*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<byte>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miUINT8);
                fileout.Write(allLength * elemLen);

                byte* arrP = (byte*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<UInt64>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miUINT64);
                fileout.Write(allLength * elemLen);

                UInt64* arrP = (UInt64*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<Int64>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miINT64);
                fileout.Write(allLength * elemLen);

                Int64* arrP = (Int64*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }
               

            } else if (storage is Storage<sbyte>) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miINT8);
                fileout.Write(allLength * elemLen);

                sbyte* arrP = (sbyte*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]   
                    );
                }

#endregion HYCALPER AUTO GENERATED CODE
            } else if (storage is LogicalStorage) {
                // header of array subdata element      
                fileout.Write((int)MatFileType.miINT8);
                fileout.Write(allLength * elemLen);

                sbyte* arrP = (sbyte*)storage.Handles[0].Pointer;

                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    fileout.Write( //
                        arrP[i]
                    );
                }
            } else {
                throw new FormatException("The format of the array was not known!");
            }
            // pad to 8 byte border 
            var tmpInt = allLength * elemLen;
            byte dummy = 0;
            while (tmpInt % 8 != 0) {
                fileout.Write(dummy);
                tmpInt++;
            }
            #region imaginary part
            if (storage is Storage<complex> || storage is Storage<fcomplex>) {
                if (storage is Storage<complex>) {

                    // header of array subdata element      
                    fileout.Write((int)MatFileType.miDOUBLE);
                    fileout.Write(allLength * elemLen);

                    complex* arrP = (complex*)storage.Handles[0].Pointer;

                    for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                        fileout.Write(arrP[i].imag);
                    }
                } else if (storage is Storage<fcomplex>) {
                    fileout.Write((int)MatFileType.miSINGLE);
                    fileout.Write(allLength * elemLen);

                    fcomplex* arrP = (fcomplex*)storage.Handles[0].Pointer;

                    for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                        fileout.Write(arrP[i].imag);
                    }
                }
                // pad to 8 byte border 
                tmpInt = allLength * elemLen;
                dummy = 0;
                while (tmpInt % 8 != 0) {
                    fileout.Write(dummy);
                    tmpInt++;
                }
            }
            #endregion

        }

        /// <summary>
        /// create name subelement for Matfile storage - padded to 8 byte border
        /// </summary>
        /// <param name="arrName">name property</param>
        /// <param name="type">will be 'miINT8' on return</param>
        /// <param name="data">return data array </param>
        private void createNameSubelement(string arrName, out int type, out byte[] data) {
            int len = arrName.Length;
            if (len % 8 != 0)
                len += (8 - len % 8);
            data = new byte[len];
            for (int i = 0; i < arrName.Length; i++) {
                data[i] = (byte)arrName[i];
            }
            type = (int)MatFileType.miINT8;
            return;
        }
        private int[] createDimensionSubelement(Size arrSize) {
            int[] ret;
            long LengthInt;
            // determine length of dimensions array byte (padding to full 8 byte border)
            if (arrSize.NumberOfDimensions % 2 == 0) {
                LengthInt = (arrSize.NumberOfDimensions + 2);
            } else {
                // must pad to 8 byte border
                LengthInt = (arrSize.NumberOfDimensions + 3);
            }
            ret = new int[LengthInt];
            ret[0] = (int)MatFileType.miINT32;
            ret[1] = (int)arrSize.NumberOfDimensions * 4;
            for (uint i = 0; i < arrSize.NumberOfDimensions; i++) {
                ret[i + 2] = (int)arrSize[i];
            }
            return ret;
        }
        private int IndexOf(string name) {
            for (int i = 0; i < Count; i++) {
                if (m_data.GetValue<string>(i, 1, 0) == name)
                    return i;
            }
            return -1;
        }
        private string findName(BaseArray value) {
            string elementType = string.Empty;
            Type type = value.GetType();
            if (type.GetGenericArguments() != null && type.GetGenericArguments().Length > 0) {
                elementType = type.GetGenericArguments()[0].Name;
            } else {
                elementType = type.Name;
            }
            string dimString = value.Size.ToString();  // <---------------- releases RetT! 
            string baseName = String.Format("{0}_{1}", elementType, dimString);

            // make the new name unique
            string tempName = baseName;
            int postFix = 0;
            while (IndexOf(tempName) >= 0) {
                tempName = baseName + "_" + postFix.ToString();
                postFix++;
            }
            return tempName;
        }
        /// <summary>
        /// get mat file array class type corresponding to this arra element type
        /// </summary>
        /// <param name="storage">arra with generic system type or complex/fcomplex</param>
        /// <returns>mat file array class type code (int value)</returns>
        private static int getElementClass<T, LocalT, InT, OutT, RetT, StorageT>(BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (storage is LogicalStorage)
                return (int)MatFileArrayClass.mxINT8_CLASS;

            string innerType = typeof(T).Name;
            switch (innerType) {
                case "Double":
                    return (int)MatFileArrayClass.mxDOUBLE_CLASS;
                case "Single":
                    return (int)MatFileArrayClass.mxSINGLE_CLASS;
                case "Int16":
                    return (int)MatFileArrayClass.mxINT16_CLASS;
                case "Int32":
                    return (int)MatFileArrayClass.mxINT32_CLASS;
                case "Int64":
                    return (int)MatFileArrayClass.mxINT64_CLASS;
                case "UInt16":
                    return (int)MatFileArrayClass.mxUINT16_CLASS;
                case "UInt32":
                    return (int)MatFileArrayClass.mxUINT32_CLASS;
                case "UInt64":
                    return (int)MatFileArrayClass.mxUINT64_CLASS;
                case "complex":
                    return (int)MatFileArrayClass.mxDOUBLE_CLASS;
                case "fcomplex":
                    return (int)MatFileArrayClass.mxSINGLE_CLASS;
                case "Byte":
                    return (int)MatFileArrayClass.mxUINT8_CLASS;
                case "SByte":
                    return (int)MatFileArrayClass.mxINT8_CLASS;
                case "Char":
                    return (int)MatFileArrayClass.mxCHAR_CLASS;
                default:
                    throw new InvalidOperationException("Arrays of inner element type: '" + innerType + "' can not be written as Matfile!");
            }
        }

        /// <summary>
        /// Gets the length of a single elements of <typeparamref name="T"/> in bytes.
        /// </summary>
        /// <param name="storage">The storage instance.</param>
        /// <returns>Element length in bytes.</returns>
        private static int getElementLength<T, LocalT, InT, OutT, RetT, StorageT>(BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (storage is LogicalStorage) {
                return 1;
            } else if (storage is Storage<complex>) {
                return miSIZE_DOUBLE;
            } else if (storage is Storage<fcomplex>) {
                return miSIZE_SINGLE;
            } else {
                return (int)Storage<T>.SizeOfT;
            }
            //switch (innerType) {
            //    case "Double":
            //        return miSIZE_DOUBLE;
            //    case "Single":
            //        return miSIZE_SINGLE;
            //    case "Int16":
            //        return miSIZE_INT16;
            //    case "Int32":
            //        return miSIZE_INT32;
            //    case "Int64":
            //        return miSIZE_INT64;
            //    case "UInt16":
            //        return miSIZE_UINT16;
            //    case "UInt32":
            //        return miSIZE_UINT32;
            //    case "UInt64":
            //        return miSIZE_UINT64;
            //    case "complex":
            //        return miSIZE_DOUBLE;
            //    case "fcomplex":
            //        return miSIZE_SINGLE;
            //    case "Byte":
            //        return miSIZE_INT8;
            //    case "Char":
            //        return miSIZE_UINT16;
            //    default:
            //        throw new InvalidOperationException("Arrays of inner element type: '" + innerType + "' can not be written as Matfile!");
            //}
        }
        #endregion

        #region public interface
        /// <summary>
        /// Convert MatFileType enumeration member to string representation
        /// </summary>
        /// <param name="type">MatFileType enumeration member</param>
        /// <returns>String representing the Matlab's inner element type</returns>
        /// <remarks>This function is obsolete. You may directly use the enumeration's functionality instead.</remarks>
        internal static String typeToString(MatFileType type)
        {
            String s;
            switch (type)
            {
                case MatFileType.miUNKNOWN:
                    s = "unknown";
                    break;
                case MatFileType.miINT8:
                    s = "int8";
                    break;
                case MatFileType.miUINT8:
                    s = "uint8";
                    break;
                case MatFileType.miINT16:
                    s = "int16";
                    break;
                case MatFileType.miUINT16:
                    s = "uint16";
                    break;
                case MatFileType.miINT32:
                    s = "int32";
                    break;
                case MatFileType.miUINT32:
                    s = "uint32";
                    break;
                case MatFileType.miSINGLE:
                    s = "single";
                    break;
                case MatFileType.miDOUBLE:
                    s = "double";
                    break;
                case MatFileType.miINT64:
                    s = "int64";
                    break;
                case MatFileType.miUINT64:
                    s = "uint64";
                    break;
                case MatFileType.miMATRIX:
                    s = "matrix";
                    break;
                case MatFileType.miCOMPRESSED:
                    s = "compressed";
                    break;
                case MatFileType.miUTF8:
                    s = "uft8";
                    break;
                case MatFileType.miUTF16:
                    s = "utf16";
                    break;
                case MatFileType.miUTF32:
                    s = "utf32";
                    break;
                default:
                    s = "unknown";
                    break; 
            }
            return s;
        }

        /// <summary>
        /// Adds a new array to the collection of arrays in this MatFile container.
        /// </summary>
        /// <param name="A">Array to be added.</param>
        /// <param name="key">The name to be used as key for identifying <paramref name="A"/> among the 
        /// arrays in this MatFile.</param>
        /// <returns>String used to identify the array in the collection of arrays.</returns>
        /// <exception cref="ArgumentException">If the new name <paramref name="key"/> or the element 
        /// type of <paramref name="A"/> do not fullfill the restrictions defined by Matlab.</exception>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public string AddArray<T>(BaseArray<T> A, string key) {
            string name = key;
            if (String.IsNullOrEmpty(name)) {
                throw new ArgumentException($"The name for a new array in a MatFile5 must not be empty."); 
            }
            this[name] = A;
            return name;
        }

        /// <summary>
        /// Path to mat file, if this object was created from an existing mat file.
        /// </summary>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public string Filelocation {
            get {
                return m_filename;
            }
        }
        /// <summary>
        /// List all key names currently stored with arrays 
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
        public IList<string> Keys {
            get {
                List<string> ret = new List<string>();
                for (int i = 0; i < m_data.S[0]; i++) {
                    ret.Add(m_data.GetValue<string>(i, 1, 0));
                }
                return ret;
            }
        }
        /// <summary>
        /// Retrieve a cell with all arrays stored in the mat file
        /// </summary>
        /// <remarks>The cell returned will be clone of the arrays stored in the mat file. Altering any cell 
        /// elements will leave the arrays in the matfile (class/memory object) untouched.
        /// <para>The cell returned will be of size [n,2], where n is the number of arrays contained. The 
        /// first row saved the arrays, the second row containes scalar string arrays with the name of 
        /// the array in the corresponding row.</para>
        /// </remarks>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        /// <seealso cref="Cell"/>
        public Cell Arrays {
            get { return m_data.C; }
            private set { m_data.a = value; }
        }
        /// <summary>
        /// Retrieves an array from this MatFile instance or replaces it.
        /// </summary>
        /// <param name="name">The name of the array to retrieve or replace.</param>
        /// <remarks><para>The 'get' accessor is provided for compatibility reasons only. It may be removed in a future version!
        /// Use one of the typed retrieval functions <see cref="ILNumerics.MatFile.GetArray{T}(string)"/> or 
        /// <see cref="ILNumerics.MatFile.GetArray{T}(int)"/> instead.</para>
        /// <para>For get access the name must exist as key in the container. Use the MatFile.Keys property to get a list of all names if needed</para>
        /// <para>For set access, the name given must not be null or empty. It cannot be one of the <see cref="ReservedKeywords">ReservedKeywords</see>.
        /// If the name allready exist in the collection as name, the array currently assigned to it will be replaced. If the value is null, the current 
        /// array will be removed from the list. If the name does not already exist, the new array will be added and assigned to this name.</para>
        /// <para>Restrictions on array names: Matlab allows variables to have names of maximum length 63. Longer names are abbreviated. Names must start 
        /// with a letter and contain only digits, (ASCII) letters or underscores '_'.</para></remarks>
        /// <exception cref="ArgumentException">If the name violates a Matlab naming rule.</exception>
        public BaseArray this[string name] {
            get {
                return m_data.GetValue(IndexOf(name), 0); 
            }
            set {
                using (Scope.Enter(value, ArrayStyles.ILNumericsV4)) {
                    #region test if name is valid
                    if (String.IsNullOrEmpty(name)) {
                        throw new ArgumentException($"The name of a new array for a MatFile5 object must not be empty."); 
                    }
                    foreach (string nono in ReservedKeywords) {
                        if (name == nono)
                            throw new ArgumentException("MatFile: the name " + nono + " is a reserved keyword in Matlab and may not be used as array name!");
                    }
                    if (name.Length > 63)
                        name = name.Substring(0, 63);
                    if (!Char.IsLetter(name[0]))
                        throw new ArgumentException("MatFile: the name must start with a letter!");
                    int i;
                    for (i = 1; i < name.Length; i++) {
                        char c = name[i];
                        if (!Char.IsLetter(c) && !Char.IsDigit(c) && c != '_')
                            throw new ArgumentException("MatFile: variable names must contain letters, digits or underscores only!");
                    }
                    #endregion
                    i = IndexOf(name);
                    if (i >= 0) {
                        // alter array
                        if (object.Equals(value, null))
                            // remove from 
                            m_data[i, Globals.full] = null;
                        else
                            m_data.SetValue(value, i, 0);
                    } else {
                        if (object.Equals(value, null))
                            return;
                        // add array 
                        m_data.SetValue(name, m_data.S[0], 1);
                        m_data.SetValue(value, m_data.S[0] - 1, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Number of arrays in the mat file container
        /// </summary>
        public uint Count { get { return (uint)m_data.S[0]; } }
        /// <summary>
        /// Retrieves array by name.
        /// </summary>
        /// <typeparam name="T">Expected type of the array</typeparam>
        /// <param name="name">Name of the array to retrieve.</param>
        /// <returns>A clone of the array found or null, if no array with the given name exists.</returns>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public Array<T> GetArray<T>(string name) {
            int ind = IndexOf(name);
            if (ind >= 0)
                return m_data.GetArray<T>(ind, 0);
            throw new ArgumentException("no array with name '" + name + "' found"); 
        }
        /// <summary>
        /// Retrieve array by index
        /// </summary>
        /// <typeparam name="T">Expected type of the array</typeparam>
        /// <param name="index">Index of the array</param>
        /// <returns>A clone of the array found or null, if no array at the given index exists</returns>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public Array<T> GetArray<T>(int index) {
            using (Scope.Enter()) {
                if (index >= m_data.S[0])
                    throw new ArgumentException($"The index of the array to retrieve must lay in the range of existing arrays: 0..{m_data.S[0] - 1}].");
                return m_data.GetArray<T>(index, 0);
            }
        }
        /// <summary>
        /// Write this mat file into a binary stream
        /// </summary>
        /// <param name="stream">Stream to receive data. This will commonly be a FileStream object.</param>
        /// <remarks>
        /// <para>This method writes the full content of the current mat file into a binary stream. The file 
        /// afterwards is suitable to be read again by ILNumerics.MatFile classes or by compatible *.mat file 
        /// readers - including Matlab, e.g.</para>
        /// <example><code>
        /// MatFile m = new MatFile(myarrays); 
        /// using (Stream s = new FileStream("test.mat",FileMode.Create)) {
        ///     m.Write(s);
        /// }
        /// </code></example></remarks>
        public void Write(Stream stream) {
            using (BinaryWriter fileout = new BinaryWriter(stream)) {
                write(fileout);
            }
        }
        /// <summary>
        /// Write all arrays to *.mat file
        /// </summary>
        /// <param name="filename">Filename of the file to write the mat file to</param>
        /// <remarks>
        /// <para>The method writes the full content of the matfile to the file specified. If the filename 
        /// points to a file which already exists, that file will be overwritten. Otherwise a new file will
        /// be created. </para>
        /// <para>The file will be suitable for reading by ILNumerics.MatFile classes or by compatible *.mat file 
        /// readers - including e.g. matlab</para>
        /// </remarks>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public void Write(string filename) {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate)) {
                Write(fs);
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Create MatFile object from existing mat file 
        /// </summary>
        /// <param name="file2open">Path to Matlab mat file to open</param>
        /// <remarks>Curently mat files up to Matlab version 6.5 are supported. Compressed mat file content is not supported yet.</remarks>
        /// <example><code><![CDATA[
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile()) {
        ///     mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
        /// Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// 
        /// // reading back using MatFile
        /// using (var back = new MatFile("file.mat")) {
        ///     Array<sbyte> B = back.GetArray<sbyte>("myArray");
        /// 
        ///     // ... or usign cell methods: 
        ///     Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);
        /// 
        ///     Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        ///     Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
        /// }
        /// ]]></code></example>
        public MatFile(string file2open) {

            using (Scope.Enter()) {
                m_filename = file2open;
                using (FileStream fs = File.OpenRead(file2open)) {
                    BinaryReader br = new BinaryReader(fs);
                    read_header(br);
                    // read elements 
                    Cell targetCell = cell(size(0, 2), null);
                    while (br.BaseStream.Position < br.BaseStream.Length - 7) {
                        MatFileType dataType = (MatFileType)Enum.Parse(typeof(MatFileType), br.ReadInt32().ToString());
                        // the length of this chunk may be used for error checking, but....
                        int len = br.ReadInt32();
                        switch (dataType) {
                            //case MatFileType.miCOMPRESSED:
                            //    BaseArray mat = read_compressed(br, len);
                            //    targetCell.SetValue(mat.Name, targetCell.S[0], 1); 
                            //    targetCell.SetValue(mat, targetCell.S[0] - 1, 0);
                            //    break;
                            case MatFileType.miMATRIX:
                                string name; 
                                BaseArray mat = read_miMATRIX(br, out name);
                                targetCell.SetValue(name, targetCell.S[0], 1);  // expand: add new row
                                targetCell.SetValue(mat, targetCell.S[0] - 1, 0);
                                break;
                            default:
                                // ignore all other elements, not supported yet
                                break;
                        }
                    }
                    m_data.a = targetCell;
                    br.Close();
                }
            }
        }
        /// <summary>
        /// Create MatFile object from multiple arrays of arbitrary types. Automatically choose array names for saving.
        /// </summary>
        /// <param name="input">Cell array holding the arrays to be added to the <see cref="MatFile"/>. The size of the cell is neglected. Elements are read in column major order.</param>
        /// <exception cref="ArgumentException">If <paramref name="input"/> or any element in <paramref name="input"/>
        /// is or points to null.</exception>
        /// <example><code><![CDATA[
        /// Array<sbyte> A = counter<sbyte>(-10, 2, 4, 8, 13);
        /// Array<double> B = ones<double>(199, 200);
        /// 
        /// // MatFile should be used in an 'using' block, 
        /// // cleaning up its resources automatically.
        /// using (MatFile mat = new MatFile(cellv(A, B))) {
        ///     mat.Write("file.mat");
        /// }
        /// 
        /// // reading back using ILMath.loadArray<T>(...)
        /// Array<sbyte> C = loadArray<sbyte>("file.mat", "Array0"); // names are automatic
        /// Array<double> D = loadArray<double>("file.mat", "Array1");
        /// Assert.IsTrue(C.Equals(A));
        /// Assert.IsTrue(D.Equals(B));
        /// ]]></code></example>
        /// <exception cref="ArgumentNullException">If input array was null or one of the arrays in the input arrays was null.</exception>
        /// <seealso cref="Keys"/>
        /// <seealso cref="AddArray{T}(BaseArray{T}, string)"/>
        public MatFile(InCell input) {
            if (object.Equals(input,null))
                throw new ArgumentNullException ("Input arrays for the MatFile may not be null!");
            //m_data = new Cell(input.Length,2);
            using (Scope.Enter(input, ArrayStyles.ILNumericsV4)) {
                for (uint i = 0; i < input.Length; i++) {
                    if (isnull(input.GetValue(i))) {
                        throw new ArgumentNullException($"The input array at index {i} was null.");
                    }
                    this["Array" + i.ToString()] = input.GetValue(i);
                }
            }
        }
        /// <summary>
        /// Create an empty MatFile object. To be used to add arrays later on.
        /// </summary>
        /// <seealso cref="MatFile.MatFile(InCell)"/>
            /// <seealso cref="AddArray{T}(BaseArray{T}, string)"/>
        public MatFile() {
            m_data = cell();
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose all arrays of the matfile object
        /// </summary>
        /// <remarks>Calling dispose should be the last action for a matfile object. It is recommended to 
        /// utilize the matfile class in using blocks (C# / Visual Basic).</remarks>
        public void Dispose() {
            if (!object.Equals(m_data,null)) 
                m_data.Dispose(); 
        }

        #endregion
    }
}