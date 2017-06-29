using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] private GameObject cursor;
    [SerializeField] private int menuStateIndex = 0;
    [SerializeField] private string[] menuStates;
    [SerializeField] private string[] optionsMenuStates;
    private float cursorMoveValue = 0.93f;

    private float inputDelayTimer = 0;
    private float inputDelatTimerStart = 0.15f;

    [SerializeField] private AudioClip scrollSFX;
    [SerializeField] private AudioClip confirmSFX;

    bool inOptions = false;
    Vector3 inOptionsPos = new Vector3(6.32f, 0.52f, 0);

    GameObject optionsPanel;
    GameObject singlePlayerPanel;
    GameObject multiPlyerPanel;
    GameObject creditsPanel;
    GameObject exitPanel;

    void Start()
    {
        optionsPanel = GameObject.Find("OptionsPanel");
        singlePlayerPanel = GameObject.Find("SinglePlayer");
        multiPlyerPanel = GameObject.Find("MultiPlayer");
        creditsPanel = GameObject.Find("Credits");
        exitPanel = GameObject.Find("Exit");

        optionsPanel.SetActive(false);
    }

    public void ButtonClicked()
    {
        SceneManager.LoadScene(level);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void Update()
    {
        if (inputDelayTimer > 0)
        {
            inputDelayTimer -= Time.deltaTime;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (!inOptions)
        {
            if (Input.GetAxisRaw("Vertical1") > 0 && inputDelayTimer <= 0 && menuStateIndex > 0)
            {
                cursor.transform.position += new Vector3(0, 0.93f, 0);
                inputDelayTimer = inputDelatTimerStart;
                menuStateIndex--;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(0, 0, 0), 1.5f);
            }

            if (Input.GetAxisRaw("Vertical1") < 0 && inputDelayTimer <= 0 && menuStateIndex < 4)
            {
                cursor.transform.position -= new Vector3(0, 0.93f, 0);
                inputDelayTimer = inputDelatTimerStart;
                menuStateIndex++;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(0, 0, 0), 1.5f);
            }

            if (Input.GetButtonDown("Confirm1"))
            {
                AudioSource.PlayClipAtPoint(confirmSFX, new Vector3(0, 0, 0), 1.5f);
                DoInput(menuStates[menuStateIndex]);
            }
        }
        else if (inOptions)
        {
            if (Input.GetAxisRaw("Vertical1") > 0 && inputDelayTimer <= 0 && menuStateIndex > 0)
            {
                cursor.transform.position += new Vector3(0, 1.85f, 0);
                inputDelayTimer = inputDelatTimerStart;
                menuStateIndex--;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(0, 0, 0), 1.5f);
            }

            if (Input.GetAxisRaw("Vertical1") < 0 && inputDelayTimer <= 0 && menuStateIndex < 2)
            {
                cursor.transform.position -= new Vector3(0, 1.85f, 0);
                inputDelayTimer = inputDelatTimerStart;
                menuStateIndex++;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(0, 0, 0), 1.5f);
            }

            if (Input.GetButtonDown("Confirm1"))
            {
                AudioSource.PlayClipAtPoint(confirmSFX, new Vector3(0, 0, 0), 1.5f);
                DoInput(optionsMenuStates[menuStateIndex]);
            }
        }
    }
    

    public void QuitGame()
    {
        Application.Quit();        
    }

    void OnGUI()
    {
        
    }

    void DoInput(string currentMenuState)
    {
        string menuState = currentMenuState;

        if (!inOptions)
        {
            switch (currentMenuState)
            {
                case "Single Player":
                    StartGame();
                    break;

                case "MultiPlayer":
                    Debug.Log("Missing Menu Logic");
                    break;

                case "Options":
                    optionsPanel.SetActive(true);
                    GameObject.Find("SinglePlayer").SetActive(false);
                    GameObject.Find("MultiPlayer").SetActive(false);
                    GameObject.Find("Credits").SetActive(false);
                    GameObject.Find("Exit").SetActive(false);
                    menuStateIndex = 0;
                    inOptions = true;
                    cursor.transform.position = inOptionsPos;
                    break;

                case "Credits":
                    Debug.Log("Missing Menu Logic");
                    break;

                case "Quit Game":
                    QuitGame();
                    break;

                default:
                    Debug.Log("State Not Found!");
                    break;
            }
        }
        else if (inOptions)
        {
            switch (currentMenuState)
            {
                case "Music":
                    if (SoundManager.toggleMusic)
                    {
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().DisableMusic();
                        SoundManager.toggleMusic = false;
                    }
                    else if (!SoundManager.toggleMusic)
                    {
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().EnableMusic();
                        SoundManager.toggleMusic = true;
                    }
                    break;

                case "SFX":
                    if (SoundManager.toggleSFX)
                    {
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().DisableSFX();
                        SoundManager.toggleSFX = false;
                    }
                    else if (!SoundManager.toggleMusic)
                    {
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().EnableSFX();
                        SoundManager.toggleSFX = true;
                    }
                    break;

                case "QuitOptions":
                    Debug.Log("Missing Menu Logic");
                    optionsPanel.SetActive(false);
                    singlePlayerPanel.SetActive(true);
                    multiPlyerPanel.SetActive(true);
                    creditsPanel.SetActive(true);
                    exitPanel.SetActive(true);

                    menuStateIndex = 0;
                    inOptions = false;
                    cursor.transform.position = new Vector3(6.32f, 1.36f, 0);
                    break;


                default:
                    Debug.Log("State Not Found!");
                    break;
            }
        }
    }
}
