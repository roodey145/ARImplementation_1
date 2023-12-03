using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : Attacker
{

    public AttackHealth attackHealth;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // Ensure the attacker does not attack when its health is equal or below zero
        if (health <= 0) return;

        //when we are close enugh we should attack
        if (GetComponent<EnemyNav>().target != null)
        {
            //used to know what object to damages
            currentTarget = GetComponent<EnemyNav>().target.gameObject;


            float distance = Vector3.Distance(transform.position, GetComponent<EnemyNav>().target.position);
            if (distance <= range) {
                print("true");
                attack(damage);
            }
        }
    }
   
}
