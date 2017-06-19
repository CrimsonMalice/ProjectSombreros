using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JillSingleton : MonoBehaviour {

    public static JillSingleton instance = null;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
