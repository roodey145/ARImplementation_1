using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    public Transform target;
    public Transform closestWall;
    public float radius;
    public string[] targetType;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Make sure to move the agent to the navmesh
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            // Calculate whether a path exists between the agent and the target
            bool pathExists = NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

            if (pathExists && path.status == NavMeshPathStatus.PathComplete || target.gameObject.CompareTag("Wall"))
            {
                // Path is connected
                //Debug.Log("Path found");
                agent.SetDestination(target.position);
            }
            else
            {
                // attack wall
                FindClosestWall();
                // Path is not connected
                //Debug.Log("Path not found");
                // You might want to reset the target here or handle this case accordingly
            }
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

                if (distance <= radius) // and path available
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

    void FindClosestWall()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Wall");
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objectsWithTag)
        {
            float distanceToObj = Vector3.Distance(currentPosition, obj.transform.position);

            if (distanceToObj < closestDistance)
            {
                closestDistance = distanceToObj;
                closestWall = obj.transform;
            }
        }
        target = closestWall;
        agent.SetDestination(target.position);
    }

    void OnDrawGizmosSelected()
    {
        // Ensure the radius is not negative
        radius = Mathf.Max(0, radius);

        // Draw a wireframe sphere to represent the radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
