using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance = null;

    public Rigidbody2D rbody;
    Animator animator;

    [SerializeField] public Vector2 velocity;
    [SerializeField] public Vector2 direction;
    [SerializeField] [Range(40f, 100f)] public float speed = 70;
    [SerializeField] private int points;
    [SerializeField] public int money;

    [SerializeField] private bool isActive = true;
    [SerializeField] private bool invisable = false;
    [SerializeField] public bool readInput = true;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool moving;
    [SerializeField] private bool canGetHit = true;
    [SerializeField] public bool canTakeExtraHit = false;
    [SerializeField] public bool affectedByMud = true;

    [SerializeField] [Range(0, 9)] public int lives = 3;

    [SerializeField] private float bombDelayTimer;
    [SerializeField] [Range(1.5f, 5.5f)] private float bombDelayTimerStart;
    [SerializeField] public float deathTimer;
    [SerializeField] [Range(2.5f, 7.5f)] public float deathTimerStart = 3.5f;
    [SerializeField] float flashTimer = 0f;
    [SerializeField] float flashTimerStart = 0.01f;
    [SerializeField] float freezeTimer = 0f;
    [SerializeField] float freezeTimerStart = 1.0f;

    [SerializeField] float gameOverTimer = 6.66f;

    [SerializeField] public GameObject selectedBomb;

    [SerializeField] private Vector3 spawnPoint = new Vector3(-80, 16, 0);

    [SerializeField] public AudioClip deathSound;

    //[SerializeField] private bool powerupsLoaded = false;
    //[SerializeField] public List<string> powerUps;

    private static GameObject gameOverText;
    private static Text scoreText;
    private static Text livesText;
    private static Text moneyText;

    public int Points
    {
        get { return this.points; }
        set { this.points = value; }
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        points = LevelManager.currentPlayerScore;
        lives = LevelManager.playerLives;

        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        points = LevelManager.currentPlayerScore;
        money = LevelManager.playerMoney;
    }
    // Use this for initialization
    void Start ()
    {

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("GameOverText").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("GameOverText")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("ScoreText").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("ScoreText")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("MoneyText").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("MoneyText")[1]);
        }

        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);

        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
        livesText = GameObject.FindGameObjectWithTag("LivesText").GetComponent<Text>();

        //powerUps = LevelManager.powerUps;

        //if(powerUps.Count >= 1)
        //    LoadPowerUps();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (LevelManager.stageTransitLoaded)
        {
            readInput = false;

            direction = new Vector2(1, 0);
            velocity = rbody.velocity / (speed / 3);
            rbody.velocity = new Vector2(1f, 0f) * (speed / 3);
        }

        if (readInput)
        {
            DoInput();
            rbody.velocity = NewVelocity() * speed;
            velocity = rbody.velocity / speed;
        }

        if (!isActive)
        {
            if (lives > 0)
            {
                PlayerDeath();
            }
            else if (lives <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;

                readInput = false;
                canAttack = false;
                moving = false;
                canGetHit = false;
                isActive = false;

                gameOverText.SetActive(true);

                if (gameOverTimer > 0)
                {
                    gameOverTimer -= Time.deltaTime;
                }
                else if (gameOverTimer <= 0)
                {
                    Destroy(gameObject);
                    Destroy(GameObject.FindGameObjectWithTag("LevelManager"));
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        UpdateAnimator();
        Timers();

        scoreText.text = "Score: " + points + " / " + LevelManager.requiredScore;
        moneyText.text = "Money: " + money;
        livesText.text = "x" + lives;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet" && canGetHit && !canTakeExtraHit)
        {
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);
            }
            AudioSource.PlayClipAtPoint(deathSound, new Vector3(7, 8, -10), 1.0f);
            lives--;

            rbody.velocity = Vector2.zero;

            readInput = false;
            canAttack = false;
            moving = false;
            canGetHit = false;
            isActive = false;

            //Wait until end of Death Animation, then change position of the player and start the timer.
            transform.position = spawnPoint;

            freezeTimer = freezeTimerStart;
            deathTimer = deathTimerStart;
        }
    }

    Vector2 NewVelocity()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moving = true;
            direction = new Vector2(1, 0);
            return new Vector2(1, 0);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moving = true;
            direction = new Vector2(-1, 0);
            return new Vector2(-1, 0);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            moving = true;
            direction = new Vector2(0, 1);
            return new Vector2(0, 1);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            moving = true;
            direction = new Vector2(0, -1);
            return new Vector2(0, -1);
        }

        else
        {
            moving = false;
            return Vector2.zero;
        }
    }

    void DoInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Instantiate(selectedBomb, transform.position, Quaternion.identity);
            canAttack = false;
            bombDelayTimer = bombDelayTimerStart;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < ListOfItems.listOfAllShopItems.Count; i++)
            {
                print(ListOfItems.listOfAllShopItems[i]);
            }
        }
    }

    void Timers()
    {
        if (bombDelayTimer > 0)
        {
            bombDelayTimer -= Time.deltaTime;
        }
        else if (bombDelayTimer <= 0)
        {
            bombDelayTimer = 0;
            canAttack = true;
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat("DirX", rbody.velocity.x);
        animator.SetFloat("DirY", rbody.velocity.y);
        animator.SetInteger("VelocityX", (int)velocity.x);
        animator.SetInteger("VelocityY", (int)velocity.y);
    }

    void PlayerDeath()
    {
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
        }
        else if (freezeTimer <= 0)
        {
            readInput = true;
            canAttack = true;

            if (deathTimer > 0)
            {
                if (flashTimer > 0)
                {
                    GetComponent<SpriteRenderer>().enabled = false;

                    flashTimer -= Time.deltaTime;
                }
                else if (flashTimer <= 0)
                {
                    GetComponent<SpriteRenderer>().enabled = true;

                    flashTimer = flashTimerStart;
                }

                deathTimer -= Time.deltaTime;
            }
            else if (deathTimer <= 0)
            {
                canGetHit = true;
                isActive = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        print(flashTimer);
    }

    //void LoadPowerUps()
    //{
    //    int count = 0;

    //    if (count < powerUps.Count)
    //    {
    //        switch (powerUps[0])
    //        {
    //            case "SpeedBoots":
    //                gameObject.AddComponent<SpeedBootsScript>();
    //                LevelManager.powerUps.Add("SpeedBoots");
    //                count++;
    //                break;

    //            default:
    //                print("No Script Found");
    //                break;
    //        }
    //    }
    //}
}
