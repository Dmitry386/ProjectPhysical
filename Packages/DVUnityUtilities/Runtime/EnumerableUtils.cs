using System;
using System.Collections.Generic;
using System.Linq;

namespace DVUnityUtilities
{
    public static class EnumerableUtils
    {
        public static bool IsHaveElement<T>(this IEnumerable<T> arr, int id, out T element)
        {
            element = default;
            if (!arr.IsValidIndex(id)) return false;

            element = arr.ElementAt(id);
            return true;
        }

        public static bool IsValidIndex<T>(this IEnumerable<T> arr, int id)
        {
            if (arr == null) return false;
            return id >= 0 && id < arr.Count();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> arr)
        {
            if (arr == null) return true;
            return arr.Count() == 0;
        }

        public static bool IsLastOrMore<T>(IEnumerable<T> arr, int id)
        {
            return id >= arr.Count() - 1;
        }

        public static void Add<T>(ref T[] array, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }
    }
}