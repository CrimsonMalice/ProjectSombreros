using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitStartMenu : MonoBehaviour {

    [SerializeField] private GameObject mainMenuPic;
    [SerializeField] private GameObject itemFrame;
    [SerializeField] private GameObject player;

	// Use this for initialization
	void Start ()
    {
        mainMenuPic.SetActive(false);
        itemFrame.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainMenuPic.SetActive(true);
            itemFrame.SetActive(true);

            gameObject.SetActive(false);
            player.SetActive(false);
        }
	}
}
