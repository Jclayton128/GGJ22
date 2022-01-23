using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a shuffled list of strings. GetRandomString() returns a string.
/// </summary>
public class StringLibrary
{

    private string name;
    private string fallbackValue;
    private List<string> list = null;
    private int next = 0;

    public StringLibrary(TextAsset textAsset, string fallbackValue)
    {
        this.fallbackValue = fallbackValue;
        if (textAsset == null)
        {
            Debug.LogError($"{GetType().Name} constructor didn't receive a TextAsset.");
            return;
        }
        name = textAsset.name;
        list = new List<string>(textAsset.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        ShuffleList();
    }

    public string GetRandomString()
    {
        if (list != null)
        {
            if (next >= list.Count)
            {
                ShuffleList();
            }
            if (0 <= next && next < list.Count)
            {
                return list[next++];
            }
        }
        Debug.LogError($"Failed to get random string from StringLibrary {name}");
        return fallbackValue;
    }

    private void ShuffleList()
    {
        if (list == null) return;
        ListUtility.Shuffle(list);
        next = 0;
    }
}
