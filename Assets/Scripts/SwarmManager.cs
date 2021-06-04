using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class SwarmManager : MonoBehaviour
{
    public VisualEffect[] BatSpawners;
    public Transform SwarmTarget;

    void Start()
    {
        BatSpawners = (VisualEffect[])FindObjectsOfType<VisualEffect>().Where(a => a.tag == "SwarmSpawner");
    }
    bool swarm = false;

    // Update is called once per frame
    void Update()
    {
        bool flag = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swarm = !swarm;

        }
        foreach (VisualEffect ef in BatSpawners)
        {
            if (swarm)
            {
                ef.SendEvent("OnSwarm");
                ef.SetFloat("Stick Distance / Disipate", 3);
            }
            else
                if (!swarm)
            {
                ef.SetFloat("Stick Distance / Disipate", -1);
            }
            ef.SetVector3("Target Swam/Position", SwarmTarget.position);
        }

      
    }
}
