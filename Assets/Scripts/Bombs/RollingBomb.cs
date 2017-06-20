using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBomb : MonoBehaviour {

    Rigidbody2D rbody;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionObject;


    // Use this for initialization
    void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();

        velocity = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().direction; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        rbody.velocity = velocity * speed;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Walls")
        {
            Instantiate(explosionObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
