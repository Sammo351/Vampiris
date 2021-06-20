using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    [ReadOnly]
    public float points = 100;
    public float max = 100;

    public bool startWithMaxHP = true;
    [HideIf("startWithMaxHP")]
    public float startingHP = 100;

    void Start()
    {
        points = startWithMaxHP ? max : startingHP;
        ClampPoints(points);
    }
    public void Heal(float amount)
    {
        if (amount <= 0) { return; }
        ClampPoints(points + amount);
        Events.Instance.OnEntityHealed.Invoke(this, amount);
    }

    public void Damage(params DamagePacket[] packets)
    {

        for (int i = 0; i < packets.Length; i++)
        {
            AcceptDamage(packets[i]);
        }
    }
    void AcceptDamage(DamagePacket packet)
    {
        float damageAmount = packet.amount;
        if (damageAmount <= 0) { return; }
        float newHealth = points - damageAmount;
        if (packet.OnDamage != null)
        {
            packet.OnDamage(packet, this);
        }
        ClampPoints(newHealth);
        Events.Instance.OnEntityDamaged.Invoke(packet, this);
        if (newHealth <= 0)
        {
            Kill(packet);
        }

    }

    /// <summary>
    /// Trigger Death by outsider
    /// </summary>
    /// <param name="packet"></param>
    void Kill(DamagePacket packet)
    {
        Events.Instance.OnEntityKilled.Invoke(packet, this);
        OnDeath();
    }


    void OnDeath()
    {
        //Event
    }







    void ClampPoints(float p)
    {
        points = Mathf.Clamp(p, 0, max);
    }

}