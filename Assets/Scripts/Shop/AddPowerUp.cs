using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPowerUp : MonoBehaviour {

    public static void AddPowerUpScript(string powerUpName, GameObject shopItem)
    {
        switch (powerUpName)
        {
            case "SpeedBoots":
                GameObject.FindGameObjectWithTag("Player").AddComponent<SpeedBootsScript>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "ExtraHeart":
                GameObject.FindGameObjectWithTag("Player").AddComponent<ExtraHeart>();
                break;

            case "BagOfGreaterGreed":
                GameObject.FindGameObjectWithTag("Player").AddComponent<BagOfGreaterGreed>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "MetalPlate":
                GameObject.FindGameObjectWithTag("Player").AddComponent<MetalPlate>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "MudBoots":
                GameObject.FindGameObjectWithTag("Player").AddComponent<MudBoots>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "RollingBomb":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().selectedBomb = Resources.Load("Bombs/RollingBomb") as GameObject;
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            default:
                Debug.LogError("The Power Up Was not found and therefor not added to the Player! Make sure that the script is added in the AddPowerUp script's Switch Statement!");
                break;
        }
    }
}
