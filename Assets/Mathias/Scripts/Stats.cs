using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum defining different types of characters
public enum CharacterType
{
    GroundAlly,
    AerialAlly,
    GroundEnemy,
    AerialEnemy,
}

//enum defining different types of damage
public enum DamageType
{
    GroundAttack,
    RangedAttack,
    Both,
}

//Class defining the stats for game characters
public class Stats : MonoBehaviour
{
    public CharacterType characterType;
    public DamageType damageType;
    public int health;
    public float range;
    public int defense;
    public int attack;
}
