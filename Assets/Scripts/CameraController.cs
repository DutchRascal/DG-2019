﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public Camera
            mainCamera,
            bigMapCamera;
    public bool isBossRoom;

    private bool bigMapActive;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (isBossRoom)
            target = PlayerController.instance.transform;
    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
            if (bigMapActive)
                DeactivateBigMap();
            else
                ActivateBigMap();
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;
            mainCamera.enabled = false;
            bigMapCamera.enabled = true;
            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;
            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;
            mainCamera.enabled = true;
            bigMapCamera.enabled = false;
            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);
        }
    }
}
