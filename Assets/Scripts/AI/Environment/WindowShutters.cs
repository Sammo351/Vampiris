using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class WindowShutters : MonoBehaviour
{
    public GameObject leftShutter, rightShutter;
    public float force = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [Button()]
    void Force()
    {
        leftShutter.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, force, 0), ForceMode.Impulse);
    }
}
