using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    [SerializeField] bool move = true;
    [SerializeField] float distanceToMove = 245f;
    float idleTimer = 0f;
    float idleTimerStart = 5.5f;
    [SerializeField] float distanceMoved = 0;
    float speed = 40f;
    int dir = 1;
    Rigidbody2D rbody;
    Animator animator;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (LevelManager.bankDestroyed)
        {
            Destroy(gameObject);
        }

        if (move)
        {
            rbody.velocity = new Vector2(dir, 0) * speed;
            distanceMoved += 1;


            if (distanceMoved >= distanceToMove)
            {
                move = false;
                distanceMoved = 0;

                idleTimer = idleTimerStart;
            }
        }

        if (!move)
        {
            rbody.velocity = Vector2.zero;
            if (idleTimer > 0)
            {
                idleTimer -= Time.deltaTime;
            }
            else if (idleTimer <= 0)
            {
                if (dir == -1f)
                    GetComponent<SpriteRenderer>().flipX = true;

                if (dir == 1f)
                    GetComponent<SpriteRenderer>().flipX = false;

                dir *= -1;

                move = true;
            }
        }

        animator.SetInteger("DirX", (int)rbody.velocity.x);
    }
}
