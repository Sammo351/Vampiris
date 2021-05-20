using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Nav_Villager : MonoBehaviour
{
    public float walkSpeed = 1.5f;
    public float runSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDestination(Vector3 pos)
    {
        GetComponentInChildren<NavMeshAgent>().SetDestination(pos);
    }
    public float Speed
    {
        get { return GetComponentInChildren<NavMeshAgent>().speed; }
        set { GetComponentInChildren<NavMeshAgent>().speed = value; }
    }
}
