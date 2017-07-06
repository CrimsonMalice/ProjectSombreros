using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    [SerializeField] public string dialougeText;
    [SerializeField] public string itemNameText;
    [SerializeField] public string itemDescription;
    [SerializeField] public string itemScript;

    [SerializeField] public int itemCost;
    [SerializeField] public bool sold = false;
    [SerializeField] public bool unlocked;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
