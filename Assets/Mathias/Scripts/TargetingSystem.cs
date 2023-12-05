using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public CharacterType characterType;
    public DamageType damageType;

    //Method to check if the character can target a given type
    public bool CanTarget(CharacterType targetType)
    {
        if (characterType == CharacterType.GroundAlly && damageType == DamageType.GroundAttack)
        {
            return targetType == CharacterType.GroundEnemy;
        }

        if (characterType == CharacterType.GroundAlly && damageType == DamageType.RangedAttack)
        {
            return targetType == CharacterType.GroundEnemy || targetType == CharacterType.AerialEnemy;
        }

        if (characterType == CharacterType.AerialAlly && damageType == DamageType.RangedAttack)
        {
            return targetType == CharacterType.GroundEnemy || targetType == CharacterType.AerialEnemy;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterType targetType1 = CharacterType.GroundEnemy;
        CharacterType targetType2 = CharacterType.GroundEnemy;

        if (CanTarget(targetType1))
        {
            Debug.Log("Character can target the specified type.");
        }
        else
        {
            Debug.Log("Character Cannot target the specified type");
        }

        if (CanTarget(targetType2))
        {
            Debug.Log("Character can target the specified type.");
        }
        else
        {
            Debug.Log("Character Cannot target the specified type");
        }
    }
}
