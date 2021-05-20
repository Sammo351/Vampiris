using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
[ExecuteInEditMode]
public class WeatherManager : MonoBehaviour
{
    [FoldoutGroup("Wind", expanded: true)]
    public Vector3 windDirection = Vector3.forward;
    [FoldoutGroup("Wind")]
    public bool overrideWindDirection = false;
    [FoldoutGroup("Wind")]
    [Range(0, 12)]
    public float windForce = 0;

    public enum RainFall { None, Light, Heavy };
    [FoldoutGroup("Rain", expanded: true)]
    public RainFall rainFall = RainFall.Light;
    RainFall prevRainFall = RainFall.Light;
    // [FoldoutGroup("Rain/Sounds")]
    // public AudioClip lightRainSound, heavyRainSound;
    [FoldoutGroup("Rain/Sound Objects")]
    public GameObject lightRainSoundObject, heavyRainSoundObject;
    [FoldoutGroup("Fog", expanded: true)]

    public bool comingSoon = true;
    void Start()
    {
        windDirection = GetComponentInChildren<WindZone>().gameObject.Facing();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnValidate()
    {
        GetComponentInChildren<WindZone>().windMain = windForce;
        if (overrideWindDirection)
        {
            GetComponentInChildren<WindZone>().gameObject.Facing(windDirection);
        }
        if (rainFall != prevRainFall)
        {
            SetRain(rainFall);
            prevRainFall = rainFall;
        }
    }

    public void SetRain(RainFall fall)
    {

        rainFall = fall;
        List<GameObject> rainParents = GameObject.FindGameObjectsWithTag("Rain").ToList();
        for (int i = 0; i < rainParents.Count; i++)
        {
            GameObject parent = rainParents[i];
            ParticleSystem falling = parent.transform.Find("Falling").GetComponent<ParticleSystem>();
            ParticleSystem colliding = parent.transform.Find("Collision").GetComponent<ParticleSystem>();

            var fallEmi = falling.emission;
            fallEmi.rateOverTime = (fall == RainFall.Heavy ? 900 : fall == RainFall.Light ? 100 : 0);
            float collideRate = fall == RainFall.Heavy ? 900 : fall == RainFall.Light ? 100 : 0;
            StartCoroutine(UpdateRainCollisions(colliding, collideRate));

            SetRainSound(rainFall);

        }
    }

    IEnumerator UpdateRainCollisions(ParticleSystem system, float rate)
    {
        yield return new WaitForSeconds(0.9f);
        var colEmi = system.emission;
        colEmi.rateOverTime = rate;
    }
    void SetRainSound(RainFall fall)
    {
        Transform soundParent = transform.Find("Sounds");

        GameObject nextRainSoundObjectTemplate = fall == RainFall.Heavy ? heavyRainSoundObject : fall == RainFall.Light ? lightRainSoundObject : null;
        AudioSource currentRainSound = soundParent.GetComponentInChildren<AudioSource>();
        GameObject nextRainSoundObject = null;
        if (nextRainSoundObjectTemplate != null)
        {
            nextRainSoundObject = GameObject.Instantiate(nextRainSoundObjectTemplate);
            nextRainSoundObject.transform.parent = soundParent;
        }


        AudioSource nextAudioSource = nextRainSoundObject != null ? nextRainSoundObject.GetComponent<AudioSource>() : null;
        AudioHelper.Fade(currentRainSound, nextAudioSource);
    }




}
/*
thunder/lightning (sound/lighting)
Fog

*/