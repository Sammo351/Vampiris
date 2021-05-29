using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Events : MonoBehaviour
{
    public static Events Instance;
    public UnityEvent<Enums.RainFall> OnRainChanged;
    public UnityEvent OnLightningStrike;
    void OnEnable()
    {
        Instance = this;
    }

    /*
    How to use;

    To listen;
    Events.Instance.OnLightningStrike.AddListener(myFunction)

    To call;
    Events.Instance.OnRainChanged.Invoke(param), in this case param would be Enums.RainFall.Heavy
    */


}
