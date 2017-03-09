using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudScript : MonoBehaviour {

    private float originalPlayerSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().affectedByMud)
        {
            originalPlayerSpeed = other.gameObject.GetComponent<PlayerController>().speed;
            other.gameObject.GetComponent<PlayerController>().speed -= 50;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().affectedByMud)
        {
            other.gameObject.GetComponent<PlayerController>().speed = originalPlayerSpeed;
        }
    }

}
