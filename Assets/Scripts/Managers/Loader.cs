using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject lvlManager;
    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (LevelManager.instance == null)
            Instantiate(lvlManager);
    }
}
