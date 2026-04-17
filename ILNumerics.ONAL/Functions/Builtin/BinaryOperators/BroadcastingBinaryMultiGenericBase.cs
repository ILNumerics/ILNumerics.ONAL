//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using static ILNumerics.Core.StorageLayer.Iterators;

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART 
    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            BroadcastingBinaryMultiGenericBase_C_CS
        </source>
        <destination>BroadcastingBinaryMultiGenericBase_C_CC</destination>
        <destination>BroadcastingBinaryMultiGenericBase_C_SC</destination>
        <destination>BroadcastingBinaryMultiGenericBase_C_SS</destination>
        <destination>BroadcastingBinaryMultiGenericBase_S_CC</destination>
        <destination>BroadcastingBinaryMultiGenericBase_S_CS</destination>
        <destination>BroadcastingBinaryMultiGenericBase_S_SC</destination>
        <destination>BroadcastingBinaryMultiGenericBase_S_SS</destination>
   </type>
    <type>
        <source locate="nextline">
            HC_TOUT_CLASS
        </source>
        <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
   </type>
    <type>
        <source locate="nextline">
            HC_TIN1_CLASS
        </source>
        <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
   </type>
    <type>
        <source locate="nextline">
            HC_TIN2_CLASS
        </source>
        <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
        <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
   </type>
    </hycalper>
    */

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_C_CS
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_S_SS
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if !TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if !TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if !TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_S_SC
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if !TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if !TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_S_CS
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if !TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if !TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_S_CC
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if !TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_C_SS
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if !TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if !TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_C_SC
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if !TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }
   

    /// <summary>
    /// <![CDATA[Base class for 'apply<,,>()' type operators.Implements all permutations for class/struct element types.]]>
    /// </summary>
    /// <typeparam name="Tin1"></typeparam>
    /// <typeparam name="LocalTin1"></typeparam>
    /// <typeparam name="InTin1"></typeparam>
    /// <typeparam name="OutTin1"></typeparam>
    /// <typeparam name="RetTin1"></typeparam>
    /// <typeparam name="StorageTin1"></typeparam>
    /// <typeparam name="Tin2"></typeparam>
    /// <typeparam name="LocalTin2"></typeparam>
    /// <typeparam name="InTin2"></typeparam>
    /// <typeparam name="OutTin2"></typeparam>
    /// <typeparam name="RetTin2"></typeparam>
    /// <typeparam name="StorageTin2"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    /// <typeparam name="LocalTout"></typeparam>
    /// <typeparam name="InTout"></typeparam>
    /// <typeparam name="OutTout"></typeparam>
    /// <typeparam name="RetTout"></typeparam>
    /// <typeparam name="StorageTout"></typeparam>
    /// <typeparam name="DelegateT"></typeparam>
    internal abstract unsafe partial class BroadcastingBinaryMultiGenericBase_C_CC
        <
        Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1,
        Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2,
        Tout, LocalTout, InTout, OutTout, RetTout, StorageTout,
        DelegateT
        >

        where StorageTin1 : BaseStorage<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>, new()
        where LocalTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where InTin1 : Immutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where OutTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>
        where RetTin1 : Mutable<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1>

        where StorageTin2 : BaseStorage<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>, new()
        where LocalTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where InTin2 : Immutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where OutTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>
        where RetTin2 : Mutable<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2>

        where StorageTout : BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>, new()
        where LocalTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where InTout : Immutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where OutTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>
        where RetTout : Mutable<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout> {
        
        internal abstract void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, DelegateT function);

        
        internal RetTout operate(
            ConcreteArray<Tin1, LocalTin1, InTin1, OutTin1, RetTin1, StorageTin1> A,
            ConcreteArray<Tin2, LocalTin2, InTin2, OutTin2, RetTin2, StorageTin2> B,
            DelegateT func) {

            // Do not release A or B! Both may be RetTs!
            System.Diagnostics.Debug.Assert(!ReferenceEquals(A?.Storage, B?.Storage) || (A is RetTin1 == false || B is RetTin2 == false)); // should never happen, if ILN func rules are respected. (But may happen with BaseArray<T> as input parameter.)

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");


            var buffer = Storage<Tin1>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageTout ret;
            if (A is RetTout && storageA.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageA)) {
                ret = storageA as StorageTout;
            } else if (B is RetTout && storageB.Size.NumberOfElements == buffer[1] && Helper.canBeUsedForInplaceOp(storageB)) {
                ret = storageB as StorageTout;
            } else {
                ret = BaseStorage<Tout, LocalTout, InTout, OutTout, RetTout, StorageTout>.Create();
                ret.Handles[0] = MathInternal.New<Tout>((ulong)buffer[1], 0, false);
                // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
                var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA.S : storageB.S;
                if (larger.IsContinuous) {
                    ret.S.SetDimensionLengths(buffer, larger.StorageOrder);
                } else {
                    ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
                }
            }
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<Tin1, Tin2, Tout>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            #if TOUT_IS_CLASS
                byte* pOut = (byte*)ret.m_handles[0].Pointer;
                long offOut = ret.Size.BaseOffset * Storage<Tout>.SizeOfT;
#else
            Tout[] pOut = (ret.Handles[0] as ManagedHostHandle<Tout>).HostArray;
            long offOut = ret.Size.BaseOffset;
#endif
            #if TIN1_IS_CLASS
                byte* pIn1 = (byte*)storageA.m_handles[0].Pointer;
                long offIn1 = storageA.Size.BaseOffset * Storage<Tin1>.SizeOfT;
#else
            Tin1[] pIn1 = (storageA.Handles[0] as ManagedHostHandle<Tin1>).HostArray;
            long offIn1 = storageA.Size.BaseOffset;
#endif
            #if TIN2_IS_CLASS
            byte* pIn2 = (byte*)storageB.Handles[0].Pointer;
            long offIn2 = storageB.Size.BaseOffset * Storage<Tin2>.SizeOfT;
#else
                Tin2[] pIn2 = (storageB.m_handles[0] as ManagedHostHandle<Tin2>).HostArray;
                long offIn2 = storageB.Size.BaseOffset;
#endif

            Strided64(pOut, pIn1, pIn2, offOut, offIn1, offIn2,
                0, buffer[1], buffer, func);

            return ret.RetArray;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}