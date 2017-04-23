using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{

    [SerializeField]
    private float mTimer;

    void Update()
    {
        mTimer += Time.deltaTime;

        if (mTimer >= 0.5)
        {
            GetComponent<Text>().enabled = true;
        }

        if(mTimer >= 1)
        {
            GetComponent<Text>().enabled = false;
            mTimer = 0;
        }
    }

	
}
