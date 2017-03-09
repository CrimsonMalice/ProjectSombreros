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

            default:
                Debug.LogError("The Power Up Was not found and therefor not added to the Player! Make sure that the script is added in the AddPowerUp script's Switch Statement!");
                break;
        }
    }
}
