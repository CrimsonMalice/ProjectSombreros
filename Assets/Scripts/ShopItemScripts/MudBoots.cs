using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudBoots : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<PlayerController>().affectedByMud = false;	
	}
}
