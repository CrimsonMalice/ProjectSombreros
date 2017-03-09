using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] [Range(5.0f, 25.0f)] private float spawnTimerStart; //At which interval to spawn new enemies.
    [SerializeField] private float spawnTimer; //The active timer.
    [SerializeField] private GameObject enemy; //Which enemy to spawn from the spawner. Set this in the Unity Inspector
    [SerializeField] private GameObject enemyStart; //Which point should the enemy start walking towards? Set this in the Unity Inspector

    [SerializeField] public bool spawnActive = false; //The active timer.

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnActive)
        {
            if (spawnTimer <= 0) //When the timer is below 0, spawn a new enemy and set it's starting PatrolPoint.
            {
                GameObject enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);

                enemyInstance.GetComponent<Sheriff>().NextPos = enemyStart; //Set the Next Position to move towards to the enemyStart
                enemyInstance.GetComponent<Sheriff>().OldPos = gameObject; //Set the old Pos to the spawner
                spawnTimer = spawnTimerStart; //Reset the timer
            }
            else if (spawnTimer > 0) //If the timer is above 0, tick it down.
            {
                spawnTimer -= Time.deltaTime;
            }
        }
	}
}
