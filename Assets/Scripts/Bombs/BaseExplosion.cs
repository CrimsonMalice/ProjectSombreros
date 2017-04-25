using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExplosion : MonoBehaviour {

    /* The purpose of BaseExplosion is to implement the basic behaviours of a basic explosion.
     * So that scripters can easily edit and add extra behaviours if neccesary.
     * Go nuts!
     */

    [SerializeField] private float lifeSpan; //How long the explosion should be active
    private int collisionCount = 0;

    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, lifeSpan); //Destroy the explosion after a certain time
	}
}
