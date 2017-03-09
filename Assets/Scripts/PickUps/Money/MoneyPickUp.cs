using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour {

    [SerializeField] private int pointsValue;
    [SerializeField] private AudioClip pickUpClip;

    public int PointsValue
    {
        get { return this.pointsValue; }
        set { this.pointsValue = value; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            AudioSource.PlayClipAtPoint(pickUpClip, new Vector3(7, 8, -10), 1.0f);
            other.gameObject.GetComponent<PlayerController>().Points += pointsValue;
            other.gameObject.GetComponent<PlayerController>().money += pointsValue;
            print(other.gameObject.GetComponent<PlayerController>().Points);
            Destroy(gameObject);
        }
    }
}
