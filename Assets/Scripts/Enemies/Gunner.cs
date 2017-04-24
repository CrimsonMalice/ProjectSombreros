using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour {

    [SerializeField] private LayerMask worldCollisionMask; //Which layers the Sheriff should check for objects when raycasting
    [SerializeField] private float castDistance = 2000f; //How far to raycast for obstacles

    [SerializeField] private float shootingTimer = 0;
    [SerializeField] private float shootingTimerStart = 3.666f;

    [SerializeField] private Sheriff sheriffScript;
    [SerializeField] private GameObject bullet;

	// Use this for initialization
	void Start ()
    {
        sheriffScript = GetComponent<Sheriff>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckCollison();

        if (shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
        }

        //Debug.DrawRay(transform.position, sheriffScript.direction * castDistance, Color.red);
    }

    void CheckCollison()
    {
        //Cast a Ray from the Sheriffs position in the moving direction, with a set castDistance and according to the WorldCollisionMask
        RaycastHit2D frontHit = Physics2D.Raycast(transform.position, sheriffScript.direction, castDistance * Time.fixedDeltaTime, worldCollisionMask);

        //If there is a valid hit and there is a collider on the object, then switch so that the Sheriff will go back from where he came.
        if (frontHit.collider != null && shootingTimer <= 0)
        {
            Debug.LogError("Hit");
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

            newBullet.GetComponent<BulletScript>().direction = sheriffScript.direction;

            shootingTimer = shootingTimerStart;
        }

    }
}
