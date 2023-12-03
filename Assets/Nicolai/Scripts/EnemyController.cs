using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public int health;
    
    public int damage;
    public float attackZone;
    public float attackSpeed;
    public float timer;

    public AttackHealth attackHealth;
    public GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //when we are close enugh we should attack
        if(GetComponent<EnemyNav>().target != null)
        {
            //used to know what object to damages
            currentTarget = GetComponent<EnemyNav>().target.gameObject;


            float distance = Vector3.Distance(transform.position, GetComponent<EnemyNav>().target.position);
            if (distance <= attackZone) {
                print("true");
                attack(damage);
                
            }
        }
        

    }

    private void attack(int damages)
    {
        timer += Time.deltaTime;
        if (timer >= attackSpeed)
        {
            currentTarget.GetComponent<Defence>().health -= damages;
            timer = 0;
        }

        
    }

    private void takeDamages(int damages)
    {
        health -= damages;
    }



   
}
