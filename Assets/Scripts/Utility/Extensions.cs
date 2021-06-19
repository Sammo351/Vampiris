using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class Extensions
{

    public static T Any<T>(this List<T> data)
    {
        return data[Random.Range(0, data.Count)];
    }
    public static List<T> Any<T>(this List<T> data, int amount = 2)
    {
        return data.OrderBy(x => Random.Range(0, 1f)).Take(5).ToList();
    }

    public static List<T> AnyDifferent<T>(this List<T> data, int amount = 2)
    {
        if (amount > data.Count) { return data; }
        T[] copyArray = new T[data.Count];
        List<T> copy;
        data.CopyTo(copyArray);
        copy = copyArray.ToList();
        List<T> ret = new List<T>();
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, copy.Count);
            T item = copy[index];
            ret.Add(item);
            copy.RemoveAt(index);
        }

        return ret;
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
    /// <summary>
    ///  Returns random value between Vec.x and Vec.y
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static float Next(this Vector2 vec)
    {
        return Random.Range(vec.x, vec.y);
    }


    public static Vector3 SetX(this Vector3 vec, float x)
    {
        return new Vector3(x, vec.y, vec.z);
    }
    public static Vector3 SetY(this Vector3 vec, float y)
    {
        return new Vector3(vec.x, y, vec.z);
    }
    public static Vector3 SetZ(this Vector3 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    public static Vector3 AdjustX(this Vector3 vec, float a)
    {
        return vec.SetX(vec.x + a);
    }
    public static Vector3 AdjustY(this Vector3 vec, float a)
    {
        return vec.SetY(vec.y + a);
    }
    public static Vector3 AdjustZ(this Vector3 vec, float a)
    {
        return vec.SetZ(vec.z + a);
    }


    public static void KillChildren(this GameObject gameObject, bool immediate = false)
    {
        int count = gameObject.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (immediate) { GameObject.DestroyImmediate(child.gameObject); }
            else { GameObject.Destroy(child.gameObject); }
        }
    }
    /// <summary>
    /// Returns all children and their children...and their children......and their children...im bored, you get the point
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static List<GameObject> GetDescendants(this GameObject gameObject, List<GameObject> list = null)
    {
        if (list == null) { list = new List<GameObject>(); }
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            list.Add(child);
            GetDescendants(child, list);
        }

        return list;
    }
    public static List<GameObject> GetDescendantsByName(this GameObject gameObject, string name)
    {
        return gameObject.GetDescendants().Where(x => x.name.ToLower() == name.ToLower()).ToList();
    }
    public static List<GameObject> GetChildren(this GameObject gameObject)
    {
        List<GameObject> list = new List<GameObject>();
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            list.Add(child);

        }

        return list;
    }

    public static T GetOrAddComponent<T>(this GameObject game) where T : Component
    {
        if (game.GetComponent<T>() != null)
            return game.GetComponent<T>();

        return game.AddComponent<T>();
        

    }

}
