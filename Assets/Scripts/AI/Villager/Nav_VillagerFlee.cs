using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Nav_VillagerFlee : Nav_Villager
{
    // Start is called before the first frame update
    void Start()
    {
        //Remove all opposing Villager Navigation Scripts
        GetComponents<Nav_Villager>().Where(x => x != this).ToList().ForEach(x => Destroy(x));


        List<Nav_VillagerExit> exits = GameObject.FindObjectsOfType<Nav_VillagerExit>().ToList();
        if (exits.Count > 0)
        {
            exits.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
            SetDestination(exits[0].transform.position);
            Speed = runSpeed;
        }
        else
        {
            //else take cover /cower?
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
