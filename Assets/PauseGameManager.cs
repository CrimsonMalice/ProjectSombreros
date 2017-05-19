using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    [SerializeField] public bool toggle = false;
    [SerializeField] public GameObject pauseMenuObject;

    void Start()
    {
        LevelManager.currentLevel = SceneManager.GetActiveScene().ToString();
        pauseMenuObject.SetActive(false);
    }

    void Update()
    {
        if (pauseMenuObject != null && !ShopMenu.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                toggle = !toggle;
            }

            if (toggle)
            {
                pauseMenuObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (!toggle)
            {
                pauseMenuObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void ResumeGame()
    {
        print("Pressed");

        toggle = false;

        Time.timeScale = 1f;
        pauseMenuObject.SetActive(false);
    }

    public void RestartGame()
    {
        toggle = false;
        Time.timeScale = 1f;

        Destroy(GameObject.Find("Player"));

        LevelManager.bankDestroyed = false;
        LevelManager.currentPlayerScore = 0;
        LevelManager.enemiesKilled = 0;
        LevelManager.level = 1;
        LevelManager.moneyPickedUp = 0;
        LevelManager.playerLives = 3;
        LevelManager.playerMoney = 0;
        LevelManager.requiredScore = 10000;
        LevelManager.tempCashCounter = 0;

        SceneManager.LoadScene("LoadingScene");
    }

    public void BackToMenu()
    {
        toggle = false;
        Time.timeScale = 1f;

        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("SoundManager"));

        SceneManager.LoadScene("NewMenu");
    }

    public void QuitGame()
    {
        toggle = false;
        Time.timeScale = 1f;

        Application.Quit();
    }
}
