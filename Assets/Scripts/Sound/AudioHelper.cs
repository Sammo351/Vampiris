using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper
{
    public static void Fade(AudioSource one, AudioSource two)
    {
        GameObject g = new GameObject("Cross Fader");
        CrossFade crossFade = g.AddComponent<CrossFade>();

        crossFade.Begin(one, two, 1.5f);
    }
}
