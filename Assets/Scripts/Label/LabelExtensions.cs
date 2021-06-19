using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LabelExtensions
{
    public static List<string> GetLabels(this GameObject go)
    {
        return go.GetOrAddComponent<Labels>().labels;
    }

    public static void AddLabel(this GameObject go, string s)
    {
        go.GetOrAddComponent<Labels>().labels.Add(s);
    }
}
