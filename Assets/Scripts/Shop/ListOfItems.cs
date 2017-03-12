using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfItems : MonoBehaviour {

    public static List<GameObject> listOfAllShopItems;

    // Use this for initialization
    void Start ()
    {
        listOfAllShopItems = new List<GameObject>();

        foreach (GameObject go in Resources.LoadAll("ShopItems", typeof(GameObject)))
        {
            if(go.gameObject.GetComponent<ShopItem>().unlocked)
                listOfAllShopItems.Add(go);
        }

        //for (int i = 0; i < listOfAllShopItems.Count; i++)
        //{
        //    print(listOfAllShopItems[i]);
        //}
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
