using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour {

    [SerializeField] GameObject floatText;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 2.5f);
        GameObject instance = Instantiate(floatText, transform.position, Quaternion.identity);

        instance.GetComponent<MoneyFloatText>().moneyAmount = "POWER UP!";

        //instance.transform.SetParent(GameObject.Find("PlayerOne").transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
