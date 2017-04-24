using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheriff : MonoBehaviour {

    Rigidbody2D rbody;
    Animator animator;

    [SerializeField] private GameObject nextPos; //The current travelling destination for the Sheriff.
    [SerializeField] private GameObject oldPos; //The old travelling destination for the Sheriff.
    [SerializeField] private GameObject newDestination; //The next travelling destination for the Sheriff, generated on collision with a PatrolPoint.
    [SerializeField] [Range(0.1f, 1.5f)] private float speed; //The movement speed of the Sheriff.
    [SerializeField] private bool changeGoal; //If the Sheriff should change to the next destination.
    [SerializeField] private bool moving; //If the Sheriff is moving.
    [SerializeField] private GameObject[] pointOptions; //The selection of new destination in which the Sheriff can travel towards.

    [SerializeField] public Vector2 direction; //The moving direction of the Sheriff
    [SerializeField] private float castDistance = 750f; //How far to raycast for obstacles

    [SerializeField] private AudioClip deathSound; //The clip to play when a Sheriff dies

    [SerializeField] private LayerMask worldCollisionMask; //Which layers the Sheriff should check for objects when raycasting

    public GameObject NextPos
    {
        get { return this.nextPos; }
        set { this.nextPos = value; }
    }
    public GameObject OldPos
    {
        get { return this.oldPos; }
        set { this.oldPos = value; }
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moving = true; //Start moving at start
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //direction = new Vector2(Mathf.Sign(newDirection.x), Mathf.Sign(newDirection.y));

        if (moving)
        {
            if (transform.position == nextPos.transform.position && changeGoal) //When the Sheriff is at the exact same position as the PatrolPoint and can Change Destination:
            {
                SetNewDestination(); //Set the new Destination
                direction = SetNewDir(); //Set the new Direction
                changeGoal = false; //Can't change to a new destiantion
                moving = true; //Start moving
            }
            else //If not supposed to change destination:
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPos.transform.position, speed); //Procedd towards the PatrolPoints Position
            }
        }

        CheckCollison(); //Raycast for objects in the way of the sheriff

        //Set animation paramaters
        animator.SetFloat("DirX", direction.x);
        animator.SetFloat("DirY", direction.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion") //If colliding with an Explosion, Destroy the Sheriff.
        {
            AudioSource.PlayClipAtPoint(deathSound, new Vector3(7, 8, -10), 1.0f);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PatrolPoint") //If colliding with a PatrolPoint
        {
            pointOptions = other.gameObject.GetComponent<PatrolPoint>().AdjacentPoints; //Retrieve that point's Adjacent Points.
            changeGoal = true; //The Sheriff can not change to a new destination.

            newDestination = pointOptions[Random.Range(0, pointOptions.Length)]; //Random a new destination out of the adjacent points.

            do //Make the Sheriff never go back the same way he came, perventing lerping between two points.
            {
                newDestination = pointOptions[Random.Range(0, pointOptions.Length)];
            }
            while (newDestination.gameObject.name == oldPos.gameObject.name);
        }
    }

    void SetNewDestination() //Update the old Destintion and set the new one.
    {
        oldPos = nextPos;
        nextPos = newDestination;
    }

    void CheckCollison()
    {
        //Cast a Ray from the Sheriffs position in the moving direction, with a set castDistance and according to the WorldCollisionMask
        RaycastHit2D frontHit = Physics2D.Raycast(transform.position, direction, castDistance * Time.fixedDeltaTime, worldCollisionMask);

        //Debug.DrawRay(transform.position, direction * castDistance * Time.fixedDeltaTime, Color.red);

        //If there is a valid hit and there is a collider on the object, then switch so that the Sheriff will go back from where he came.
        if (frontHit.collider != null)
        {
            GameObject tempPos; //Create a container to switch around the destinations
            tempPos = oldPos; //Store down the "old" oldPos
            oldPos = nextPos; //The store the current nextPos down in oldPos
            nextPos = tempPos; //Then set the nextPos to be the "old" oldPos.

            direction = SetNewDir(); //Update the Sheriff's direction
        }

    }

    Vector2 SetNewDir()
    {
        //By subtracting the nextPos with our current position, we'll get a new vection which is the direction and distance, between the nextPos and the Sheriff.
        Vector2 newDirection = nextPos.transform.position - transform.position;

        //Then we cap it so that the direction can never be anything but -1, 0 and 1.
        if (newDirection.x == 0)
        {
            return new Vector2(0, newDirection.y);
        }

        if (newDirection.y == 0)
        {
            return new Vector2(newDirection.x, 0);
        }

        if (newDirection.x >= 1)
        {
            return new Vector2(1, newDirection.y);
        }
        else if (newDirection.x <= -1)
        {
            return new Vector2(-1, newDirection.y);
        }

        if (newDirection.y >= 1)
        {
            return new Vector2(newDirection.x, 1);
        }
        else if (newDirection.y <= -1)
        {
            return new Vector2(newDirection.x, -1);
        }
        else
        {
            return Vector2.zero;
        }
    }
}
