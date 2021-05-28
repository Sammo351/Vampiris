using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nav_VillagerExit : MonoBehaviour
{
    public bool destroyEnteringVillager = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {

        if (destroyEnteringVillager)
        {
            //Debug.Log(col.gameObject.name + " exited world");
            Destroy(col.transform.root.gameObject);
        }

    }
}
