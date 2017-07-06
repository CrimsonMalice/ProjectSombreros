using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCompleteScript : MonoBehaviour {

    [SerializeField] private AudioClip stageCompleteSong;
    [SerializeField] private Text killsText;
    [SerializeField] private Text moneyTextPlayerOne;
    [SerializeField] private Text bombBonusText;

    [SerializeField] private float timer; //Loads next level after 12 seconds
    [SerializeField] private float showTextOne = 3.0f;
    [SerializeField] private float showTextTwo = 4.0f;
    [SerializeField] private float showTextThree = 5.0f;

    [SerializeField] private AudioClip blastSFX;

    [SerializeField] private string[] coolArray;
    [SerializeField] private int coolIndex;
	
    // Use this for initialization
	void Start ()
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 147.9161f;
        GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = new Vector3(0, 0, -10);

        GameStatsTracker.levelsCleared++;

        if (LevelManager.playerAmount == 1)
        {
            Destroy(GameObject.Find("Jill"));
        }

        coolIndex = Random.Range(0, coolArray.Length);
        killsText.text = "Kills: " + LevelManager.enemiesKilled;
        moneyTextPlayerOne.text = "Money Picked Up: " + LevelManager.moneyPickedUp;
        bombBonusText.text = coolArray[coolIndex];
        LevelManager.stageTransitLoaded = true;
        killsText.enabled = false;
        moneyTextPlayerOne.enabled = false;
        bombBonusText.enabled = false;

        LevelManager.enemiesKilled = 0;
        LevelManager.moneyPickedUp = 0;

        GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-314f, 2f, 0);

        if (GameObject.FindGameObjectWithTag("PlayerTwo"))
        {
            GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-340f, 2f, 0);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= showTextOne && killsText.enabled == false)
        {
            killsText.enabled = true;
            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= showTextTwo && moneyTextPlayerOne.enabled == false)
        {
            moneyTextPlayerOne.enabled = true;
            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= showTextThree && bombBonusText.enabled == false)
        {
            bombBonusText.enabled = true;
            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerOne")
        {
            LevelManager.stageTransitLoaded = false;
            GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().readInput = true;
            GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-450.4f, 245.9f, 0);

            if (GameObject.FindGameObjectWithTag("PlayerTwo"))
            {
                GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().readInput = true;
                GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-450.4f, 213.9f, 0);
            }

            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 272.499f;
            GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = new Vector3(7, 8, -10);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
