using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    [SerializeField] public static int requiredScore = 10000;
    public static int playerMoney = 0;
    [SerializeField] public static int playerLives = 3;
    [SerializeField] public static int currentPlayerScore = 0;
    [SerializeField] public static int tempCashCounter = 0;
    [SerializeField] public static int itemShopSlots = 3;
    [SerializeField] private string[] levels;
    public static string[] levelList;
    //[SerializeField] public static List<string> powerUps;

    [SerializeField] public static bool bankDestroyed = false;

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

    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (lvlSpawner == null)
        //{
        //    lvlSpawner = GameObject.FindGameObjectWithTag("LevelSpawner").GetComponent<LevelSpawner>();
        //}
    }
}
