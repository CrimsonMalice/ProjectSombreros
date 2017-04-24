using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    /* MAP TILE-INDEX
     * 0 = EMPTY
     * 1 = ROAD
     * 2 = ROCK
     * 3 = BUIDLING
     */

    [SerializeField] private GameObject grid;
    [SerializeField] private Vector2 currentPos; //Starts at x32, y32 to take the road-offset into account.
    [SerializeField] private int currentArrayPosX = 2;
    [SerializeField] private int currentArrayPosY = 2;

    const int MINWIDTH = 4;
    const int MINHEIGHT = 5;
    const int MAXWIDTH = 8; //19 /8
    const int MAXHEIGHT = 5; //7 /5
    const int MAPHEIGHT = 33;
    const int MAPWIDTH = 59; //60
    Vector2 topLeftMap = new Vector2(-432, 238);
    private Vector2 mapVectorSize;

    const int roadOffset = 32; //32 pixels = 2 16x16 tiles.
    private int distanceToBottom;
    private int distanceToRightEdge;

    public static int[,] levelMap; //Certain numbers represent certain objects. 0 = empty, 1 = road, 2 = rock, 3 = building.
    private GameObject[] priorityBuildings;
    [SerializeField]
    private GameObject[] buildings;

    private List<GameObject> buildingReferences;

    [SerializeField]
    private GameObject debugDot;

    [SerializeField] private GameObject rock;


    // Use this for initialization
    void Start()
    {
        mapVectorSize = new Vector2(MAPWIDTH, MAPHEIGHT);
        currentPos = new Vector2(grid.transform.position.x, grid.transform.position.y);

        currentArrayPosX = 2;
        currentArrayPosY = 2;

        MakeOffsetRoads();

        GenerateStructures();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeOffsetRoads()
    {
        levelMap = new int[,] {
 {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
 {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
 {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
 {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
 };
    }

    void GenerateStructures()
    {
        distanceToRightEdge = MAPWIDTH - currentArrayPosX; //Update our current distance to the right edge.
        distanceToBottom = MAPHEIGHT - currentArrayPosY; //Update our current distance to the bottom.

        while (currentArrayPosY < MAPHEIGHT - 1) //Loop through the map as long as we haven't gone outside the map-borders.
        {
            if (levelMap[currentArrayPosY, currentArrayPosX] == 2)
            {
                //Instantiate(rock, currentPos, Quaternion.identity);

                currentArrayPosX++; //Since there's nothing to store or do on this spot, we move on to the next tile.
                currentPos = new Vector2(currentPos.x + 16, currentPos.y); //Update the current world-coordinates

                if (currentArrayPosX >= MAPWIDTH) //If we exceed the Maps width:
                {
                    currentArrayPosY++; //Move down a row.
                    currentArrayPosX = 2; // Is there to not overwrite the road

                    currentPos = new Vector2(grid.transform.position.x, currentPos.y - 16);
                }

                distanceToRightEdge = MAPWIDTH - currentArrayPosX; //Update our current distance to the right edge.
                distanceToBottom = MAPHEIGHT - currentArrayPosY; //Update our current distance to the bottom.
            }
            else if (levelMap[currentArrayPosY, currentArrayPosX] != 0) //If the current position in the array isn't occupied:
            {
                currentArrayPosX++; //Since there's nothing to store or do on this spot, we move on to the next tile.
                currentPos = new Vector2(currentPos.x + 16, currentPos.y); //Update the current world-coordinates

                if (currentArrayPosX >= MAPWIDTH) //If we exceed the Maps width:
                {
                    currentArrayPosY++; //Move down a row.
                    currentArrayPosX = 2;

                    currentPos = new Vector2(grid.transform.position.x, currentPos.y - 16);
                }

                distanceToRightEdge = MAPWIDTH - currentArrayPosX; //Update our current distance to the right edge.
                distanceToBottom = MAPHEIGHT - currentArrayPosY; //Update our current distance to the bottom.
            }
            else if (levelMap[currentArrayPosY, currentArrayPosX] == 0) //If the tile is empty
            {
                List<GameObject> validHouses = new List<GameObject>(); //Stores our valid house-options.
                GameObject newBuilding = null; //The current building that we're about to generate.
                int structureHeight = 0;
                int structureWidth = 0;
                bool validSize = false;
                bool placeRocks = false;

                if (!placeRocks)
                {
                    for (int i = 0; i < buildings.Length; i++)
                    {

                        while (!validSize && !placeRocks)
                        {
                            structureHeight = Random.Range(MINHEIGHT, MAXHEIGHT); //Generate a random Height with set min & max parameters.
                            structureWidth = Random.Range(MINWIDTH, MAXWIDTH); //Generate a random Width with set min & max parameters.

                            CheckValidPlacement(structureHeight, structureWidth, validSize);

                            if (structureHeight < distanceToBottom - 1 && structureWidth < distanceToRightEdge - 1) //If the Height and Width doesn't go outside the map or cross the preset roads.
                            {
                                validSize = true;
                            }
                            if (structureHeight > distanceToBottom - 1 || structureWidth > distanceToRightEdge - 1)
                            {
                                placeRocks = true;
                            }
                        }

                        //print("Height: " + structureHeight);
                        //print("Width: " + structureWidth);

                        for (int j = 0; j < buildings.Length; j++)
                        {
                            if (buildings[j].GetComponent<HouseObject>().width == structureWidth && buildings[j].GetComponent<HouseObject>().height == structureHeight)
                            {
                                validHouses.Add(buildings[j]); //Add all the buildings with the valid sizes.
                            }
                        }

                        //for (int k = 0; k < validHouses.Count; k++)
                        //{
                        //    Debug.Log(validHouses[k]);
                        //}
                    }

                    newBuilding = Instantiate(buildings[Random.Range(0, buildings.Length)], new Vector3(currentPos.x, currentPos.y, 0), Quaternion.identity); //Then generate one at random
                    newBuilding.GetComponent<HouseObject>().arrayY = currentArrayPosY; //Declare the Y position in the grid
                    newBuilding.GetComponent<HouseObject>().arrayX = currentArrayPosX; //Declare the X position in the grid

                    structureHeight = newBuilding.GetComponent<HouseObject>().height;
                    structureWidth = newBuilding.GetComponent<HouseObject>().width;

                }

                if (!placeRocks && validSize)
                {
                    int widthCounter = 0;

                    for (int j = 0; j < structureHeight; j++)
                    {
                        for (int k = 0; k < structureWidth; k++)
                        {
                            //Don't exceed MapWidth
                            levelMap[currentArrayPosY + j, currentArrayPosX + k] = 3;
                            widthCounter++;

                            //currentArrayPosX++;
                            //currentPos = new Vector2(currentPos.x + 16, currentPos.y);

                            //distanceToRightEdge = MAPWIDTH - currentArrayPosX; //Update our current distance to the right edge.
                            //distanceToBottom = MAPHEIGHT - currentArrayPosY; //Update our current distance to the bottom.
                        }

                        for (int k = 0; k < structureHeight; k++)
                        {
                            for (int l = 0; l < 1; l++)
                            {
                                levelMap[currentArrayPosY + k, currentArrayPosX + structureWidth - 1 + 1] = 1;
                                levelMap[currentArrayPosY + k, currentArrayPosX + structureWidth - 1 + 2] = 1;
                            }

                            for (int l = 0; l < structureWidth + 2; l++)
                            {
                                levelMap[currentArrayPosY + structureHeight - 1 + 1, currentArrayPosX + l] = 1;
                                levelMap[currentArrayPosY + structureHeight - 1 + 2, currentArrayPosX + l] = 1;

                                //levelMap[currentArrayPosY - 1, currentArrayPosX + l] = 1;
                                //levelMap[currentArrayPosY - 2, currentArrayPosX + l] = 1;
                            }
                        }
                        //levelMap[currentArrayPosY, widthCounter + 1] = 1;
                        //levelMap[currentArrayPosY, widthCounter + 2] = 1;
                        widthCounter = 0;

                        //Don't exceed MapHeight
                        //if(levelMaplevelMap[currentArrayPosY + j, currentArrayPosX] > MAPHEIGHT)
                        //levelMap[currentArrayPosY + j, currentArrayPosX] = 3;
                    }
                }

                if (placeRocks && !validSize)
                {
                    if(levelMap[currentArrayPosY, currentArrayPosX] == 0)
                    {
                        levelMap[currentArrayPosY, currentArrayPosX] = 2; //Mark all the rock-tiles
                        Instantiate(rock, currentPos, Quaternion.identity);
                    }
                }

                currentArrayPosX++; //Since there's nothing to store or do on this spot, we move on to the next tile.
                currentPos = new Vector2(currentPos.x + 16, currentPos.y); //Update the current world-coordinates

                if (currentArrayPosX >= MAPWIDTH) //If we exceed the Maps width:
                {
                    currentArrayPosY++; //Move down a row.
                    currentArrayPosX = 2; // Is there to not overwrite the road

                    currentPos = new Vector2(grid.transform.position.x, grid.transform.position.y /*+ roadOffset*/);
                }

                distanceToRightEdge = MAPWIDTH - currentArrayPosX; //Update our current distance to the right edge.
                distanceToBottom = MAPHEIGHT - currentArrayPosY; //Update our current distance to the bottom.
            }

            //Todo:
            //- Generate a structure from prefab depending on the set width and height.
            //- Instansiate that Prefab
            //- Cover up all the positions in the array depending on the height and width
            //- Generate roads above, below and to the sides of the structure
            //- Incement the currentArrayPos according the the width.
        }
    }

    void CheckValidPlacement(int structureHeight, int structureWidth, bool validSize)
    {
        //Loop through all spots where the building will be placed, so that it doesn't intersect anything.
        for (int j = 0; j <= structureHeight; j++)
        {
            for (int k = 0; k <= structureWidth; k++)
            {
                if (levelMap[currentArrayPosY + j, currentArrayPosX + k] != 0)
                {
                    return;
                }

                if (j == structureHeight && k == structureWidth)
                {
                    validSize = true;
                    return;
                }
            }
        }
    }
}
