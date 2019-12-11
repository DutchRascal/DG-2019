﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        Instantiate(layoutyRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
    }

    void Update()
    {

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
