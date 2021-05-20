using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFade : MonoBehaviour
{

    bool started = false;
    AudioSource one, two;
    float timePassed = 0;
    float totalDuration;
    float targetVolume;
    float oneStartVol = 0.5f;
    public void Begin(AudioSource _one, AudioSource _two, float _duration, float _targetVolume = 0.5f)
    {
        one = _one;
        two = _two;
        totalDuration = _duration;
        targetVolume = _targetVolume;
        if (_one != null)
        {
            oneStartVol = _one.volume;
        }

        started = true;
        Destroy(gameObject, 10);
    }
    void Update()
    {
        if (started)
        {
            timePassed += Time.deltaTime;
            float p = timePassed / totalDuration;
            if (one != null)
            {
                one.volume = Mathf.Lerp(oneStartVol, 0, p);
            }
            if (two != null)
            {
                two.volume = Mathf.Lerp(0, targetVolume, p);
            }

            if (timePassed >= totalDuration)
            {
                if (one != null)
                {
                    Destroy(one.gameObject);
                }

                Destroy(gameObject);
            }
        }
    }



}
