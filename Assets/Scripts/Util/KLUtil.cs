using UnityEngine;
using System.Collections;
using System.Linq;

public static class KLUtil {

    public static bool IsNullOrEmpty<T>(T[] array) where T : class
    {
        if (array == null || array.Length == 0)
            return true;
        else
            return array.All(item => item == null);
    }

    public static ArrayList Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return new ArrayList(array);
    }
}
