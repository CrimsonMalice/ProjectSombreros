using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiArrayTool : MonoBehaviour {

    private int[,] multiDArray;
    private bool isInterfaceDrawn = false;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private Text arrayText;
    private string arrayString;

    private int arrayXLength;
    private int arrayYLength;

    private void Awake()
    {
        multiDArray = new int[arrayXLength, arrayYLength];
    }

    // Use this for initialization
    void Start ()
    {
        //multiDArray = new int[arrayXLength, arrayYLength];
        multiDArray = LevelGenerator.levelMap; //Insert the array you want to visualize in the UI here.
        panelObject.SetActive(false);
        arrayText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !isInterfaceDrawn)
        {
            arrayText.enabled = true;

            arrayXLength = LevelGenerator.levelMap.GetLength(1);
            arrayYLength = LevelGenerator.levelMap.GetLength(0);

            print(LevelGenerator.levelMap.GetLength(1));
            print(LevelGenerator.levelMap.GetLength(0));

            for (int i = 0; i < arrayYLength; i++)
            {
                for (int j = 0; j < arrayXLength; j++)
                {
                    arrayString += LevelGenerator.levelMap[i, j] + ", ";
                }

                arrayString += "\n";
            }

            arrayText.text = arrayString;
            panelObject.SetActive(true);
            isInterfaceDrawn = true;

            //print(multiDArray[0, 0]);
        }
        else if (Input.GetKeyDown(KeyCode.F1) && isInterfaceDrawn)
        {
            panelObject.SetActive(false);
            arrayText.text = "";
            arrayString = "";
            arrayText.enabled = false;
            isInterfaceDrawn = false;
        }
	}
}
