using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LightningFlash : MonoBehaviour
{
    public float[] flashDelays;
    public float distanceDelay;
    public AudioClip[] soundClips;
    Light light;
    bool show = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Trigger()
    {
        if (flashDelays == null)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, 21);
        GameObject lg = new GameObject();
        light = lg.AddComponent<Light>();
        lg.transform.SetParent(transform);
        lg.transform.eulerAngles = lg.transform.eulerAngles.SetY(Random.Range(0, 360));
        light.type = LightType.Point;
        light.shadowRadius = 120;
        light.intensity = 0;
        light.range = 320;
        light.shadows = LightShadows.Hard;
        for (int i = 0; i < flashDelays.Length; i++)
        {
            float flashDelay = flashDelays[i];
            StartCoroutine(Strike(flashDelay, i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Strike(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Thunder(distanceDelay, index));
        light.intensity = 2000;
        light.transform.position = new Vector3(Random.Range(-100, 100), 40, Random.Range(-100, 100));
        StartCoroutine(StopStrike());

    }
    IEnumerator StopStrike()
    {
        yield return new WaitForSeconds(Random.Range(0.03f, 0.3f));
        light.shadows = LightShadows.None;
        light.intensity = 0;
    }
    IEnumerator Thunder(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(soundClips[index], Vector3.zero);

    }

}
