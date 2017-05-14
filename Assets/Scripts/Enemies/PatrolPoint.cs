using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour {

    [SerializeField] private List<GameObject> adjacentPoints; //Adjacent points to this PatrolPoint. Set these in the Unity inspector.

    public List<GameObject> AdjacentPoints { get { return this.adjacentPoints; } }
    private List<float> adjacentPointsDistanceX = new List<float>();
    private List<float> adjacentPointsDistanceY = new List<float>();
    private List<float> adjacentNegativePointsDistanceX = new List<float>();
    private List<float> adjacentNegativePointsDistanceY = new List<float>();
    private float pointsAmount = 2;

    bool minusX = false;
    bool minusY = false;

    // Use this for initialization
    void Start ()
    {
        List<GameObject> patrolPoints = new List<GameObject>();

        //Find all of the PatrolPoints with the same X and Y values.
        //foreach (GameObject pp in GameObject.FindGameObjectsWithTag("PatrolPoint"))
        //{
        //    if (pp.transform.position.x == gameObject.transform.position.x)
        //    {
        //        patrolPoints.Add(pp);
        //    }
        //    else if (pp.transform.position.y == gameObject.transform.position.y)
        //    {
        //        patrolPoints.Add(pp);
        //    }
        //}

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            if (gameObject.name == patrolPoints[i].gameObject.name)
            {
                patrolPoints.RemoveAt(i);
            }
        }

        adjacentPoints = patrolPoints; //REMOVE AFTER CODE ABOVE IS FINISHED
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
