using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour 
{

    public static AudioSource aus;

	// Use this for initialization
	void Start ()
    {
        aus = GetComponent<AudioSource>();

        if (!SoundManager.toggleMusic)
        {
            aus.enabled = false;
        }
        else if (!SoundManager.toggleMusic)
        {
            aus.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
