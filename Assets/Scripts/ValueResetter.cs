using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueResetter : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LevelManager.requiredScore = 10000;
        LevelManager.playerMoney = 0;
        LevelManager.playerLives = 3;
        LevelManager.currentPlayerScore = 0;
        LevelManager.tempCashCounter = 0;
        LevelManager.itemShopSlots = 3;
        LevelManager.bankDestroyed = false;
        LevelManager.stageTransitLoaded = false;

        LevelManager.level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
