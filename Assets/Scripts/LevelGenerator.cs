﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    public GameObject layoutyRoom;
    public int
            distanceToEnd,
            minDistanceToShop,
            maxDistanceToShop;
    public Color
            startColor,
            endColor,
            shopColor;
    public Transform generatorPoint;
    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    public Direction selectedDirection;
    public float
        xOffSet = 18f,
        yOffSet = 10f;
    public LayerMask whatIsRoom;
    public RoomPrefabs rooms;
    public RoomCenter
        centerStart,
        centerEnd,
        centerShop;
    public RoomCenter[] potentialCenters;
    public bool includeShop;

    private GameObject
            endRoom,
            shopRoom;
    private List<GameObject>
                layoutRoomObjects = new List<GameObject>(),
                 generatedOutlines = new List<GameObject>();

    void Start()
    {
        CreateRoomLayout();
        if (includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop + 1);
            shopRoom = layoutRoomObjects[shopSelector];
            layoutRoomObjects.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }
        CreateRoomOutlines();
        /*       foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
               {
                   print(enemy);
               }
               int numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
               print(numberOfEnemies);*/
    }

    private void CreateRoomOutlines()
    {
        CreateRoomOutline(Vector3Int.zero);
        foreach (GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);
        if (includeShop)
        {
            CreateRoomOutline(shopRoom.transform.position);
        }
        foreach (GameObject outline in generatedOutlines)
        {
            bool generateCenter = true;
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }
            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }
            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centerShop, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }
            if (generateCenter)
            {
                int centerSelect = Random.Range(0, potentialCenters.Length);
                Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
        }
    }

    private void CreateRoomLayout()
    {
        Instantiate(layoutyRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutyRoom, generatorPoint.position, generatorPoint.rotation);
            layoutRoomObjects.Add(newRoom);
            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();
            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffSet, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffSet, 0f, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffSet, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffSet, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f), 0.2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), 0.2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffSet, 0f, 0f), 0.2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffSet, 0f, 0f), 0.2f, whatIsRoom);
        int directionCount = UpdateDirectionCount(roomAbove) + UpdateDirectionCount(roomBelow) + UpdateDirectionCount(roomLeft) + UpdateDirectionCount(roomRight);
        switch (directionCount)
        {
            case 0:
                Debug.LogError(("Found no room exists!!"));
                break;
            case 1:
                if (roomAbove)
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                if (roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                if (roomRight)
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                if (roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                break;
            case 2:
                if (roomAbove && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                if (roomAbove && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                if (roomAbove && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                if (roomRight && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                if (roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                if (roomBelow && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                break;
            case 3:
                if (roomAbove && roomRight && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRighDown, roomPosition, transform.rotation));
                if (roomAbove && roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                if (roomRight && roomBelow && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                if (roomBelow && roomLeft && roomAbove)
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                break;
            case 4:
                generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
                break;
            default:
                Debug.LogError("More then 4 exits!!");
                break;

        }
    }

    public int UpdateDirectionCount(bool room)
    {
        if (room)
            return 1;
        else
            return 0;
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject
            singleUp, singleDown, singleRight, singleLeft,
            doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
            tripleUpRighDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
            fourway;
}
