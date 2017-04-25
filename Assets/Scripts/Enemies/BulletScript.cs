using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField] public Vector2 direction;
    [SerializeField] private float speed = 110f;
    [SerializeField] Rigidbody2D rbody;
    [SerializeField] Animator animator;
    [SerializeField] private AudioClip shootingSound;

    [SerializeField] private GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AudioSource.PlayClipAtPoint(shootingSound, new Vector3(7, 8, -10), 1.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        rbody.velocity = new Vector2(direction.x / 100 * speed, direction.y / 100 * speed);

        animator.SetFloat("DirX", direction.x);
        animator.SetFloat("DirY", direction.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Walls" || other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //void OnCollisionEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "House" || other.gameObject.tag == "Walls" || other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Player")
    //    {
    //        Instantiate(explosion, transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}
}
