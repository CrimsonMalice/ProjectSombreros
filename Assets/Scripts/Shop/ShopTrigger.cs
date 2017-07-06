using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    [SerializeField] public GameObject shopCanvas;

    // Use this for initialization
    void Start()
    {
        print(LevelManager.playerAmount);

        if (GameObject.FindGameObjectWithTag("PlayerOne") && GameObject.FindGameObjectWithTag("PlayerTwo"))
        {
            shopCanvas = GameObject.Find("MultiplayerShopCanvas");
        }
        else if (GameObject.FindGameObjectWithTag("PlayerOne") && !GameObject.FindGameObjectWithTag("PlayerTwo"))
        {
            shopCanvas = GameObject.Find("ShopCanvas2");
        }

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
        if (other.gameObject.tag == "PlayerOne" || other.gameObject.tag == "PlayerTwo")
        {
            PlayerController pc;
            PlayerController pc2;
            if (GameObject.FindGameObjectWithTag("PlayerOne") && GameObject.FindGameObjectWithTag("PlayerTwo"))
            {
                pc = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();
                pc2 = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>();

                pc.readInput = false;
                pc.rbody.velocity = Vector2.zero;

                pc2.readInput = false;
                pc2.rbody.velocity = Vector2.zero;
                shopCanvas.SetActive(true);

                print(ShopMenu.active);
                ShopMenu.active = true;
                print(ShopMenu.active);
            }
            else if (GameObject.FindGameObjectWithTag("PlayerOne") && !GameObject.FindGameObjectWithTag("PlayerTwo"))
            {
                pc = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();

                pc.readInput = false;
                pc.rbody.velocity = Vector2.zero;

                shopCanvas.SetActive(true);

                print(ShopMenu.active);
                ShopMenu.active = true;
                print(ShopMenu.active);
            }
        }
    }
}
