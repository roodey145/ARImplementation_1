using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    
    public Transform target;
    public float radius;
    public string[] targetType;
    private NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }else
        {
            agent.destination = target.position;
        }
        
    }
    void FindTarget()
    {
        bool targetFound = false;

        for (int i = 0; i < targetType.Length; i++)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetType[i]);

            foreach (GameObject defenceObj in taggedObjects)
            {
                float distance = Vector3.Distance(transform.position, defenceObj.transform.position);

                if (distance <= radius)// and path avaiable
                {

                    target = defenceObj.transform;
                    targetFound = true; // Set target found to true
                    break; // Break out of the foreach loop
                }
            }

            if (targetFound)
            {
                break; // Break out of the for loop if target is found
            }
        }
           
        
        


      
    }
    void OnDrawGizmosSelected()
    {
        // Ensure the radius is not negative
        radius = Mathf.Max(0, radius);

        // Draw a wireframe sphere to represent the radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
