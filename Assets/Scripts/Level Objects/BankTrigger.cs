using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankTrigger : MonoBehaviour {

    /*
     * TODO:
     * - Make it that the Gameplay song is chosen at random from an array.
     */

    [SerializeField] private GameObject levelSpawner; //Holds the Prefab for the levelSpawner object + script
    [SerializeField] private GameObject whiteBlast; //Holds the Prefab for the White blast effect
    [SerializeField] private GameObject particlefx; //Holds the Prefab for the money blast effect
    [SerializeField] private AudioClip sfxBlast; //Holds the Prefab for the blast sound effect
    [SerializeField] private AudioClip gameplaySong; //Holds the Gameplay song

    [SerializeField] private bool doOnce = false;
    [SerializeField] float timer = BankWhiteFade.duration;

	// Use this for initialization
	void Start ()
    {
        LevelManager.bankDestroyed = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //When the player walks in to the Bank Trigger
        {
            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(sfxBlast, new Vector3(7, 8, 0)); //Play the blast sound
            }
            AudioPlayer.aus.clip = null; //Stop playing the idle-town track

            //Instantiate(whiteBlast, transform.position, Quaternion.identity); //Play the white blast effect
            GameObject fxinstance =  Instantiate(particlefx, transform.position, Quaternion.identity); //Play the Money blast effect
            Destroy(fxinstance, 1.30f); //Destroy it after 1.3 seconds

            print(LevelManager.bankDestroyed);

            GameObject instance = Instantiate(levelSpawner, new Vector3(0, 0, 0), Quaternion.identity); //Instantiate the levelSpawner

            LevelManager.bankDestroyed = true; //Set it so that the bank has been destroyed

            AudioPlayer.aus.clip = gameplaySong; //Set the current song to the gameplay song
            AudioPlayer.aus.Play(44000); //Play the track with a delay

            Destroy(gameObject); //Destroy the bank trigger.
        }
    }
}
