using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopFrame;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text dialougeText;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private Text itemCostText;

    [SerializeField] public GameObject shopCanvas;
    [SerializeField] private PlayerController pc;

    [SerializeField] private List<GameObject> itemList;
    [SerializeField] private List<GameObject> itemInstanceList;
    [SerializeField] private Vector3 newPos;

    [SerializeField] private int highLightedItem = 0;

    [SerializeField] public static bool active = false;
    [SerializeField] private bool iconsActive = false;

    [SerializeField] private Sprite soldSpr;

    [SerializeField] private string soldDescription;
    [SerializeField] private string soldDialouge;

    [SerializeField] private List<int> soldItems;

    [SerializeField] private GameObject[] ItemSpots;

    [SerializeField] private GameObject itemsObject;

    [SerializeField] private GameObject powerUpEffect;

    private Vector3 framePos;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        itemInstanceList = new List<GameObject>();
        newPos = new Vector3(-524, -422, 0);

        framePos = ItemSpots[0].GetComponent<RectTransform>().position;
        shopFrame.transform.position = framePos;

        int shopItemCounter = 0;
        List<GameObject> currentItemList = new List<GameObject>(); /*= ListOfItems.listOfAllShopItems;*/

        foreach (GameObject go in Resources.LoadAll("ShopItems", typeof(GameObject)))
        {
            if (go.gameObject.GetComponent<ShopItem>().unlocked)
                currentItemList.Add(go);
        }

        do
        {
            int randomIndex = Random.Range(0, Resources.LoadAll("ShopItems").Length - shopItemCounter);
            itemList.Add(currentItemList[randomIndex]);
            currentItemList.RemoveAt(randomIndex);
            shopItemCounter++;
        }
        while (shopItemCounter < LevelManager.itemShopSlots);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (active && !iconsActive)
        {
            iconsActive = true;

            if (itemList.Count >= 1)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemInstanceList.Add(Instantiate(itemList[i], ItemSpots[i].GetComponent<RectTransform>().position, Quaternion.identity));
                    itemInstanceList[i].GetComponent<Image>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    itemInstanceList[i].transform.SetParent(itemsObject.transform);
                    //itemInstanceList[i].GetComponent<RectTransform>().anchoredPosition = ItemSpots[i].GetComponent<RectTransform>().position;

                    ////newPos += new Vector3(111, 0, 0);
                }
            }
            else if (itemList.Count == 0)
            {
                dialougeText.text = "Yeah yeah, sorry pal, we're sold out for now, you bought EVERYTHING! Come and speak to me later!";
                itemNameText.text = "";
                itemDescriptionText.text = "";
                itemCostText.text = "";
            }

            if(itemList.Count >= 1)
                UpdateText();
        }

        if (shopCanvas.activeInHierarchy)
        {
            DoInput();
        }

        //print(itemList.Count);
    }


    void DoInput()
    {
        if (!pc.readInput)
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                for (int i = 0; i < itemInstanceList.Count; i++)
                {
                    print(itemInstanceList[i].gameObject.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print(soldItems.Count);
                pc.readInput = true;

                if(soldItems.Count > 0)
                    Instantiate(powerUpEffect, pc.transform);

                if (soldItems.Count != 0)
                {
                    for (int i = 0; i < soldItems.Count; i++)
                    {
                        itemList.Remove(itemList[soldItems[i]]);
                    }
                }

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("ShopItemUI"))
                {
                    Destroy(go);
                }

                itemInstanceList.Clear();

                shopCanvas.SetActive(false);
                active = false;
                iconsActive = false;

                newPos = new Vector3(-524, -422, 0);
                highLightedItem = 0;

                shopFrame.transform.position = framePos;

                soldItems.Clear();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && highLightedItem < itemList.Count - 1)
            {
                highLightedItem++;
                shopFrame.transform.position = ItemSpots[highLightedItem].GetComponent<RectTransform>().position;

                UpdateText();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && highLightedItem > 0)
            {
                highLightedItem--;
                shopFrame.transform.position = ItemSpots[highLightedItem].GetComponent<RectTransform>().position;

                UpdateText();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && highLightedItem > 2)
            {
                highLightedItem -= 3;
                shopFrame.transform.position = ItemSpots[highLightedItem].GetComponent<RectTransform>().position;

                UpdateText();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && highLightedItem < itemList.Count - 3)
            {
                highLightedItem += 3;
                shopFrame.transform.position = ItemSpots[highLightedItem].GetComponent<RectTransform>().position;

                UpdateText();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !itemInstanceList[highLightedItem].GetComponent<ShopItem>().sold)
            {
                if (pc.money >= itemList[highLightedItem].GetComponent<ShopItem>().itemCost)
                {
                    soldItems.Add(highLightedItem);

                    //AddScript(itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().itemScript);

                    //Have a look at how to check for certain power-ups.
                    print(itemList[highLightedItem]);
                    AddPowerUp.AddPowerUpScript(itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().itemScript, itemList[highLightedItem]);

                    pc.money -= itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().itemCost;
                    itemInstanceList[highLightedItem].gameObject.GetComponent<Image>().sprite = soldSpr;
                    itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().dialougeText = soldDialouge;
                    itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().itemDescriptionText = soldDescription;
                    moneyText.text = "Money: " + pc.money.ToString();
                    itemInstanceList[highLightedItem].GetComponent<ShopItem>().sold = true;

                    dialougeText.text = itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().dialougeText;
                    itemDescriptionText.text = itemInstanceList[highLightedItem].gameObject.GetComponent<ShopItem>().itemDescriptionText = soldDescription;

                    
                }
            }
        }
    }

    void UpdateText()
    {
        dialougeText.text = itemInstanceList[highLightedItem].GetComponent<ShopItem>().dialougeText;
        itemNameText.text = itemInstanceList[highLightedItem].GetComponent<ShopItem>().itemNameText;
        itemDescriptionText.text = itemInstanceList[highLightedItem].GetComponent<ShopItem>().itemDescriptionText;
        itemCostText.text = "Cost: " + itemInstanceList[highLightedItem].GetComponent<ShopItem>().itemCost.ToString() + " $";
        moneyText.text = "Money: " + pc.money.ToString();

    }

    //void AddScript(string scriptName)
    //{
    //    switch (scriptName)
    //    {
    //        case "SpeedBoots":
    //            pc.gameObject.AddComponent<SpeedBootsScript>();
    //            LevelManager.powerUps.Add("SpeedBoots");
    //            break;

    //        default:
    //            print("No Script Found");
    //            break;
    //    }
    //}
}
