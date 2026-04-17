//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    public unsafe abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region DimSpec - WriteTo_np_ IterIter + _BSDIter implementations
        
        internal StorageT SetRangeDimSpec_np(StorageT value, long* outbsd) {
            if (object.Equals(value, null)) {
                throw new ArgumentException("'null' is not a valid assignment value for SetRange in numpy array mode. Did you mean to perform removal as in Settings.ArrayStyle=ILNumericsV4?");
            }
            var isFullAssign = m_size.NumberOfElements == outbsd[1] && m_size.NumberOfElements == value.m_size.NumberOfElements;
            // ensure is writable
            var storage = this; 
            if (m_handles.ReferenceCount > 1 && !isFullAssign) {
                storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                // correct the target outbsd for changed base offset (was consolidated in GetDetached())
                outbsd[2] -= m_size.BaseOffset;
                System.Diagnostics.Debug.Assert(outbsd[2] >= 0,$"Invalid base offset: {outbsd[2]} after detaching shared source buffer in {nameof(SetRangeDimSpec_np)}. Expected value: >= 0."); 
                // ho: Don't Assign() here, yet! the new storage is the only one created for this array's method. It will be further configured 
                // and eventually Assign()-ed on the array layer afterwards. 
                //if (!ReferenceEquals(newStor, storage)) {
                //    storage.Assign(newStor, toOutT: true, fromRetT: true, renameInT: false);
                //    storage = newStor;
                //}
            }
            if (isFullAssign) {
                // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                // means: same shape.
                // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                value.m_size.CheckIsBroadcastableTo_np(outbsd + 3, (uint)outbsd[0]);
                return storage.SetFullOptim(value);

            } else {
                // broadcasting assignment, includes the "all elements provided, same shape" case.

                // Checks for broadcasting shape is performed within WriteTo_...

                Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.m_size, storage.m_handles[0], outbsd, SizeOfT);
                return storage as StorageT; 
            }
        }

        
        internal StorageT WriteTo_np(int nIters, Iterators.MultidimIterator* iterators,
                        int firstIDXArrayPos, long nrIdxOutDims, long* idxOutDims, long** buffer,
                        StorageT rightSide) {

            try {
                if (object.Equals(rightSide, null)) {
                    throw new ArgumentException("The source value for subarray assignment cannot be null in numpy array style.");
                }
                uint nMissingDims = (uint)Math.Max((int)m_size.NumberOfDimensions - nIters, 0);
                // reorder iterators according to (broadcasted, merged) idx dimensions
                // compute output shape 
                long outBaseOffset = 0;
                uint nOutDims = 0;

                var outDims = *buffer;
                *buffer += Size.MaxNumberOfDimensions; // outdims

                findOutDimensionOrder(iterators, ref nIters, ref nOutDims, outDims, firstIDXArrayPos,
                                    nrIdxOutDims, idxOutDims, ref outBaseOffset, nMissingDims, buffer);

                var storage = this; 
                if (m_handles.ReferenceCount > 1) {

                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                    // Note: we do not need to adopt base offset here. It was consolidated away in GetDetached() but
                    // WriteTo below acquires the new value from the new storage.

                    // ho: Don't Assign() here, yet! the new storage is the only one created for this array's method. It will be further configured 
                    // and eventually Assign()-ed on the array layer afterwards. 
                    //if (!ReferenceEquals(newStor, storage)) {
                    //    storage.Assign(newStor, toOutT: true, fromRetT: true, renameInT: false);
                    //    storage = newStor;
                    //}
                }

                // WriteTo...
                if (!(ElementInstance is System.ValueType)) {
                    WriteTo_T_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                } else {
                    switch (SizeOfT) {
                        case 1:
                            WriteTo_1_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                            break;
                        case 2:
                            WriteTo_2_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                            break;
                        case 4:
                            WriteTo_4_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                            break;
                        case 8:
                            WriteTo_8_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                            break;
                        case 16:
                            WriteTo_16_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer);
                            break;
                        default:
                            WriteTo_Arbitrary_np(rightSide, storage as StorageT, iterators, outDims, (uint)nIters, nOutDims, outBaseOffset, buffer, SizeOfT);
                            break;
                            //throw new InvalidProgramException($"Unsupported element type: {typeof(T).Name}");
                    }
                }
                return storage as StorageT; 
            } catch (ArgumentException exc) {
                throw new ArgumentException("Unable to assign the right side array to the target range. The inner exception may contain more details.", exc);
            }

        }
        #endregion

        #region WriteTo_?_np

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                WriteTo_2
            </source>
            <destination>WriteTo_1</destination>
            <destination>WriteTo_4</destination>
            <destination>WriteTo_8</destination>
            <destination>WriteTo_16</destination>
        </type>
        <type>
            <source locate="here">
                ushort
            </source>
            <destination>byte</destination>
            <destination>uint</destination>
            <destination>ulong</destination>
            <destination>complex</destination>
        </type>
        <type>
            <source locate="here">
                UInt16
            </source>
            <destination>Byte</destination>
            <destination>UInt32</destination>
            <destination>UInt64</destination>
            <destination>Complex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        
        internal static void WriteTo_2_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            ushort* pOut = (ushort*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset + outBaseOffset;
            ushort* pIn = (ushort*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                pOut[0] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        
        internal static void WriteTo_16_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            complex* pOut = (complex*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset + outBaseOffset;
            complex* pIn = (complex*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                pOut[0] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        
        internal static void WriteTo_8_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            ulong* pOut = (ulong*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset + outBaseOffset;
            ulong* pIn = (ulong*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                pOut[0] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        
        internal static void WriteTo_4_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            uint* pOut = (uint*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset + outBaseOffset;
            uint* pIn = (uint*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                pOut[0] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        
        internal static void WriteTo_1_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset + outBaseOffset;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                pOut[0] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        
        private void WriteTo_np_IterIter(StorageT value, IIndexIterator[] iterators, uint ndimsOut) {

            if (m_handles[0] is ManagedHostHandle<T>) {
                WriteTo_np_IterIter_T(value, iterators, ndimsOut);
            } else {
                switch (SizeOfT) {
                    case 1:
                        WriteTo_np_IterIter_1(value, iterators, ndimsOut);
                        break;
                    case 2:
                        WriteTo_np_IterIter_2(value, iterators, ndimsOut);
                        break;
                    case 4:
                        WriteTo_np_IterIter_4(value, iterators, ndimsOut);
                        break;
                    case 8:
                        WriteTo_np_IterIter_8(value, iterators, ndimsOut);
                        break;
                    case 16:
                        WriteTo_np_IterIter_16(value, iterators, ndimsOut);
                        break;
                    default:
                        WriteTo_np_IterIter_Arbitrary(value, iterators, ndimsOut, SizeOfT);
                        break;
                        //throw new NotSupportedException($"The element type {typeof(T)} is not supported in this context. Supported element types: value types (structs) of length 1,2,4,8 or 16 bytes and reference types.");
                }
            }

        }
        
        internal unsafe void WriteTo_np_BSDIter(StorageT value, long* outBSD) {

            if (m_handles[0] is ManagedHostHandle<T>) {
                WriteTo_np_BSDIter_T(value, outBSD);
            } else {
                switch (SizeOfT) {
                    case 1:
                        WriteTo_np_BSDIter_1(value, outBSD);
                        break;
                    case 2:
                        WriteTo_np_BSDIter_2(value, outBSD);
                        break;
                    case 4:
                        WriteTo_np_BSDIter_4(value, outBSD);
                        break;
                    case 8:
                        WriteTo_np_BSDIter_8(value, outBSD);
                        break;
                    case 16:
                        WriteTo_np_BSDIter_16(value, outBSD);
                        break;
                    default:
                        WriteTo_np_BSDIter_Arbitrary(value, outBSD, SizeOfT);
                        break;
                        //throw new NotSupportedException($"The element type {typeof(T)} is not supported in this context. Supported element types: value types (structs) of length 1,2,4,8 or 16 bytes and reference types.");
                }
            }

        }

        #region HYCALPER LOOPSTART WriteTo_IterIter_'s

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                WriteTo_np_IterIter_2
            </source>
            <destination>WriteTo_np_IterIter_1</destination>
            <destination>WriteTo_np_IterIter_4</destination>
            <destination>WriteTo_np_IterIter_8</destination>
            <destination>WriteTo_np_IterIter_16</destination>
        </type>
        <type>
            <source locate="here">
                WriteTo_np_BSDIter_2
            </source>
            <destination>WriteTo_np_BSDIter_1</destination>
            <destination>WriteTo_np_BSDIter_4</destination>
            <destination>WriteTo_np_BSDIter_8</destination>
            <destination>WriteTo_np_BSDIter_16</destination>
        </type>
        <type>
            <source locate="here">
                ushort
            </source>
            <destination>byte</destination>
            <destination>uint</destination>
            <destination>ulong</destination>
            <destination>complex</destination>
        </type>
        <type>
            <source locate="here">
                UInt16
            </source>
            <destination>Byte</destination>
            <destination>UInt32</destination>
            <destination>UInt64</destination>
            <destination>Complex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_np_IterIter_2(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ushort* pOut = (ushort*)m_handles[0].Pointer;
            ushort* pIn = (ushort*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_2(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ushort* pOut = (ushort*)m_handles[0].Pointer;
            ushort* pIn = (ushort*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_np_IterIter_16(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            complex* pOut = (complex*)m_handles[0].Pointer;
            complex* pIn = (complex*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_16(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            complex* pOut = (complex*)m_handles[0].Pointer;
            complex* pIn = (complex*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_np_IterIter_8(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ulong* pOut = (ulong*)m_handles[0].Pointer;
            ulong* pIn = (ulong*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_8(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ulong* pOut = (ulong*)m_handles[0].Pointer;
            ulong* pIn = (ulong*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_np_IterIter_4(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            uint* pOut = (uint*)m_handles[0].Pointer;
            uint* pIn = (uint*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_4(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            uint* pOut = (uint*)m_handles[0].Pointer;
            uint* pIn = (uint*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_np_IterIter_1(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_1(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        /// <param name="elementSize">Single element size in bytes.</param>
        
        private unsafe void WriteTo_np_BSDIter_Arbitrary(StorageT value, long* outBSD, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    for (int i = 0; i < elementSize; i++) {
                        pOut[(higdims + cur0 * stride0) * elementSize + i] = pIn[valueIter.Current * elementSize + i];
                    }
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }


        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        /// <param name="elementSize">Single element size in bytes.</param>
        
        private unsafe void WriteTo_np_IterIter_Arbitrary(StorageT value, IIndexIterator[] outIterators, uint ndimsOut, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    for (int i = 0; i < elementSize; i++) {
                        pOut[(higdims + it0.Current * stride0) * elementSize + i] = pIn[valueIter.Current * elementSize + i];
                    }
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }


        
        private unsafe void WriteTo_np_IterIter_T(StorageT value, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var valueIter = value.GetEnumerator(storageOrder: StorageOrders.ColumnMajor, dispose: false);
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType && SizeOfT == IntPtr.Size);
            System.Diagnostics.Debug.Assert(m_handles[0] is ManagedHostHandle<T>);


            T[] pOut = (m_handles[0] as ManagedHostHandle<T>).HostArray;

            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = valueIter.Current;
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        if (++d == ndimsOut) return;
                    }
                }
            }
        }
        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_np_BSDIter_T(StorageT value, long* outBSD) {

            // This handles empty storages! 

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType);

            T[] pOut = (m_handles[0] as ManagedHostHandle<T>).HostArray;
            T[] pIn = (value.m_handles[0] as ManagedHostHandle<T>).HostArray;
            long stride0 = outBSD[3 + ndims];
            long dinpen0 = outBSD[3] * stride0;
            long* cur = stackalloc long[(int)ndims];
            while (true) {
                long cur0 = 0;
                while (cur0 < dinpen0) {
                    pOut[higdims + cur0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0 *= stride0;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="outDims"></param>
        /// <param name="buffer"></param>
        /// <param name="nOutDims"></param>
        /// <param name="outBaseOffset"></param>
        /// <param name="elementSize">Single element size in bytes.</param>
        
        internal static void WriteTo_Arbitrary_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer, uint elementSize) {

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + (destStorage.m_size.BaseOffset + outBaseOffset) * elementSize;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                for (int j = 0; j < elementSize; j++) {
                    pOut[j] = pIn[srcStorage.m_size.BaseOffset * elementSize + j];
                }
                return;
            }

            // performs check if broadcastable 
            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                // nothing to do. Empty range defined.
                // error checking was performed in iterator creation already
                return;
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;

            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    long nextInIdx = srcIt.GetNext();
                    for (int j = 0; j < elementSize; j++) {
                        pOut[idx * elementSize + j] = pIn[nextInIdx * elementSize + j];
                    }

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }


        
        internal static void WriteTo_T_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, long* outDims, uint nIter, uint nOutDims,
                                long outBaseOffset, long** buffer) {

            // This handles empty storages! (by handling empty iterators)
            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType);
            T[] outA = (destStorage.m_handles[0] as MemoryLayer.ManagedHostHandle<T>).HostArray;
            T[] pIn = (srcStorage.m_handles[0] as MemoryLayer.ManagedHostHandle<T>).HostArray;

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 
            long outP = destStorage.m_size.BaseOffset + outBaseOffset;

            if (nIter == 0) {  // all indices are scalars
                srcStorage.m_size.CheckIsBroadcastableTo_np(outDims, nOutDims);
                outA[outP] = pIn[srcStorage.m_size.BaseOffset];
                return;
            }

            var srcIt = new Iterators.BroadcastingSizeRowMajorIterator(srcStorage.m_size, outDims, nOutDims, buffer);
            // if the iterator could be created we will not check for all increments to be successfull anymore! 

            long higdims = outBaseOffset;
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            uint i = 0;

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty range.
            }

            i += setCount0;
            // highdims up from 2nd "dimension" /or after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    outA[idx] = pIn[srcIt.GetNext()];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = srcStorage.m_size.BaseOffset + outBaseOffset;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }
        #endregion

    }
}
