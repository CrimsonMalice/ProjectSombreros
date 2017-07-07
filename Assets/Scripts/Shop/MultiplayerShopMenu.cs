using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopFramePlayerOne;
    [SerializeField] private GameObject shopFramePlayerTwo;

    [SerializeField] private Text moneyTextPlayerOne;
    [SerializeField] private Text moneyTextPlayerTwo;
    [SerializeField] private Text dialougeText;
    [SerializeField] private Text itemNameTextPlayerOne;
    [SerializeField] private Text itemNameTextPlayerTwo;
    [SerializeField] private Text itemDescriptionTextPlayerOne;
    [SerializeField] private Text itemDescriptionTextPlayerTwo;
    [SerializeField] private Text itemCostTextPlayerOne;
    [SerializeField] private Text itemCostTextPlayerTwo;

    [SerializeField] public GameObject shopCanvas;
    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerController pc2;

    [SerializeField] private List<GameObject> itemList;
    [SerializeField] private List<GameObject> itemList2;
    [SerializeField] private List<GameObject> itemInstanceList;
    [SerializeField] private List<GameObject> itemInstanceList2;

    [SerializeField] private Vector3 newPosPlayerOne;
    [SerializeField] private Vector3 newPosPlayerTwo;

    [SerializeField] private int highLightedItemPlayerOne = 0;
    [SerializeField] private int highLightedItemPlayerTwo = 0;

    [SerializeField] public static bool active = false;
    [SerializeField] private bool iconsActive = false;

    [SerializeField] private Sprite soldSpr;

    [SerializeField] private string soldDescription;
    [SerializeField] private string soldDialouge;

    [SerializeField] private List<GameObject> soldItems;
    [SerializeField] private List<GameObject> soldItems2;

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
        newPosPlayerTwo = new Vector3(-524, -422, 0);

        framePosPlayerOne = ItemSpotsPlayerOne[0].GetComponent<RectTransform>().position;
        framePosPlayerTwo = ItemSpotsPlayerTwo[0].GetComponent<RectTransform>().position;
        shopFramePlayerOne.transform.position = framePosPlayerOne;
        shopFramePlayerTwo.transform.position = framePosPlayerTwo;

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
            itemList2.Add(currentItemList[randomIndex]);
            currentItemList.RemoveAt(randomIndex);
            shopItemCounter++;
        }
        while (shopItemCounter < LevelManager.itemShopSlots);
    }

    // Update is called once per frame
    void Update()
    {
        active = true;

        //Re-instasiate the arrays every time you load the chat.

        if (active && !iconsActive)
        {
            iconsActive = true;

            if (itemList.Count >= 1 || itemList2.Count >= 1)
            {
                if (itemList.Count >= 1)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        itemInstanceList.Add(Instantiate(itemList[i], ItemSpotsPlayerOne[i].GetComponent<RectTransform>().position, Quaternion.identity));
                        itemInstanceList[i].GetComponent<Image>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        itemInstanceList[i].transform.SetParent(itemsObjectPlayerOne.transform);
                        //itemInstanceList[i].GetComponent<RectTransform>().anchoredPosition = ItemSpotsPlayerOne[i].GetComponent<RectTransform>().position;

                        ////newPosPlayerOne += new Vector3(111, 0, 0);
                    }
                    UpdateText(dialougeText, itemNameTextPlayerOne, itemCostTextPlayerOne, itemDescriptionTextPlayerOne, moneyTextPlayerOne, itemInstanceList, highLightedItemPlayerOne);
                }

                if (itemList2.Count >= 1)
                {
                    for (int i = 0; i < itemList2.Count; i++)
                    {
                        itemInstanceList2.Add(Instantiate(itemList2[i], ItemSpotsPlayerTwo[i].GetComponent<RectTransform>().position, Quaternion.identity));
                        itemInstanceList2[i].GetComponent<Image>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        itemInstanceList2[i].transform.SetParent(itemsObjectPlayerTwo.transform);
                        //itemInstanceList[i].GetComponent<RectTransform>().anchoredPosition = ItemSpotsPlayerOne[i].GetComponent<RectTransform>().position;

                        ////newPosPlayerOne += new Vector3(111, 0, 0);
                    }
                    UpdateText(dialougeText, itemNameTextPlayerTwo, itemCostTextPlayerTwo, itemDescriptionTextPlayerTwo, moneyTextPlayerTwo, itemInstanceList2, highLightedItemPlayerTwo);
                }
            }
            else if (itemList.Count == 0 && itemList2.Count == 0)
            {
                print("Calling");
                dialougeText.text = "Yeah yeah, sorry pals, we're sold out for now, you bought EVERYTHING! Come and speak to me later!";
                itemNameTextPlayerOne.text = "";
                itemDescriptionTextPlayerOne.text = "";
                itemCostTextPlayerOne.text = "";

                itemNameTextPlayerTwo.text = "";
                itemDescriptionTextPlayerTwo.text = "";
                itemCostTextPlayerTwo.text = "";
            }
        }

        if (shopCanvas.activeInHierarchy)
        {
            DoInput();
            DoInput2();
            //Read Input Player 1

            
        }

        //print(itemList.Count);
    }


    void DoInput()
    {
        if (!pc.readInput)
        {
            print(inputDelayTimerPlayerOne);
            if (inputDelayTimerPlayerOne > 0)
            {
                inputDelayTimerPlayerOne -= Time.deltaTime;
            }

            //if (Input.GetKeyDown(KeyCode.F2))
            //{
            //    for (int i = 0; i < itemInstanceList.Count; i++)
            //    {
            //        print(itemInstanceList[i].gameObject.name);
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Bomb" + pc.playerIndex))
            {
                //DO THIS FOR BOTH PLAYERS!!!!!

                pc.readInput = true;
                pc2.readInput = true;

                if (soldItems.Count > 0)
                {
                    Instantiate(powerUpEffect, pc.transform);
                }

                if (soldItems2.Count > 0)
                {
                    Instantiate(powerUpEffect, pc2.transform);
                }

                //if (soldItems.Count != 0)
                //{
                //    for (int i = 0; i < soldItems.Count; i++)
                //    {
                //        for (int j = 0; j < itemList.Count; j++)
                //        {
                //            if (soldItems[i].name == itemList[j].name)
                //            {
                //                itemList.RemoveAt(j);
                //            }
                //        }
                //    }
                //}

                //if (soldItems2.Count != 0)
                //{
                //    for (int i = 0; i < soldItems2.Count; i++)
                //    {
                //        for (int j = 0; j < itemList2.Count; j++)
                //        {
                //            if (soldItems2[i].name == itemList2[j].name)
                //            {
                //                itemList2.RemoveAt(j);
                //            }
                //        }
                //    }
                //}

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("ShopItemUI"))
                {
                    Destroy(go);
                }

                itemInstanceList.Clear();
                itemInstanceList2.Clear();

                shopFramePlayerOne.GetComponent<Image>().rectTransform.position = framePosPlayerOne;
                shopFramePlayerTwo.GetComponent<Image>().rectTransform.position = framePosPlayerTwo;
                shopCanvas.SetActive(false);
                active = false;
                iconsActive = false;

                newPosPlayerOne = new Vector3(-524, -422, 0);
                newPosPlayerTwo = new Vector3(-524, -422, 0);
                highLightedItemPlayerOne = 0;
                highLightedItemPlayerTwo = 0;

                soldItems.Clear();
                soldItems2.Clear();
            }

            if (Input.GetAxisRaw("Horizontal1") > 0 && highLightedItemPlayerOne < itemInstanceList.Count - 1 && inputDelayTimerPlayerOne <= 0 ||
                Input.GetButtonDown("Right1") && highLightedItemPlayerOne < itemInstanceList.Count - 1 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne++;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerOne, itemCostTextPlayerOne, itemDescriptionTextPlayerOne, moneyTextPlayerOne, itemInstanceList, highLightedItemPlayerOne);
            }
            else if (Input.GetAxisRaw("Horizontal1") < 0 && highLightedItemPlayerOne > 0 && inputDelayTimerPlayerOne <= 0 ||
                Input.GetButtonDown("Left1") && highLightedItemPlayerOne > 0 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne--;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerOne, itemCostTextPlayerOne, itemDescriptionTextPlayerOne, moneyTextPlayerOne, itemInstanceList, highLightedItemPlayerOne);
            }
            else if (Input.GetAxisRaw("Vertical1") > 0 && highLightedItemPlayerOne > 2 && inputDelayTimerPlayerOne <= 0 ||
                Input.GetButtonDown("Up1") && highLightedItemPlayerOne > 2 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne -= 3;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerOne, itemCostTextPlayerOne, itemDescriptionTextPlayerOne, moneyTextPlayerOne, itemInstanceList, highLightedItemPlayerOne);
            }
            else if (Input.GetAxisRaw("Vertical1") < 0 && highLightedItemPlayerOne < itemInstanceList.Count - 3 && inputDelayTimerPlayerOne <= 0 ||
                Input.GetButtonDown("Down1") && highLightedItemPlayerOne < itemInstanceList.Count - 3 && inputDelayTimerPlayerOne <= 0)
            {
                highLightedItemPlayerOne += 3;
                shopFramePlayerOne.transform.position = ItemSpotsPlayerOne[highLightedItemPlayerOne].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerOne = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerOne, itemCostTextPlayerOne, itemDescriptionTextPlayerOne, moneyTextPlayerOne, itemInstanceList, highLightedItemPlayerOne);
            }

            if (itemInstanceList.Count != 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl) && !itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold || Input.GetButtonDown("Confirm" + pc.playerIndex) && !itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold)
                {
                    inputDelayTimerPlayerOne = inputDelayTimerStart;
                    if (pc.money >= itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemCost)
                    {
                        soldItems.Add(itemInstanceList[highLightedItemPlayerOne]);

                        //AddScript(itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemScript);

                        //Have a look at how to check for certain power-ups.
                        print(itemInstanceList[highLightedItemPlayerOne]);
                        AudioSource.PlayClipAtPoint(purchaseAcceptedSFX, new Vector3(7, 8, -10), 1.0f);
                        AddPowerUp.AddPowerUpScript(itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemScript, itemInstanceList[highLightedItemPlayerOne], pc.gameObject);

                        for (int i = 0; i < itemList.Count; i++)
                        {
                            if (itemList[i].GetComponent<ShopItem>().itemNameText == soldItems[0].GetComponent<ShopItem>().itemNameText)
                            {
                                itemList.RemoveAt(i);
                                soldItems.RemoveAt(0);

                                break;
                            }
                        }

                        pc.money -= itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemCost;
                        itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<Image>().sprite = soldSpr;
                        itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().dialougeText = soldDialouge;
                        itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemDescription = soldDescription;
                        moneyTextPlayerOne.text = "Money: " + pc.money.ToString();
                        itemInstanceList[highLightedItemPlayerOne].GetComponent<ShopItem>().sold = true;

                        dialougeText.text = "Score! Another one sold to the SUCKE.. Fine lad!";
                        itemDescriptionTextPlayerOne.text = itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemDescription = soldDescription;
                    }
                    else if (pc.money <= itemList[highLightedItemPlayerOne].GetComponent<ShopItem>().itemCost)
                    {
                        AudioSource.PlayClipAtPoint(purchaseDeniedSFX, new Vector3(7, 8, -10), 1.0f);
                        dialougeText.text = "You trying to cheat me you little bastards??? NOT ENOUGH CASH! Comprende?";
                    }

                }
            }
        }
    }

    void DoInput2()
    {
        if (!pc2.readInput)
        {
            if (inputDelayTimerPlayerTwo > 0)
            {
                inputDelayTimerPlayerTwo -= Time.deltaTime;
            }

            //if (Input.GetKeyDown(KeyCode.F2))
            //{
            //    for (int i = 0; i < itemInstanceList.Count; i++)
            //    {
            //        print(itemInstanceList[i].gameObject.name);
            //    }
            //}
            if (/*Input.GetKeyDown(KeyCode.Escape) || */Input.GetButtonDown("Bomb" + pc2.playerIndex))
            {
                //DO THIS FOR BOTH PLAYERS!!!!!

                pc.readInput = true;
                pc2.readInput = true;

                if (soldItems.Count > 0)
                {
                    Instantiate(powerUpEffect, pc.transform);
                }

                if (soldItems2.Count > 0)
                {
                    Instantiate(powerUpEffect, pc2.transform);
                }

                //if (soldItems.Count != 0)
                //{
                //    for (int i = 0; i < soldItems.Count; i++)
                //    {
                //        for (int j = 0; j < itemList.Count; j++)
                //        {
                //            if (soldItems[i].name == itemList[j].name)
                //            {
                //                itemList.RemoveAt(j);
                //            }
                //        }
                //    }
                //}

                //if (soldItems2.Count != 0)
                //{
                //    for (int i = 0; i < itemList2.Count; i++)
                //    {
                //        for (int j = 0; j < soldItems2.Count; j++)
                //        {
                //            if (soldItems2[j].name == itemList2[i].name)
                //            {
                //                itemList2.RemoveAt(i);
                //            }
                //        }
                //    }
                //}

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("ShopItemUI"))
                {
                    Destroy(go);
                }

                itemInstanceList.Clear();
                itemInstanceList2.Clear();

                shopFramePlayerOne.GetComponent<Image>().rectTransform.position = framePosPlayerOne;
                shopFramePlayerTwo.GetComponent<Image>().rectTransform.position = framePosPlayerTwo;
                shopCanvas.SetActive(false);
                active = false;
                iconsActive = false;

                newPosPlayerOne = new Vector3(-524, -422, 0);
                newPosPlayerTwo = new Vector3(-524, -422, 0);
                highLightedItemPlayerOne = 0;
                highLightedItemPlayerTwo = 0;

                soldItems.Clear();
                soldItems2.Clear();
            }

            if (Input.GetAxisRaw("Horizontal2") > 0 && highLightedItemPlayerTwo < itemInstanceList2.Count - 1 && inputDelayTimerPlayerTwo <= 0 ||
                Input.GetButtonDown("Right2") && highLightedItemPlayerTwo < itemInstanceList2.Count - 1 && inputDelayTimerPlayerTwo <= 0)
            {
                highLightedItemPlayerTwo++;
                shopFramePlayerTwo.transform.position = ItemSpotsPlayerTwo[highLightedItemPlayerTwo].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerTwo = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerTwo, itemCostTextPlayerTwo, itemDescriptionTextPlayerTwo, moneyTextPlayerTwo, itemInstanceList2, highLightedItemPlayerTwo);
            }
            else if (Input.GetAxisRaw("Horizontal2") < 0 && highLightedItemPlayerTwo > 0 && inputDelayTimerPlayerTwo <= 0 ||
                Input.GetButtonDown("Left2") && highLightedItemPlayerTwo > 0 && inputDelayTimerPlayerTwo <= 0)
            {
                highLightedItemPlayerTwo--;
                shopFramePlayerTwo.transform.position = ItemSpotsPlayerTwo[highLightedItemPlayerTwo].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerTwo = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerTwo, itemCostTextPlayerTwo, itemDescriptionTextPlayerTwo, moneyTextPlayerTwo, itemInstanceList2, highLightedItemPlayerTwo);
            }
            else if (Input.GetAxisRaw("Vertical2") > 0 && highLightedItemPlayerTwo > 2 && inputDelayTimerPlayerTwo <= 0 ||
                Input.GetButtonDown("Up2") && highLightedItemPlayerTwo > 2 && inputDelayTimerPlayerTwo <= 0)
            {
                highLightedItemPlayerTwo -= 3;
                shopFramePlayerTwo.transform.position = ItemSpotsPlayerTwo[highLightedItemPlayerTwo].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerTwo = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerTwo, itemCostTextPlayerTwo, itemDescriptionTextPlayerTwo, moneyTextPlayerTwo, itemInstanceList2, highLightedItemPlayerTwo);
            }
            else if (Input.GetAxisRaw("Vertical2") < 0 && highLightedItemPlayerTwo < itemInstanceList2.Count - 3 && inputDelayTimerPlayerTwo <= 0 ||
                Input.GetButtonDown("Down2") && highLightedItemPlayerTwo < itemInstanceList2.Count - 3 && inputDelayTimerPlayerTwo <= 0)
            {
                highLightedItemPlayerTwo += 3;
                shopFramePlayerTwo.transform.position = ItemSpotsPlayerTwo[highLightedItemPlayerTwo].GetComponent<RectTransform>().position;
                inputDelayTimerPlayerTwo = inputDelayTimerStart;

                AudioSource.PlayClipAtPoint(scrollSFX, new Vector3(7, 8, -10), 1.0f);

                UpdateText(dialougeText, itemNameTextPlayerTwo, itemCostTextPlayerTwo, itemDescriptionTextPlayerTwo, moneyTextPlayerTwo, itemInstanceList2, highLightedItemPlayerTwo);
            }

            if (itemInstanceList2.Count != 0)
            {
                if (Input.GetKeyDown(KeyCode.RightShift) && !itemInstanceList2[highLightedItemPlayerTwo].GetComponent<ShopItem>().sold || Input.GetButtonDown("Confirm" + pc2.playerIndex) && !itemInstanceList[highLightedItemPlayerTwo].GetComponent<ShopItem>().sold)
                {
                    if (itemInstanceList2[highLightedItemPlayerTwo] != null)
                    {
                        if (pc2.money >= itemInstanceList2[highLightedItemPlayerTwo].GetComponent<ShopItem>().itemCost)
                        {
                            inputDelayTimerPlayerTwo = inputDelayTimerStart;
                            soldItems2.Add(itemInstanceList2[highLightedItemPlayerTwo]);

                            //AddScript(itemInstanceList[highLightedItemPlayerOne].gameObject.GetComponent<ShopItem>().itemScript);

                            //Have a look at how to check for certain power-ups.
                            print(itemList2[highLightedItemPlayerTwo]);
                            AudioSource.PlayClipAtPoint(purchaseAcceptedSFX, new Vector3(7, 8, -10), 1.0f);
                            AddPowerUp.AddPowerUpScript(itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<ShopItem>().itemScript, itemInstanceList2[highLightedItemPlayerTwo], pc2.gameObject);

                            for (int i = 0; i < itemList2.Count; i++)
                            {
                                if (itemList2[i].GetComponent<ShopItem>().itemNameText == soldItems2[0].GetComponent<ShopItem>().itemNameText)
                                {
                                    itemList2.RemoveAt(i);
                                    soldItems2.RemoveAt(0);

                                    break;
                                }
                            }

                            pc2.money -= itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<ShopItem>().itemCost;
                            itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<Image>().sprite = soldSpr;
                            itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<ShopItem>().dialougeText = soldDialouge;
                            itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<ShopItem>().itemDescription = soldDescription;
                            moneyTextPlayerTwo.text = "Money: " + pc2.money.ToString();
                            itemInstanceList2[highLightedItemPlayerTwo].GetComponent<ShopItem>().sold = true;

                            dialougeText.text = "Score! Another one sold to the SUCKE.. Fine lad!";
                            itemDescriptionTextPlayerTwo.text = itemInstanceList2[highLightedItemPlayerTwo].gameObject.GetComponent<ShopItem>().itemDescription = soldDescription;

                            itemList2.Remove(itemInstanceList2[highLightedItemPlayerTwo]);
                        }
                        else if (pc2.money <= itemList2[highLightedItemPlayerTwo].GetComponent<ShopItem>().itemCost)
                        {
                            AudioSource.PlayClipAtPoint(purchaseDeniedSFX, new Vector3(7, 8, -10), 1.0f);
                            dialougeText.text = "You trying to cheat me you little bastards??? NOT ENOUGH CASH! Comprende?";
                        }
                    }
                }
            }
        }
    }

    void UpdateText(Text __dialougeText, Text __itemNameText, Text __itemCostText, Text __itemDescriptionText, Text __moneyText, List<GameObject> _itemInstanceList, int _highLightedItem)
    {
        __dialougeText.text = "Found something cool? Well, you better..";
        __itemNameText.text = _itemInstanceList[_highLightedItem].GetComponent<ShopItem>().itemNameText;
        __itemDescriptionText.text = _itemInstanceList[_highLightedItem].GetComponent<ShopItem>().itemDescription;
        __itemCostText.text = "Cost: " + _itemInstanceList[_highLightedItem].GetComponent<ShopItem>().itemCost.ToString() + " $";
        __moneyText.text = "Money: " + pc.money.ToString();

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
