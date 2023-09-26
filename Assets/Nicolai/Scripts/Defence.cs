using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Defence : MonoBehaviour
{
    public int range;
    public int health;
    public int damage;
    public GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        // Find all GameObjects with the specified tag.
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if(currentTarget == null)
        {
            foreach (GameObject enemyObj in taggedObjects)
            {
                // Calculate the distance between the search origin and the object.
                float distance = Vector3.Distance(transform.position, enemyObj.transform.position);

                // Check if the object is within the specified distance.
                if (distance <= range)
                {
                    Debug.Log("Found " + enemyObj.name + " within distance: " + distance);
                    currentTarget = enemyObj;
                    // The object is within the distance. You can perform actions here.
                    
                }
            }
        }
        

    }
}
