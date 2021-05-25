using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
[RequireComponent(typeof(Light))]
public class Flicker : MonoBehaviour
{
    [MinMaxSlider(0, 1, true)]
    public Vector2 targetBrightnessRange = new Vector2(0.7f, 1);
    [MinMaxSlider(0, 1, true)]
    public Vector2 targetSaturationRange = new Vector2(0.65f, .95f);
    public float maxBrightnessChange = 0.04f;
    public float maxSaturationChange = 0.08f;

    float timeUntilFlicker = 0.1f;
    Light lamp;
    Vector3 startingPosition;
    [Range(0, 0.04f)]
    public float maxMovement = 0.04f;
    float startingRange;
    [Range(0, 0.4f)]
    public float rangeChangeAmount = 0.2f;
    void Start()
    {

        lamp = GetComponent<Light>();
        startingRange = lamp.range;
        startingPosition = lamp.transform.position;
    }

    // Update is called once per frame
    [Button]
    void QuickSet()
    {

        GetComponent<Light>().range = 4;
        GetComponent<Light>().color = new Color(1, 0.37f, 0);
        //Shape Radius = 1.5
        // Intensity = 16 Ev 100
        EditorUtility.DisplayDialog("Additional", "Set Shape.Radius to 1.5.\nSet intensity to 16 Ev 100.\nSoz, bu I can't program it.", "Ok", "Cool");
    }
    void Update()
    {
        timeUntilFlicker -= Time.deltaTime;
        if (timeUntilFlicker <= 0)
        {
            float[] lastHSV = lamp.color.HSV();
            float newSaturation = Mathf.Clamp(lastHSV[1] + Random.Range(-maxSaturationChange, maxSaturationChange), targetSaturationRange.x, targetSaturationRange.y);
            float newBrightness = Mathf.Clamp(lastHSV[2] + Random.Range(-maxBrightnessChange, maxBrightnessChange), targetBrightnessRange.x, targetBrightnessRange.y);
            lamp.color = lamp.color.SetBrightness(newBrightness).SetSaturation(newSaturation);
            timeUntilFlicker = Random.Range(0.02f, 0.14f);

            Vector3 jumpVector = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized * maxMovement * 2;
            Vector3 potentialPos = lamp.transform.position + jumpVector;
            Vector3 dir = potentialPos - startingPosition;
            dir = Vector3.ClampMagnitude(dir, maxMovement);
            lamp.transform.position = startingPosition + dir;

            lamp.range = startingRange + Random.Range(-rangeChangeAmount, rangeChangeAmount);
        }

    }
}
