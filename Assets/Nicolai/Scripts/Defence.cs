using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class Defence : MonoBehaviour
{
    public bool canAttack;
    
    public int health;

    public float range;
    public int damages;
    public float attackSpeed;
    public float timer;
    
    public GameObject currentTarget;
    public List<GameObject> targetList = new List<GameObject>();

    public string groundTag = "Ground";
    public NavMeshSurface groundMeshSurface;


    // Start is called before the first frame update
    void Start()
    {
        groundMeshSurface = GameObject.FindGameObjectWithTag(groundTag)?.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
            //need to bake new navmesh when destroyed if it is a wall

    
        }

        if (canAttack)
        {
            if (currentTarget != null)
            {
                attack(damages);
            }

            if (currentTarget == null)
            {
                for (int i = 0; i < targetList.Count; i++)
                {
                    if (targetList[i] != null)
                    {
                        currentTarget = targetList[i];
                        break;
                    }

                }


            }
        }

        // Check for overlapping colliders within the specified sphere area




    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targetList.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider has the tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Remove the game object from the target list
            targetList.Remove(other.gameObject);
        }
    }


    private void attack(int damages)
    {
        timer += Time.deltaTime;
        if (timer >= attackSpeed)
        {
            currentTarget.GetComponent<EnemyController>().health -= damages;
            timer = 0;
            //get the heath from enemy
        }
    }

    private void takeDamages(int damages)
    {
        health -= damages;
    }

    private void OnDestroy()
    {
        groundMeshSurface.BuildNavMesh();
    }

}
