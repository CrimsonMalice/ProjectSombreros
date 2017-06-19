using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour {

    [SerializeField] private int pointsValue;
    [SerializeField] private AudioClip pickUpClip;
    [SerializeField] private GameObject moneyFloatText;

    public int PointsValue
    {
        get { return this.pointsValue; }
        set { this.pointsValue = value; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            LevelManager.moneyPickedUp += PointsValue;
            GameStatsTracker.totalMoneyCollected += PointsValue;
            GameStatsTracker.finalScore += PointsValue;
            print(LevelManager.moneyPickedUp);
            if (SoundManager.toggleSFX)
            {
                AudioSource.PlayClipAtPoint(pickUpClip, new Vector3(7, 8, -10), 1.0f);
            }
            PlayerController.points += pointsValue;
            other.gameObject.GetComponent<PlayerController>().money += pointsValue;
            print(PlayerController.points);

            GameObject instance = Instantiate(moneyFloatText, transform.position, Quaternion.identity);

            
            instance.GetComponent<MoneyFloatText>().moneyAmount = "+$" + pointsValue;

            Destroy(gameObject);
        }
    }
}
