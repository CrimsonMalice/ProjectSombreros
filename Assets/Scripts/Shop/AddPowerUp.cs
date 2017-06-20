using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPowerUp : MonoBehaviour {

    public static void AddPowerUpScript(string powerUpName, GameObject shopItem, GameObject playerObject)
    {
        switch (powerUpName)
        {
            case "SpeedBoots":
                //GameObject.FindGameObjectWithTag("PlayerOne").AddComponent<SpeedBootsScript>();
                playerObject.AddComponent<SpeedBootsScript>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "ExtraHeart":
                //GameObject.FindGameObjectWithTag("PlayerOne").AddComponent<ExtraHeart>();
                playerObject.AddComponent<ExtraHeart>();
                break;

            case "BagOfGreaterGreed":
                //GameObject.FindGameObjectWithTag("PlayerOne").AddComponent<BagOfGreaterGreed>();
                playerObject.AddComponent<BagOfGreaterGreed>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "MetalPlate":
                //GameObject.FindGameObjectWithTag("PlayerOne").AddComponent<MetalPlate>();
                playerObject.AddComponent<MetalPlate>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "MudBoots":
                //GameObject.FindGameObjectWithTag("PlayerOne").AddComponent<MudBoots>();
                playerObject.AddComponent<MudBoots>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            case "RollingBomb":
                //GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>().selectedBomb = Resources.Load("Bombs/RollingBomb") as GameObject;
                playerObject.AddComponent<RollingBomb>();
                ListOfItems.listOfAllShopItems.Remove(shopItem);
                break;

            default:
                Debug.LogError("The Power Up Was not found and therefor not added to the Player! Make sure that the script is added in the AddPowerUp script's Switch Statement!");
                break;
        }
    }
}
