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
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
