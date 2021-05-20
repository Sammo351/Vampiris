using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//V for Vampire Variables and Vunctions
public static class V
{
    /// <summary>
    /// Returns 1 or -1.
    /// </summary>
    public static int RandomSign()
    {
        return Random.Range(0, 2) * 2 - 1;
    }

    /// <summary>
    /// Returns true or false on whether a random number is Less than F.
    /// 0 = no chance, 1 = always
    /// </summary>
    public static bool Chance(float f)
    {
        float rand = Random.Range(0, 1f);
        return rand < f || rand == 1;
    }

}
