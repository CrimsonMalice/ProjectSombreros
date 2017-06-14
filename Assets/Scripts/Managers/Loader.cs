using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject lvlManager;
    public static Camera instance = null;

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (LevelManager.instance == null)
            Instantiate(lvlManager);

        if (instance == null)
            instance = gameObject.GetComponent<Camera>();

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        //if (powerUps == null)
        //    powerUps = new List<string>();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "NewMenu")
        {
            Destroy(gameObject);
        }
    }
}
