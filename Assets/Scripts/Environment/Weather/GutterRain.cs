using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GutterRain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Events.Instance.OnRainChanged.AddListener(OnRainChanged);

    }
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnRainChanged(Enums.RainFall rainFall)
    {
        StartCoroutine(UpdateParticles(rainFall));
    }
    IEnumerator UpdateParticles(Enums.RainFall rainFall)
    {
        yield return new WaitForSeconds(1f);

        //40,120
        float rainRate = rainFall == Enums.RainFall.Heavy ? 120 : rainFall == Enums.RainFall.Light ? 40 : 0;
        float mistRate = rainFall == Enums.RainFall.Heavy ? 3 : rainFall == Enums.RainFall.Light ? 1 : 0;
        GetComponent<VisualEffect>().SetFloat("Spawn Rate", rainRate);
        GetComponent<VisualEffect>().SetFloat("Mist Spawn", mistRate);

        Debug.Log("Rain Rate: " + rainRate);

    }
}
