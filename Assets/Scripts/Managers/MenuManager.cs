using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  	
	void Start ()
    {
		
    }


	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
        {
            LoadLevel();
            
          
        }
	} 

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level_1");        
    }

  
    public void Quit()
    {
        Application.Quit();
    }
}
