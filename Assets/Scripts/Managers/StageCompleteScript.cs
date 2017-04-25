using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCompleteScript : MonoBehaviour {

    [SerializeField] private AudioClip stageCompleteSong;
    [SerializeField] private Text killsText;
    [SerializeField] private Text MoneyText;
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
        coolIndex = Random.Range(0, coolArray.Length);
        killsText.text = "Kills: " + LevelManager.enemiesKilled;
        MoneyText.text = "Money Picked Up: " + LevelManager.moneyPickedUp;
        bombBonusText.text = coolArray[coolIndex];
        LevelManager.stageTransitLoaded = true;
        killsText.enabled = false;
        MoneyText.enabled = false;
        bombBonusText.enabled = false;

        LevelManager.enemiesKilled = 0;
        LevelManager.moneyPickedUp = 0;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-402f, 2f, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= showTextOne && killsText.enabled == false)
        {
            killsText.enabled = true;
            AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
        }

        if (timer >= showTextTwo && MoneyText.enabled == false)
        {
            MoneyText.enabled = true;
            AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
        }

        if (timer >= showTextThree && bombBonusText.enabled == false)
        {
            bombBonusText.enabled = true;
            AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.stageTransitLoaded = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().readInput = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameObject.transform.position = new Vector3(-450.4f, 245.9f, 0);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
