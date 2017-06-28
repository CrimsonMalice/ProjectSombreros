using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : MonoBehaviour {

    /* The purpose of BaseBomb is to implement the basic behaviours of a basic bomb.
     * So that scripters can easily edit and add extra behaviours if neccesary.
     * Go nuts!
     */
    

    [SerializeField] bool hasExploded; //If the bomb has exploded yet
    [SerializeField] float bombTimer; //The active ticking timer for the bomb
    [SerializeField] float bombTimerStart; //At what number the bombTimer should start ticking down from
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private AudioClip fuseSFX;

    public bool HasExploded { get { return this.hasExploded; } }
    public float BombTimerStart { get { return this.bombTimerStart; } }

    // Use this for initialization
    void Start ()
    {
        AudioSource.PlayClipAtPoint(fuseSFX, new Vector3(7, 8, -10), 1.0f);
        bombTimer = bombTimerStart; //When the bomb is created, set the timer to the starting value
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bombTimer <= 0) //If the bombTimer is 0 or below, make it explode.
        {
            bombTimer = 0;
            hasExploded = true;
        }
        else if (bombTimer > 0) //Else if the bombTimer is above 0, make it tick down with deltaTime.
        {
            bombTimer -= Time.deltaTime;
        }

        if (hasExploded) //If the bomb has exploded, Instantiate an Explosion object at the bombs position.
        {
            Instantiate(explosionObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
}
