using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHealth : MonoBehaviour
{
    public void attack(float timer, float attackSpeed, int damages, GameObject target, EnemyController enemyScript, Defence defenceScript)
    {
        timer += Time.deltaTime;
        if (timer >= attackSpeed)
        {
            if (target.GetComponent<EnemyController>() == enemyScript)
            {
                enemyScript.health -= damages; // Reduce health of EnemyController

                if (defenceScript.health <= 0)
                {
                    Destroy(target);
                }
            }
            else if (target.GetComponent<Defence>() == defenceScript)
            {
                defenceScript.health -= damages; // Reduce health of Defence

                if (defenceScript.health <= 0)
                {
                    Destroy(target);
                }
            }

            timer = 0;
        }
    }
}
