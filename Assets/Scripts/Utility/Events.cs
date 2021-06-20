using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public class Events : MonoBehaviour
{
    public static Events Instance;
    [FoldoutGroup("Weather")]
    public UnityEvent<Enums.RainFall> OnRainChanged;
    [FoldoutGroup("Weather")]
    public UnityEvent OnLightningStrike, OnLightningFlash;
    [FoldoutGroup("Entity")]
    public UnityEvent<DamagePacket, Health> OnEntityDamaged, OnEntityKilled;
    [FoldoutGroup("Entity")]
    public UnityEvent<Health, float> OnEntityHealed;
    void Awake()
    {
        Instance = this;
        Debug.Log("Awoken");


    }

    /*
    How to use;

    To listen; use OnEnable()
    Events.Instance.OnLightningStrike.AddListener(myFunction)

    To call;
    Events.Instance.OnRainChanged.Invoke(param), in this case param would be Enums.RainFall.Heavy
    */


}
