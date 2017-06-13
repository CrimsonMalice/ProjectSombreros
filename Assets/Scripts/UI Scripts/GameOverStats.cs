using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverStats : MonoBehaviour {

    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text sheriffsDefeatedText;
    [SerializeField] private Text moneyCollectedText;
    [SerializeField] private Text levelsClearedText;

    [SerializeField] private float timer; //Loads next level after 12 seconds
    [SerializeField] private float showTextOne = 3.0f;
    [SerializeField] private float showTextTwo = 4.0f;
    [SerializeField] private float showTextThree = 5.0f;
    [SerializeField] private float showTextFour = 6.0f;

    [SerializeField] private float exitMenu = 12.0f;

    [SerializeField] private AudioClip blastSFX;

	// Use this for initialization
	void Start ()
    {
        finalScoreText.text = "Final Score: " + GameStatsTracker.finalScore;
        sheriffsDefeatedText.text = "Sheriffs Defeated: " + GameStatsTracker.totalEnemiesKilled;
        moneyCollectedText.text = "Money Collected: " + GameStatsTracker.totalMoneyCollected;
        levelsClearedText.text = "Levels Cleared: " + GameStatsTracker.levelsCleared;

        finalScoreText.enabled = false;
        sheriffsDefeatedText.enabled = false;
        moneyCollectedText.enabled = false;
        levelsClearedText.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= showTextOne && finalScoreText.enabled == false)
        {
            finalScoreText.enabled = true;

            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= showTextTwo && sheriffsDefeatedText.enabled == false)
        {
            sheriffsDefeatedText.enabled = true;

            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= showTextThree && moneyCollectedText.enabled == false)
        {
            moneyCollectedText.enabled = true;

            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= showTextFour && levelsClearedText.enabled == false)
        {
            levelsClearedText.enabled = true;

            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(blastSFX, new Vector3(0, 0, -10), 2.0f);
            }
        }

        if (timer >= exitMenu)
        {
            SceneManager.LoadScene("NewMenu");
        }
    }
}
