using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamagePacket
{
    public float amount;
    public Enums.DamageType type = Enums.DamageType.Blunt;
    public delegate void OnDamageDelegate(DamagePacket packet, Health health);
    public OnDamageDelegate OnDamage;

    public DamagePacket(float _amount, Enums.DamageType _type = Enums.DamageType.Blunt, OnDamageDelegate _onDamage = null)
    {
        amount = _amount;
        type = _type;
        OnDamage = _onDamage;
    }
    public DamagePacket(float _amount, Enums.DamageType _type)
    {
        _amount = amount;
        type = _type;
    }

}