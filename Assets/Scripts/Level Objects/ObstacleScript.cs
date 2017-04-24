﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion" || other.gameObject.tag == "Bullet") //If colliding with an Explosion, Destroy the Game Object.
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
