﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    public static int playerAmount = 0;

    [SerializeField] public static int requiredScore = 10000;
    public static int playerMoney = 0;
    public static int playerTwoMoney = 0;
    [SerializeField] public static int playerLives = 3;
    [SerializeField] public static int playerTwoLives = 3;
    [SerializeField] public static int currentPlayerScore = 0;
    [SerializeField] public static int tempCashCounter = 0;
    [SerializeField] public static int itemShopSlots = 3;
    [SerializeField] private string[] levels;
    public static string[] levelList;
    public static string currentLevel;
    //[SerializeField] public static List<string> powerUps;

    [SerializeField] public static bool bankDestroyed = false;
    [SerializeField] public static bool stageTransitLoaded = false;

    public static int enemiesKilled;
    public static int moneyPickedUp;
    public static int bombBonus;

    public static int enemiesKilled2;
    public static int moneyPickedUp2;
    public static int bombBonus2;

    //[SerializeField] private LevelSpawner lvlSpawner;

    int callAmount = 0;
    public static int level = 1;


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        //if (powerUps == null)
        //    powerUps = new List<string>();

        DontDestroyOnLoad(gameObject);

        levelList = levels;
    }
    // Use this for initialization
    void Start ()
    {
        playerAmount = 0;

        //if (SceneManager.GetActiveScene().name == "NewMenu")
        //{
        //    playerMoney = 0;
        //    playerTwoMoney = 0;
        //    playerLives = 3;
        //    playerTwoLives = 3;
        //    currentPlayerScore = 0;
        //    requiredScore = 10000;
        //}

        if (GameObject.FindGameObjectWithTag("PlayerOne"))
        {
            playerAmount++;
        }

        if (GameObject.FindGameObjectWithTag("PlayerTwo"))
        {
            playerAmount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "NewMenu")
        {
            Destroy(gameObject);
        }
    }
}
