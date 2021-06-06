using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;
public class ChimneyController : MonoBehaviour
{
    public bool smoke = false;

    // Start is called before the first frame update
    float maxRate = 30;
    bool transition = false;
    float progress = 0;
    void Start()
    {
        transition = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSmoke();
    }
    void OnValidate()
    {
        transition = true;

    }
    void UpdateSmoke()
    {
        if (!transition) { return; }
        List<VisualEffect> viss = GetComponentsInChildren<VisualEffect>().ToList();
        for (int i = 0; i < viss.Count; i++)
        {
            VisualEffect vis = viss[i];
            if (!vis.HasFloat("Rate"))
            {
                continue;
            }
            float changeRate = 0.1f;
            progress += smoke ? changeRate : -changeRate;
            float nextRate = Mathf.Lerp(0, maxRate, progress);
            vis.SetFloat("Rate", nextRate);
            if (progress > 1 || progress < 0)
            {
                transition = false;
                progress = Mathf.Clamp01(progress);
            }
        }

    }
    public void SetSmoke(bool active)
    {
        smoke = active;
        transition = true;
    }

}
