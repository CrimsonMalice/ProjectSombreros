using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeart : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.GetComponent<PlayerController>())
        {
            gameObject.GetComponent<PlayerController>().lives++;
            Destroy(gameObject.GetComponent<ExtraHeart>());
        }
	}
}
