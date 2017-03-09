using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSystem : MonoBehaviour {

    [SerializeField] private ParticleSystem[] partSystem;

	// Use this for initialization
	void Start ()
    {
        foreach (ParticleSystem ps in partSystem)
        {
            ps.Play();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
