using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

//[RequireComponent(typeof(MovieTexture))]
//[RequireComponent(typeof(CanvasRenderer))]
//[RequireComponent(typeof(AudioSource))]
//[RequireComponent(typeof(Renderer))]
public class StartMovie : MonoBehaviour 
{

	// Use this for initialization
	void Start ()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        //Renderer renderer = GetComponent<Renderer>();
        //MovieTexture movie = (MovieTexture)renderer.material.mainTexture;

    }
	
	
}
