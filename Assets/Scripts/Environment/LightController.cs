using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public List<GameObject> dependants = new List<GameObject>();
    public bool lit = true;
    public bool canSeeSky = true;
    // Start is called before the first frame update
    void OnEnable()
    {

    }
    void Start()
    {
        Events.Instance.OnRainChanged.AddListener(OnRainChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnRainChanged(Enums.RainFall rainFall)
    {
        //extinguish or fizzle
        if (rainFall != Enums.RainFall.None)
        {
            if (canSeeSky)
            {

                SetDependants(false);
            }
        }
        else
        {

            SetDependants(true);
        }
    }
    void OnValidate()
    {
        SetDependants(lit);

    }
    public void SetDependants(bool light)
    {
        lit = light;
        for (int i = 0; i < dependants.Count; i++)
        {
            dependants[i].SetActive(lit);
        }
    }
}
