using System.Collections;
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
    public GameObject mapCameraRenderer;

    private bool bigMapActive;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.M))
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
            mapCameraRenderer.SetActive(false);
            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;
        }
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;
            mainCamera.enabled = true;
            bigMapCamera.enabled = false;
            mapCameraRenderer.SetActive(true);
            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
        }
    }
}
