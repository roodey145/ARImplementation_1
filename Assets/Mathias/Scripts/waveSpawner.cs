using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class waveSpawner : MonoBehaviour
{
    [System.Serializable]
    
    // Contains a list of the GetEnemyList monsters
    public class Wavecontent
    {
        [SerializeField] [NonReorderable] GameObject[] enemySpawner;

        // A list of the Monsters Spawned
        public GameObject[] GetEnemySpawnList()
        {
            return enemySpawner;
        } 
    }

    // A list of the wavecontent classes
    [SerializeField] [NonReorderable] Wavecontent[] waves;
    int currentWave = 0;
    float spawnRange = 10;

    public List<GameObject> currentMonster;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        // For loop that removes enemies if there are >=0.
        for (int i = currentMonster.Count - 1; i >= 0; i--)
        {
            if (currentMonster[i] == null) 
            {
                currentMonster.RemoveAt(i);
            }
        }

        //Kill counter for currentMonsters. Tells us when to spawn the next wave
        if(currentMonster.Count == 0)
        {
            currentWave++;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        // Checks the currentWave class and get the length of the GetEnemySpawnList
        for(int i = 0; i < waves[currentWave].GetEnemySpawnList().Length; i++)
        {
            GameObject newspawn = Instantiate(waves[currentWave].GetEnemySpawnList()[i], FindSpawnLoc(),Quaternion.identity);
            currentMonster.Add(newspawn);

            EnemyController monster = newspawn.GetComponent<EnemyController>();//Swap enemy with anything I suppose
        }
    }

    //finds a vector3 spawn location. If it can not find a location, it will create an infinite loop of trying to find a location to spawn enemies on
    Vector3 FindSpawnLoc()
    {
        Vector3 SpawnPos;

        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yloc = transform.position.y;

        SpawnPos = new Vector3(xLoc, yloc, zLoc);

        if(Physics.Raycast(SpawnPos, Vector3.down, 5))
        {
            return SpawnPos;
        }
        else
        {
            return FindSpawnLoc();
        }
    }
}
