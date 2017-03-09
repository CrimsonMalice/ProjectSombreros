using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBootsScript : MonoBehaviour {

    [SerializeField] private PlayerController pc;
    [SerializeField] private bool equiped = false;
    [SerializeField] private int speedBonus = 20;

	// Use this for initialization
	void Start ()
    {
        pc = GetComponent<PlayerController>();
        pc.speed += speedBonus;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
