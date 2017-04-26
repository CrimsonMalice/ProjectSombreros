using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    [SerializeField] public GameObject shopCanvas;

    // Use this for initialization
    void Start()
    {
        shopCanvas = GameObject.Find("ShopCanvas2");
        shopCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (shopCanvas == null)
        //{
        //    shopCanvas = GameObject.Find("ShopCanvas2");
        //}

        if (LevelManager.bankDestroyed)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (!LevelManager.bankDestroyed)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();

            pc.readInput = false;
            pc.rbody.velocity = Vector2.zero;
            shopCanvas.SetActive(true);
            ShopMenu.active = true;
        }
    }
}
