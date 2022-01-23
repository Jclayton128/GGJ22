using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListUtility
{

    /// <summary>
    /// Performs in-place Fischer-Yates shuffle on a list.
    /// </summary>
    public static void Shuffle<T>(List<T> list)
    {
        if (list == null) return;
        for (int i = 0; i < list.Count - 1; i++)
        {
            int j = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
