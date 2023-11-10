using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    GroundAlly,
    AerialAlly,
    GroundEnemy,
    AerialEnemy,
}

public enum DamageType
{
    GroundAttack,
    AerialAttack,
    Both,
}

public class Stats : MonoBehaviour
{
    public CharacterType characterType;
    public DamageType damageType;
    public int health;
    public float range;
    public int defense;
    public int attack;
}
