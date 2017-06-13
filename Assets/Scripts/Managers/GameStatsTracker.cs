using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsTracker : MonoBehaviour {

    public static int totalEnemiesKilled = 0; //Increments after each kill in Sheriff.cs
    public static int totalMoneyCollected = 0; //Increments after each money pickup in Money.cs
    public static int levelsCleared = 0; //Increments after each level completed in StageCompleteScript.cs
    public static int bombsBlown = 0; //Increments after each bomb blown in PlayerController.cs
    public static int finalScore = 0; //Increments after each money pickup in Money.cs and Sheriff.cs

    public static GameStatsTracker instance = null;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
