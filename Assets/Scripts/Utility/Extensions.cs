using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T Any<T>(this List<T> data)
    {
        return data[Random.Range(0, data.Count)];
    }
    public static T Any<T>(this T[] data)
    {
        return data[Random.Range(0, data.Length)];
    }


    public static Vector3 Pos(this GameObject g)
    {
        return g.transform.position;
    }
    public static Vector3 Pos(this GameObject g, Vector3 pos)
    {
        return g.transform.position = pos;
    }
    public static Vector3 Facing(this GameObject g)
    {
        return g.transform.forward;
    }
    public static Vector3 Facing(this GameObject g, Vector3 lookDir)
    {
        return g.transform.forward = lookDir;
    }


    public static void KillChildren(this Transform t)
    {
        // for (int i = 0; i < t.childCount; i++)
        // {
        //     try
        //     {
        //         GameObject.DestroyImmediate(t.GetChild(i).gameObject);

        //     }
        //     catch (System.Exception)
        //     {
        //         GameObject.Destroy(t.GetChild(i).gameObject);

        //     }

        // }
    }
}
