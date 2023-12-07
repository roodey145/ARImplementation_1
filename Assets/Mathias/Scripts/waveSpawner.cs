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

    public static bool spawningStarted = false;

    private Locateable _locateable;

    // Start is called before the first frame update
    void Start()
    {
        _locateable = GetComponent<Locateable>();
        //SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures that the waves only starts when the player click on the Conquer UI Button
        if (!spawningStarted) return;

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
            // Check if this is the last wave
            if(currentWave >= waves.Length)
            { // Reset the spawner, so the player can start from the start.
                spawningStarted = false;
                currentWave = 0;
            }
            else
            {
                SpawnWave();
            }
        }
    }

    public void SpawnWave()
    {
        // Check if all the buildings have been added
        if(!ListItemsManager.instance.IsAllBuildingsAdded())
        { // Some of the buildings are not added yet
            return;
        }

        spawningStarted = true;
        // Checks the currentWave class and get the length of the GetEnemySpawnList
        for (int i = 0; i < waves[currentWave].GetEnemySpawnList().Length; i++)
        {
            Vector2Int location = FindSpawnLoc(_locateable);

            GameObject newspawn = Instantiate(waves[currentWave].GetEnemySpawnList()[i], _locateable.GetPosition(location.x, location.y), Quaternion.identity);
            Locateable locateable = newspawn.GetComponent<Locateable>();
            locateable.AssignPosition(location.x, location.y);
            currentMonster.Add(newspawn);

            EnemyController monster = newspawn.GetComponent<EnemyController>();//Swap enemy with anything I suppose
        }
    }

    //finds a vector3 spawn location. If it can not find a location, it will create an infinite loop of trying to find a location to spawn enemies on
    Vector2Int FindSpawnLoc(Locateable locateable)
    {
        int x = Random.Range(-GroundData.width, GroundData.width);
        int z = Random.Range(-GroundData.length, GroundData.length);

        //float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        //float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        //float yloc = transform.position.y;

        //SpawnPos = new Vector3(xLoc, yloc, zLoc);

        if(!locateable.CheckCollision(x,z))
        {
            return new Vector2Int(x, z);
        }
        else
        {
            return FindSpawnLoc(locateable);
        }
    }
}
