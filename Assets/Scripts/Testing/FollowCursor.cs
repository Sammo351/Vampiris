using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask layer;
    public NavMeshAgent agent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layer))
        {
            agent.SetDestination(hit.point);
        }
    }
}
