using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Global {
    public struct Tuple<T1, T2> {
        public T1 Item1;
        public T2 Item2;
        public static Tuple<Item1T, Item2T> Create<Item1T, Item2T>(Item1T item1, Item2T item2) {
            return new Tuple<Item1T, Item2T>() {
                Item1 = item1,
                Item2 = item2
            };
        }
    }
    public struct Tuple<T1, T2, T3> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public static Tuple<Item1T, Item2T, Item3T> Create<Item1T, Item2T, Item3T>(Item1T item1, Item2T item2, Item3T item3) {
            return new Tuple<Item1T, Item2T, Item3T>() {
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
            };
        }
    }
    public struct Tuple<T1, T2, T3, T4> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public static Tuple<Item1T, Item2T, Item3T, Item4T> Create<Item1T, Item2T, Item3T, Item4T>(Item1T item1, Item2T item2, Item3T item3, Item4T item4) {
            return new Tuple<Item1T, Item2T, Item3T, Item4T>() {
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
                Item4 = item4,
            };
        }
    }
    public struct Tuple<T1, T2, T3, T4, T5> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public static Tuple<Item1T, Item2T, Item3T, Item4T, Item5T> Create<Item1T, Item2T, Item3T, Item4T, Item5T>(
                    Item1T item1, Item2T item2, Item3T item3, Item4T item4, Item5T item5) {
            return new Tuple<Item1T, Item2T, Item3T, Item4T, Item5T>() {
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
                Item4 = item4,
                Item5 = item5,
            };
        }
    }
    public struct Tuple<T1, T2, T3, T4, T5, T6> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public static Tuple<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T> Create<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T>(
                    Item1T item1, Item2T item2, Item3T item3, Item4T item4, Item5T item5, Item6T item6) {
            return new Tuple<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T>() {
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
                Item4 = item4,
                Item5 = item5,
                Item6 = item6,
            };
        }
    }
    public struct Tuple<T1, T2, T3, T4, T5, T6, T7> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public T7 Item7;
        public static Tuple<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T, Item7T> Create<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T, Item7T>(
                    Item1T item1, Item2T item2, Item3T item3, Item4T item4, Item5T item5, Item6T item6, Item7T item7) {
            return new Tuple<Item1T, Item2T, Item3T, Item4T, Item5T, Item6T, Item7T>() {
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
                Item4 = item4,
                Item5 = item5,
                Item6 = item6,
                Item7 = item7,
            };
        }
    }
}

