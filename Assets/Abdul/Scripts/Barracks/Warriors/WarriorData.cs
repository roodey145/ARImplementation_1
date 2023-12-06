using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarriorData : Buyable
{
    [Header("Attacking Settings")]
    public CharacterType characterType;
    public DamageType damageType;
    public int health;
    public float range;
    public int armor;
    public int attackDamage;
    public float attackSpeed;
    // timer need to be reset
    public float timer;

    public WarriorType warriorType;
    public int level;

    public int requiredBarracksLevel;
}
