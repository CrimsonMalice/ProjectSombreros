using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    [SerializeField] private GameObject shopCanvas;

    // Use this for initialization
    void Start()
    {
        shopCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
