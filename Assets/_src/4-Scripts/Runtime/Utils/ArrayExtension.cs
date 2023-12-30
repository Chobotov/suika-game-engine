using System.Collections.Generic;

namespace TapSwap.Utils
{
    public static class ArrayExtension
    {
        public static T GetRandomItem<T>(this T[] arr)
        {
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }

        public static T GetRandomItem<T>(this IReadOnlyList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}