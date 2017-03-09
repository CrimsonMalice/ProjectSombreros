using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour {

    [SerializeField] private GameObject[] adjacentPoints; //Adjacent points to this PatrolPoint. Set these in the Unity inspector.

    public GameObject[] AdjacentPoints { get { return this.adjacentPoints; } }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
