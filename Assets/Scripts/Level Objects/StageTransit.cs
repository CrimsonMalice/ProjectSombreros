using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransit : MonoBehaviour {

    PlayerController pc;
    bool hasTriggered = false;

    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasTriggered) //If a collision with the player happened and we haven't transitioned to a new level
        {
            LevelSpawner lvlSpawner = GameObject.FindGameObjectWithTag("LevelSpawner").GetComponent<LevelSpawner>();

            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            LevelManager.requiredScore += 8000; //Set a new required score for the next stage
            LevelManager.currentPlayerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Points; //Update the current player score
            LevelManager.playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().money; //Update the current player money
            LevelManager.playerLives = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().lives; //Update the current player lives
            LevelManager.level++; //Tell the LevelManager we have progressed one level

            lvlSpawner.moneyHasSpawned = false; //Set it so that money hasn't spawned
            lvlSpawner.obstaclesHasSpawned = false; //Set it so that obstacles hasn't spawned
            lvlSpawner.moneySpawns.Clear(); //Empty the moneySpawns list so we can have a fresh list in the next level

            LevelManager.tempCashCounter = lvlSpawner.cashCounter; //Reset the cash counter

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-80, 16, 0); //Set the player's position to the spawn

            hasTriggered = true; //Set it so the transition has been triggered to prevent an eternal loop

            SceneManager.LoadScene("Level_1"); //Load the next level
        }
    }
}
