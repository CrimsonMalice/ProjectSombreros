using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public static bool toggleSFX = true;
    public static bool toggleMusic = true;

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

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DisableSFX()
    {
        toggleSFX = false;
        print(toggleSFX);
    }

    public void EnableSFX()
    {
        toggleSFX = true;
        print(toggleSFX);
    }

    public void DisableMusic()
    {
        toggleMusic = false;
        print(toggleMusic);
    }

    public void EnableMusic()
    {
        toggleMusic = true;
        print(toggleMusic);
    }
}
