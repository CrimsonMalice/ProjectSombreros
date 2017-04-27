using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour 
{

    public static UICanvas instance = null;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () 
	{
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
        }
	}
}
