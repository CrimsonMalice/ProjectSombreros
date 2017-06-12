using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankArrow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (LevelManager.bankDestroyed)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            //print(LevelManager.bankDestroyed);
        }
    }
}
