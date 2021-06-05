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


    [FoldoutGroup("Rain", expanded: true)]
    public Enums.RainFall rainFall = Enums.RainFall.Light;
    Enums.RainFall prevRainFall = Enums.RainFall.Light;
    // [FoldoutGroup("Rain/Sounds")]
    // public AudioClip lightRainSound, heavyRainSound;
    [FoldoutGroup("Rain/Sound Objects")]
    public GameObject lightRainSoundObject, heavyRainSoundObject;
    //[FoldoutGroup("Fog", expanded: true)]
    [FoldoutGroup("Lightning", expanded: true)]
    int maxSuccessiveLightningStrikes = 3;
    [FoldoutGroup("Lightning", expanded: true)]
    [MinMaxSlider(0, 7f, true)]
    public Vector2 lightningSoundDelay = new Vector2(0f, 1.5f);
    [FoldoutGroup("Lightning", expanded: true)]
    public List<AudioClip> lightningSounds;

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

            //SetRain(rainFall);

        }
    }
    IEnumerator PostStart()
    {
        yield return new WaitForSeconds(2);
        SetRain(Enums.RainFall.Heavy);
    }
    public void SetRain(Enums.RainFall fall)
    {
        prevRainFall = rainFall;
        rainFall = fall;
        Events.Instance.OnRainChanged.Invoke(rainFall);
        List<GameObject> rainParents = GameObject.FindGameObjectsWithTag("Rain").ToList();
        for (int i = 0; i < rainParents.Count; i++)
        {
            GameObject parent = rainParents[i];
            ParticleSystem falling = parent.transform.Find("Falling").GetComponent<ParticleSystem>();
            ParticleSystem colliding = parent.transform.Find("Collision").GetComponent<ParticleSystem>();

            var fallEmi = falling.emission;
            fallEmi.rateOverTime = (fall == Enums.RainFall.Heavy ? 900 : fall == Enums.RainFall.Light ? 100 : 0);
            float collideRate = fall == Enums.RainFall.Heavy ? 900 : fall == Enums.RainFall.Light ? 100 : 0;
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
    void SetRainSound(Enums.RainFall fall)
    {
        Transform soundParent = transform.Find("Sounds");

        GameObject nextRainSoundObjectTemplate = fall == Enums.RainFall.Heavy ? heavyRainSoundObject : fall == Enums.RainFall.Light ? lightRainSoundObject : null;
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

    [FoldoutGroup("Lightning", expanded: true)]
    [Button]
    void SummonLightning()
    {
        float soundDelay = lightningSoundDelay.Next();
        GameObject lg = new GameObject("Lightning Flash");
        LightningFlash lightning = lg.AddComponent<LightningFlash>();
        int flashAmount = Random.Range(1, maxSuccessiveLightningStrikes);
        float[] flashDelays = new float[flashAmount];
        List<AudioClip> clips = new List<AudioClip>();
        for (int i = 0; i < flashAmount; i++)
        {


            float successiveDelay = Random.Range(0.3f, 0.8f);
            flashDelays[i] = successiveDelay;
        }
        lightning.soundClips = lightningSounds.AnyDifferent(flashAmount).ToArray();
        lightning.flashDelays = flashDelays;
        lightning.distanceDelay = soundDelay;
        lightning.Trigger();


    }

    IEnumerator PlayLightningSound(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);

    }

    // void FindLightWithShadow()
    // {
    //     List<Light> lights = Resources.FindObjectsOfTypeAll<Light>().Where(x => x.type == LightType.Directional).Where(x => x.shadows != LightShadows.None).ToList();
    //     if (lights.Count == 0)
    //     {
    //     }
    //     GetComponent<Light>().shadows = LightShadows.Hard;
    // }


}
/*
thunder/lightning (sound/lighting)
Fog

*/