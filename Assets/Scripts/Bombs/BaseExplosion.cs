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
    [SerializeField] private AudioClip explosionSound;

    // Use this for initialization
    void Start ()
    {
        if (SoundManager.toggleSFX)
        {
            AudioSource.PlayClipAtPoint(explosionSound, new Vector3(7, 8, -10), 1.0f);
        }
        Destroy(gameObject, lifeSpan); //Destroy the explosion after a certain time
	}
}
