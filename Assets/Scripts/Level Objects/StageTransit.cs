﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (other.gameObject.tag == "PlayerOne" && !hasTriggered || other.gameObject.tag == "PlayerTwo" && !hasTriggered) //If a collision with the player happened and we haven't transitioned to a new level
        {
            LevelSpawner lvlSpawner = GameObject.FindGameObjectWithTag("LevelSpawner").GetComponent<LevelSpawner>();

            LevelManager.requiredScore += 8000; //Set a new required score for the next stage
            LevelManager.currentPlayerScore = PlayerController.points; //Update the current player score
            LevelManager.playerMoney = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().money; //Update the current player money
            LevelManager.playerLives = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().lives; //Update the current player lives

            if (GameObject.FindGameObjectWithTag("PlayerTwo"))
            {
                LevelManager.playerTwoMoney = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().money; //Update the current player money
                LevelManager.playerTwoLives = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().lives; //Update the current player lives
            }

            LevelManager.level++; //Tell the LevelManager we have progressed one level

            lvlSpawner.moneyHasSpawned = false; //Set it so that money hasn't spawned
            lvlSpawner.obstaclesHasSpawned = false; //Set it so that obstacles hasn't spawned
            lvlSpawner.moneySpawns.Clear(); //Empty the moneySpawns list so we can have a fresh list in the next level

            LevelManager.tempCashCounter = lvlSpawner.cashCounter; //Reset the cash counter

            GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-450.4f, 245.9f, 0); //Set the player's position to the spawn

            if (GameObject.FindGameObjectWithTag("PlayerTwo"))
            {
                GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-450.4f, 245.9f, 0);
            }

            hasTriggered = true; //Set it so the transition has been triggered to prevent an eternal loop
            LevelManager.bankDestroyed = false;

            //string nextLevel = LevelManager.levelList[Random.Range(0, LevelManager.levelList.Length)];

            //SceneFadingManager.BeginFade ();

            GameObject.Find("Bank").GetComponent<BankWhiteFade>().faded = false;
            GameObject.Find("Bank").GetComponent<BankWhiteFade>().doOnce = false;
            GameObject.Find("Bank").GetComponent<BankWhiteFade>().whiteTexture.enabled = false;
            BankWhiteFade.duration = 2.5f;

            SceneManager.LoadScene("StageComplete"); //Load the next level
			//SceneFadingManager.OnLevelWasLoaded ();
        }
    }
}
