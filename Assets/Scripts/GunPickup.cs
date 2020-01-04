using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private float waitToBeCollected = 0.5f;

    public Gun theGun;

    private void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            bool hasGun = false;
            foreach (Gun gun in PlayerController.instance.availableGuns)
            {
                if (gun.weaponName == theGun.weaponName)
                    hasGun = true;
            }
            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.gunArm;
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3Int.zero);
                gunClone.transform.localScale = Vector3Int.one;
                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                PlayerController.instance.SwitchGun();
            }
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);
        }
    }
}
