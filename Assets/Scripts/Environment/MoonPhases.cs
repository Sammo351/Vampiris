using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class MoonPhases : MonoBehaviour
{
    public List<Texture2D> MoonTextures;
    [Range(0, 7)]
    public int MoonPhase;

    public HDAdditionalLightData moonLightSourceData;
    public Light moonLightSource;

    public bool BloodMoon = false;

    public Color StandardColor, BloodColor;
    public Color StandardFilterColor, BloodFilterColor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        moonLightSourceData.surfaceTexture = MoonTextures[MoonPhase];
        moonLightSourceData.surfaceTint = BloodMoon ? BloodColor : StandardColor;
        moonLightSource.color = BloodMoon ? BloodFilterColor : StandardFilterColor;
    }
}
