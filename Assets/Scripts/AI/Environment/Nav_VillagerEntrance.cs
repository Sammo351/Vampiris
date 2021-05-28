using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
public class Nav_VillagerEntrance : MonoBehaviour

{
    public GameObject villager;

    public bool spawnRandomly = true;
    [Range(0, 1f)]
    [ShowIf("spawnRandomly")]
    public float spawnChance = 0.1f;
    [ShowIf("spawnRandomly")]
    [SuffixLabel("Seconds")]
    public float spawnFrequency = 10;
    public List<Nav_VillagerExit> exits = new List<Nav_VillagerExit>();
    Nav_VillagerExit target;
    float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(villager, "Villager Entrance: Villager object required.");
        UnityEngine.Assertions.Assert.IsNotNull(exits, "Villager Entrance: Exit required.");

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= spawnFrequency)
        {
            timePassed = 0;
            if (V.Chance(spawnChance))
            {
                Spawn();
            }
        }
    }

    [Button]
    void Spawn()
    {

        target = exits.Any();
        GameObject g = Instantiate(villager, transform.position, transform.rotation);
        g.GetComponentInChildren<NavMeshAgent>().SetDestination(target.transform.position);
    }
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.transform.position, 0.3f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position - target.transform.forward * 1, 0.3f);
        }
    }
}
