using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] int level;
	
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
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    public void QuitGame()
    {
        Application.Quit();        
    }

    void OnGUI()
    {
        
    }
}
