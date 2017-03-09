using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPowerUp : MonoBehaviour {

    public static void AddPowerUpScript(string powerUpName)
    {
        switch (powerUpName)
        {
            case "SpeedBoots":
                GameObject.FindGameObjectWithTag("Player").AddComponent<SpeedBootsScript>();
                break;

            case "ExtraHeart":
                GameObject.FindGameObjectWithTag("Player").AddComponent<ExtraHeart>();
                break;

            case "BagOfGreaterGreed":
                GameObject.FindGameObjectWithTag("Player").AddComponent<BagOfGreaterGreed>();
                break;

            case "MetalPlate":
                GameObject.FindGameObjectWithTag("Player").AddComponent<MetalPlate>();
                break;

            case "MudBoots":
                GameObject.FindGameObjectWithTag("Player").AddComponent<MudBoots>();
                break;

            case "RollingBomb":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().selectedBomb = Resources.Load("Bombs/RollingBomb") as GameObject;
                break;

            default:
                Debug.LogError("The Power Up Was not found and therefor not added to the Player! Make sure that the script is added in the AddPowerUp script's Switch Statement!");
                break;
        }
    }
}
