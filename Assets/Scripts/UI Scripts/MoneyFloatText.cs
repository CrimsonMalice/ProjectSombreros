using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyFloatText : MonoBehaviour {

    GameObject canvas;

    public string moneyAmount = "+$1000";
    float positionCounter = 0;
    float speed = 1f;
    float flashingTimerStart = 0.1f;
    float flashingTimer;
    bool visible = false;

	// Use this for initialization
	void Start ()
    {
        if (LevelManager.playerAmount == 1)
        {
            canvas = GameObject.Find("Canvas");
        }

        else if (LevelManager.playerAmount == 2)
        {
            canvas = GameObject.Find("MultiplayerCanvas");
        }

        gameObject.transform.SetParent(canvas.transform);
        flashingTimer = flashingTimerStart;
        gameObject.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Text>().text = moneyAmount;
        Destroy(gameObject, 2.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (positionCounter <= 16f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            positionCounter++;
        }

        if (flashingTimer > 0)
        {
            flashingTimer -= Time.deltaTime;
        }
        else if (flashingTimer <= 0)
        {
            visible = !visible;

            if (visible)
            {
                gameObject.GetComponent<Text>().enabled = false;
                gameObject.GetComponent<Shadow>().enabled = false;
                gameObject.GetComponent<Outline>().enabled = false;
            }
            else if (!visible)
            {
                gameObject.GetComponent<Text>().enabled = true;
                gameObject.GetComponent<Shadow>().enabled = true;
                gameObject.GetComponent<Outline>().enabled = true;
            }

            flashingTimer = flashingTimerStart;
        }
	}
}
