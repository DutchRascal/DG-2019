﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    public GameObject layoutyRoom;
    public int distanceToEnd;
    public Color
            startColor,
            endColor;
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
    public RoomPrefabs roomPrefabs;

    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    void Start()
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
                print("Duplicate");
                MoveGenerationPoint();
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
