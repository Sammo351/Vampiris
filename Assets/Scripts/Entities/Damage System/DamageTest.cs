using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamageTest : MonoBehaviour
{
    void DoDamage()
    {
        DamagePacket packet = new DamagePacket(21, _onDamage: OnDamageDoThis);
    }
    void OnDamageDoThis(DamagePacket packet, Health health)
    {

    }
    [Button]
    void Trigger()
    {
        DamagePacket packet = new DamagePacket(14);
        GetComponent<Health>().Damage(packet);
    }
}