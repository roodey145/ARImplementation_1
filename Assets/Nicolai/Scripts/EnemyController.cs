using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Attacker
{
    NavMeshAgent agent;
    EnemyNav enemyNav;
    SphereCollider rangeColider;
    bool startAttack = false;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        enemyNav = GetComponent<EnemyNav>();
        rangeColider = GetComponent<SphereCollider>();
        rangeColider.radius = range;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // Ensure the attacker does not attack when its health is equal or below zero
        if (health <= 0) return;

        //when we are close enugh we should attack
        if (enemyNav.target != null)
        {
            //used to know what object to damages
            currentTarget = enemyNav.target.gameObject;

            if (startAttack) {
                attack(attackDamage);
            }
        }
        else
        {
            agent.stoppingDistance = 0;
            startAttack = false;
        }
    }

    // avoid interception with target
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        print(other.gameObject + ", " + enemyNav.target.gameObject);
        if(other.gameObject == enemyNav.target.gameObject){

         agent.stoppingDistance = Vector3.Distance(transform.position, enemyNav.target.position);
         startAttack = true;
        }

    }

}
