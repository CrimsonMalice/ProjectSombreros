using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfGreaterGreed : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MoneyPickUp>())
        {
            int pointsValue = other.gameObject.GetComponent<MoneyPickUp>().PointsValue;

            if (pointsValue >= 100 && pointsValue < 300)
            {
                gameObject.GetComponent<PlayerController>().money += 50;
            }
            else if (pointsValue >= 300 && pointsValue < 400)
            {
                gameObject.GetComponent<PlayerController>().money += 100;
            }
            else if (pointsValue >= 400 && pointsValue < 600)
            {
                gameObject.GetComponent<PlayerController>().money += 150;
            }
            else if (pointsValue >= 600 && pointsValue < 1000)
            {
                gameObject.GetComponent<PlayerController>().money += 200;
            }
        }
    }
}
