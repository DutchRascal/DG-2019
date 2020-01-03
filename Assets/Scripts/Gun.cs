using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float shotCounter;

    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    public string weaponName;
    public Sprite gunUI;

    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(12);
                    shotCounter = timeBetweenShots;
                }
            }
        }

    }
}
