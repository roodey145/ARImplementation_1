using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Stats stats;

    public void TakeDamage(int damage, DamageType damageType)
    {
        if (CanTakeDamage(damageType))
        {
            int actualDamage = Mathf.Max(0, damage - stats.defense);
            stats.health -= actualDamage;
            Debug.Log(gameObject.name + " took " + actualDamage + " damage from " + damageType.ToString());
        }
    }

    private bool CanTakeDamage(DamageType damageType)
    {
        // Check if the character can take damage based on its character type and the damage type.
        switch (stats.characterType)
        {
            case CharacterType.GroundAlly:
                return (damageType == DamageType.GroundAttack || damageType == DamageType.Both);
            case CharacterType.AerialAlly:
                return (damageType == DamageType.AerialAttack || damageType == DamageType.Both);
            case CharacterType.GroundEnemy:
                return (damageType == DamageType.GroundAttack || damageType == DamageType.Both);
            case CharacterType.AerialEnemy:
                return (damageType == DamageType.AerialAttack || damageType == DamageType.Both);
            default:
                return true; // Allow all other combinations by default
        }
    }
}
