using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ProjectileLauncher : MonoBehaviour
{
    public GameObject bolt;
    public GameObject spawnpoint;
    public float force = 32;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [Button]
    void Shoot()
    {
        GameObject g = GameObject.Instantiate(bolt, spawnpoint.Pos(), spawnpoint.transform.rotation);
        g.GetComponent<Rigidbody>().AddForce(spawnpoint.transform.forward * force, ForceMode.Impulse);
    }
}
