using ILNumerics.Core.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ILNumerics.Core.Global {

    /// <summary>
    /// Simple and efficient, threadsafe cache for storing and retrieving items from memory.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    internal class InMemoryCache<T> where T: class, ICacheable<T>, new() {

        /*  In order to cache objects T with this class, one must ... 
         *  + add and implement the ICacheable<T> interface to the object class (requires a single instance property 'Previous').
         *  + use the InMemoryCache<MyT>.Retrieve() and InMemoryCache<MyT>.Store(this) functions to get / put items from/into the cache. 
         *  
         *  Note: this cache is threadsafe ! It prevents from multiple stores of the same object (to the price of small overhead during stores).
         */

        private static readonly T s_start; 
        static T s_cache;
        static InMemoryCache() {
            s_start = new T();
            s_cache = s_start; 
        }
        //internal static void Store(T instance) {
        //    if (object.Equals(instance, null)) return;

        //    // Below implementation requires 3 CompExchange calls, because it allows concurrent 
        //    // access to both: the s_cache and to instance. Latter may not be needed, though. 
        //    // TODO: check and replace 1. and 3. CompExchangw with simple stores, if appropriate. 

        //    // thread safe singly linked list: add instance
        //    do {
        //        var last = s_cache;
        //        // prevent from multiple stores of instance: 
        //        if (Interlocked.CompareExchange(ref instance.Previous, last, null) != null) {
        //            break; // already stored 
        //        }
        //        if (Interlocked.CompareExchange(ref s_cache, instance, last) == last) {
        //            // commonly exits here 
        //            break;
        //        }
        //        // try again. Reset 'previous' back to null:
        //        if (Interlocked.CompareExchange(ref instance.Previous, null, last) != last) {
        //            break; // give up
        //        }
        //    } while (true);
        //}
        internal static void Store(T instance) {
            if (object.Equals(instance, null)) return;

            // thread safe singly linked list: add instance
            lock (s_start) {
                
                if (instance.Previous != null) return; // already stored

                // prevent from multiple stores of instance: 
                instance.Previous = s_cache; 

                s_cache = instance; 
            }
        }

        //internal static T Retrieve() {
        //    // thread safe, non-blocking linked list: Remove
        //    while (true) {
        //        T item = s_cache;
        //        if (object.ReferenceEquals(item, s_start)) {
        //            return new T();
        //        }
        //        if (Interlocked.CompareExchange(ref item.DeletionMark, 1, 0) == 0) {

        //            if (Interlocked.CompareExchange(ref s_cache, item.Previous, item) == item) {
        //                item.Previous = null;
        //                item.DeletionMark = 0;
        //                return item;
        //            }
        //            item.DeletionMark = 0;
        //        }
        //    }
        //}
        internal static T Retrieve() {

            lock (s_start) {
                T item = s_cache;
                if (!object.ReferenceEquals(item, s_start)) {
                    s_cache = item.Previous;
                    item.Previous = null;
                    return item;
                }
            }
            return new T(); // may throw OOM! 
            // Edit: don't recover here (see attempt below)! If the system is not able to create a small object like this an OOM is the right answer.
            //try {
            //} catch (OutOfMemoryException) {
            //    // this is not meant to be robust nor fail safe! 
            //    // All we want is to give the main thread a 2nd chance and wait for a segment to finish, then try again.
            //    if (Device.Wait4AwaiterCompletion()) {
            //        return new T(); 
            //    }
            //    throw; 
            //}
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DebuggerHidden]
        internal static IEnumerable<T> InstancesUnsafe {
            get {
                var item = s_cache;
                while (item != null) {
                    yield return item;
                    item = item.Previous;
                }
            }
        }
    }
}
