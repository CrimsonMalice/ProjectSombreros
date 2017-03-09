using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPlate : MonoBehaviour {

    [SerializeField] public float immortalTimer;
    [SerializeField] [Range(2.5f, 7.5f)] public float immortalTimerStart = 3.5f;
    [SerializeField] float flashTimer = 0f;
    [SerializeField] float flashTimerStart = 0.01f;
    [SerializeField] private bool flash = false;

    void Start()
    {
        gameObject.GetComponent<PlayerController>().canTakeExtraHit = true;
    }

    void Update()
    {
        if (flash)
        {
            if (immortalTimer > 0)
            {
                if (flashTimer > 0)
                {
                    GetComponent<SpriteRenderer>().enabled = false;

                    flashTimer -= Time.deltaTime;
                }
                else if (flashTimer <= 0)
                {
                    GetComponent<SpriteRenderer>().enabled = true;

                    flashTimer = flashTimerStart;
                }

                immortalTimer -= Time.deltaTime;
            }
            else if (immortalTimer <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<PlayerController>().canTakeExtraHit = false;
                flash = false;

                Destroy(gameObject.GetComponent<MetalPlate>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && gameObject.GetComponent<PlayerController>().canTakeExtraHit)
        {
            AudioSource.PlayClipAtPoint(gameObject.GetComponent<PlayerController>().deathSound, new Vector3(7, 8, -10), 1.0f);
            flash = true;
            immortalTimer = immortalTimerStart;
        }
    }
}
