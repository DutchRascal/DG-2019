using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{

    private bool inBuyZone;
    private Gun theGun;

    public GameObject buyMessage;
    public bool
            isHealthRestore,
            isHealthUpgrade,
            isWeapon;
    public int
            itemCost,
            healthUpgradeAmount;
    public Gun[] potentialGuns;
    public SpriteRenderer gunSprite;
    public Text infoText;

    private void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];
            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n- " + theGun.itemCost + " Gold -";
            itemCost = theGun.itemCost;
        }
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                {
                    if (LevelManager.instance.currentCoins >= itemCost)
                    {
                        print("CC before" + LevelManager.instance.currentCoins);
                        LevelManager.instance.SpendCoins(itemCost);
                        print("CC after" + LevelManager.instance.currentCoins);
                        if (isHealthRestore &&
                            PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth &&
                            LevelManager.instance.currentCoins > 0)
                        {
                            PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                            HandleSuccesfulBuy();
                        }
                        else if (isHealthUpgrade && LevelManager.instance.currentCoins > 0)
                        {
                            PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                            HandleSuccesfulBuy();
                        }
                        else if (isWeapon && LevelManager.instance.currentCoins > 0)
                        {
                            Gun gunClone = Instantiate(theGun);
                            gunClone.transform.parent = PlayerController.instance.gunArm;
                            gunClone.transform.position = PlayerController.instance.gunArm.position;
                            gunClone.transform.localRotation = Quaternion.Euler(Vector3Int.zero);
                            gunClone.transform.localScale = Vector3Int.one;
                            PlayerController.instance.availableGuns.Add(gunClone);
                            PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                            PlayerController.instance.SwitchGun();
                            HandleSuccesfulBuy();
                            UIController.instance.UpdateUIElements();
                            {

                            }
                        }
                        else
                        {
                            LevelManager.instance.GetCoins(itemCost);
                            AudioManager.instance.PlaySFX(19);
                        }
                    }
                    else
                        AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void HandleSuccesfulBuy()
    {
        print(itemCost);

        AudioManager.instance.PlaySFX(18);
        gameObject.SetActive(false);
        inBuyZone = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
