using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopFramePlayerOne;
    [SerializeField] private GameObject shopFramePlayerTwo;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text dialougeText;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private Text itemCostText;

    [SerializeField] public GameObject shopCanvas;
    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerController pc2;

    [SerializeField] private List<GameObject> itemList;
    [SerializeField] private List<GameObject> itemInstanceList;

    [SerializeField] private Vector3 newPosPlayerOne;

    [SerializeField] private int highLightedItemPlayerOne = 0;
    [SerializeField] private int highLightedItemPlayerTwo = 0;

    [SerializeField] public static bool active = false;
    [SerializeField] private bool iconsActive = false;

    [SerializeField] private Sprite soldSpr;

    [SerializeField] private string soldDescription;
    [SerializeField] private string soldDialouge;

    [SerializeField] private List<int> soldItems;

    [SerializeField] private GameObject[] ItemSpotsPlayerOne;
    [SerializeField] private GameObject[] ItemSpotsPlayerTwo;

    [SerializeField] private GameObject itemsObjectPlayerOne;
    [SerializeField] private GameObject itemsObjectPlayerTwo;

    [SerializeField] private GameObject powerUpEffect;

    [SerializeField] private AudioClip scrollSFX;
    [SerializeField] private AudioClip purchaseDeniedSFX;
    [SerializeField] private AudioClip purchaseAcceptedSFX;

    private Vector3 framePosPlayerOne;
    private Vector3 framePosPlayerTwo;

    float inputDelayTimerPlayerOne = 0;
    float inputDelayTimerPlayerTwo = 0;
    float inputDelayTimerStart = 0.15f;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();
        pc2 = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>();

        itemInstanceList = new List<GameObject>();
        newPosPlayerOne = new Vector3(-524, -422, 0);

        framePosPlayerOne = ItemSpotsPlayerOne[0].GetComponent<RectTransform>().position;
        shopFramePlayerOne.transform.position = framePosPlayerOne;

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
    void Update()
    {
        active = true;

        if (active && !iconsActive)
        {
            iconsActive = true;

            print(itemList.Count);

            if (itemList.Count >= 1)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    print("Calling");
                    itemInstanceList.Add(Instantiate(itemList[i], ItemSpotsPlayerOne[i].GetComponent<RectTransform>().position, Quaternion.identity));
                    itemInstanceList[i].GetComponent<Image>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    itemInstanceList[i].transform.SetParent(itemsObjectPlayerOne.transform);
                    print("Call Done");
                    //itemInstanceList[i].GetComponent<RectTransform>().anchoredPosition = ItemSpotsPlayerOne[i].GetComponent<RectTransform>().position;

                    ////newPosPlayerOne += new Vector3(111, 0, 0);
                }
            }
            else if (itemList.Count == 0)
            {
                print("Calling");
                dialougeText.text = "Yeah yeah, sorry pals, we're sold out for now, you bought EVERYTHING! Come and speak to me later!";
                itemNameText.text = "";
                itemDescriptionText.text = "";
                itemCostText.text = "";
            }

            if (itemList.Count >= 1)
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
            if (inputDelayTimerPlayerOne > 0)
            {
                inputDelayTimerPlayerOne -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                for (int i = 0; i < itemInstanceList.Count; i++)
                {
                    print(itemInstanceList[i].gameObject.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Bomb" + pc.playerIndex))
            {
                print(soldItems.Count);
                pc.readInput = true;

                if (soldItems.Count > 0)
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

                shopFramePlayerOne.GetComponent<Image>().rectTransform.position = framePosPlayerOne;
                shopCanvas.SetActive(false);
                active = false;
                iconsActive = false;

                newPosPlayerOne = new Vector3(-524, -422, 0);
                highLightedItemPlayerOne = 0;

                soldItems.Clear();
            }

            if (Input.GetAxisRaw("Horizontal1") > 0 && highLightedItemPlayerOne < itemList.Count - 1 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne++;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText();
            }
            else if (Input.GetAxisRaw("Horizontal1") < 0 && highLightedItemPlayerOne > 0 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne--;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText();
            }
            else if (Input.GetAxisRaw("Vertical1") > 0 && highLightedItemPlayerOne > 2 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne -= 3;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText();
            }
            else if (Input.GetAxisRaw("Vertical1") < 0 && highLightedItemPlayerOne < itemList.Count - 3 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne += 3;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold || Input.GetButtonDown("Confirm" + pc.playerIndex) && !itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold)
            {
                if (pc.money >= itemList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemCost)
                {
                    soldItems.Add(highLightedItemPlayerOne);

                    //AddScript(itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemScript);

                    //Have a look at how to check for certain power-ups.
                    print(itemList[highLightedItemPlayerOne]);
                    AudioSource.PlayClipAtPoint(purchaseAcceptedSFX, new Vector3(7, 8, -10), 1.0f);
                    AddPowerUp.AddPowerUpScript(itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemScript, itemList[highLightedItemPlayerOne], pc.gameObject);

                    pc.money -= itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemCost;
                    itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<Image>().sprite = soldSpr;
                    itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().dialougeText = soldDialouge;
                    itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemDescriptionText = soldDescription;
                    moneyText.text = "Money: " + pc.money.ToString();
                    itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold = true;

                    dialougeText.text = "Score! Another one sold to the SUCKE.. Fine lad!";
                    itemDescriptionText.text = itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemDescriptionText = soldDescription;


                }
                else if (pc.money <= itemList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemCost)
                {
                    AudioSource.PlayClipAtPoint(purchaseDeniedSFX, new Vector3(7, 8, -10), 1.0f);
                    dialougeText.text = "You trying to cheat me you little bastards??? NOT ENOUGH CASH! Comprende?";
                }
            }
        }
    }

    void UpdateText()
    {
        dialougeText.text = "Found something cool? Well, you better..";
        itemNameText.text = itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemNameText;
        itemDescriptionText.text = itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemDescriptionText;
        itemCostText.text = "Cost: " + itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemCost.ToString() + " $";
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
