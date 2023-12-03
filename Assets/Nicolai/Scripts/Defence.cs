using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class Defence : Attacker
{

    public string groundTag = "Ground";
    public static NavMeshSurface groundMeshSurface;


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        if(groundMeshSurface  == null)
            groundMeshSurface = GameObject.FindGameObjectWithTag(groundTag)?.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // Ensure the attacker does not attack when its health is equal or below zero
        if (health <= 0) return;

        if (canAttack)
        {
            if (currentTarget != null)
            {
                attack(damage);
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


    private void OnDestroy()
    {
        groundMeshSurface.BuildNavMesh();
    }

}
