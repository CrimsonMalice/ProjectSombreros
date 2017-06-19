using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {

    [SerializeField] public bool levelInstansiated = false;

    [SerializeField] public GameObject levelExitPath;
    [SerializeField] public GameObject exitRocks;
    [SerializeField] public GameObject levelTransitionTrigger;

    [SerializeField] public List<GameObject> moneySpawns;
    [SerializeField] public GameObject[] moneyObjects;

    [SerializeField] public List<GameObject> obstacleSpawns;
    [SerializeField] public GameObject[] obstacleObjects;

    [SerializeField] public bool moneyHasSpawned = false;
    [SerializeField] public bool obstaclesHasSpawned = false;

    [SerializeField] public int cashCounter = 0;

    [SerializeField] public bool hasSpawned = false;

    [SerializeField] private PlayerController playerObject;

    [SerializeField] float counter = BankWhiteFade.duration;
    bool counterDone = false;


    // Use this for initialization
    void Start ()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();

        levelExitPath = GameObject.FindGameObjectWithTag("LevelExitPath");
        exitRocks = GameObject.FindGameObjectWithTag("ExitRocks");
        levelTransitionTrigger = GameObject.FindGameObjectWithTag("StageTransit");

        cashCounter = LevelManager.tempCashCounter;

        playerObject.readInput = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!levelInstansiated && LevelManager.bankDestroyed && !counterDone)
        {
            if (counter <= 0)
            {
                InstansiateLevel();

                playerObject.readInput = true;

                hasSpawned = false;
                levelInstansiated = false;
                counterDone = true;

                for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemySpawn").Length; i++) //Activate all the enemy spawners in the scene
                {
                    GameObject.FindGameObjectsWithTag("EnemySpawn")[i].GetComponent<EnemySpawner>().spawnActive = true;
                }

                //levelExitPath.SetActive(false);
                levelTransitionTrigger.SetActive(false);
            }
            else if (counter > 0)
            {
                counter -= Time.deltaTime;
            }
        }

        if (PlayerController.points >= LevelManager.requiredScore && !hasSpawned)
        {
            print("Spawned");
            exitRocks.SetActive(false);
            GameObject.FindGameObjectWithTag("ExitArrow").GetComponent<SpriteRenderer>().enabled = true;
            hasSpawned = true;

            //levelExitPath.SetActive(true);
            levelTransitionTrigger.SetActive(true);
        }
    }

    void SpawnCash()
    {
        do
        {
            print("Calling SpawnCash()");

            int randomSpawn = RandomSpawn(moneySpawns);
            print(randomSpawn);

            GameObject newInstance = Instantiate(moneyObjects[Random.Range(0, moneyObjects.Length)], moneySpawns[randomSpawn].transform.position, Quaternion.identity);

            print(newInstance);

            moneySpawns.RemoveAt(randomSpawn);

            if (newInstance != null)
                cashCounter += newInstance.GetComponent<MoneyPickUp>().PointsValue;

            newInstance = null;
        }
        while (cashCounter <= LevelManager.requiredScore + 1000);

        moneyHasSpawned = true;
    }

    void SpawnObstacles()
    {
        int randomAmount = Random.Range(0, 4);
        int randomCounter = 0;
        do
        {
            print("Calling SpawnObstacle()");

            int randomSpawn = RandomSpawn(obstacleSpawns);

            Instantiate(obstacleObjects[Random.Range(0, obstacleObjects.Length)], obstacleSpawns[randomSpawn].transform.position, Quaternion.identity);

            obstacleSpawns.RemoveAt(randomSpawn);

            randomCounter++;
        }
        while (randomCounter < randomAmount);

        obstaclesHasSpawned = true;
    }

    public void InstansiateLevel()
    {
        moneySpawns = new List<GameObject>();

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("MoneySpawn").Length; i++)
        {
            moneySpawns.Add(GameObject.FindGameObjectsWithTag("MoneySpawn")[i]);
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("ObstacleSpawn").Length; i++)
        {
            obstacleSpawns.Add(GameObject.FindGameObjectsWithTag("ObstacleSpawn")[i]);
        }

        //levelExitPath = GameObject.FindGameObjectWithTag("LevelExitPath");
        //exitRocks = GameObject.FindGameObjectWithTag("ExitRocks");
        levelTransitionTrigger = GameObject.FindGameObjectWithTag("StageTransit");

        print(moneyHasSpawned);
        print(obstaclesHasSpawned);

        //levelExitPath.SetActive(false);
        //levelTransitionTrigger.SetActive(false);

        hasSpawned = false;

        if (!moneyHasSpawned)
            SpawnCash();

        if (!obstaclesHasSpawned)
            SpawnObstacles();

        levelInstansiated = true;
    }

    int RandomSpawn(List<GameObject> spawns)
    {
        return Random.Range(0, spawns.Count);
    }
}
