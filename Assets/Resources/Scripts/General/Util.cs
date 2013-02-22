using UnityEngine;
using System.Collections;

public static class Util {
    public static bool InRange<T>(this T value, T from, T to) where T : System.IComparable<T> {
        return value.CompareTo(from) >= 1 && value.CompareTo(to) <= -1;
    }
}
