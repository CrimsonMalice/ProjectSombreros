using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCamera : MonoBehaviour {

    PlayerController pc;

	// Use this for initialization
	void Start ()
    {
        pc = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(pc.transform.position.x, pc.transform.position.y, -10f);
	}
}
