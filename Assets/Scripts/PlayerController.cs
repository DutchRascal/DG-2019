using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Vector2 moveInput;
    private Rigidbody2D theRB;
    private Animator animator;
    private float
        activeMoveSpeed,
        dashCoolCounter,
        dashCounter;

    public float
        moveSpeed,
        dashSpeed = 8f,
        dashLength = .5f,
        dashCooldown = 1f,
        dashInvincibility = .5f;
    public SpriteRenderer bodySR;
    public bool isDashing;
    // [HideInInspector]
    public bool
        canMove = true,
        allowedToShoot = true;
    public List<Gun> availableGuns = new List<Gun>();
    [HideInInspector]
    public int currentGun;
    public Transform gunArm;


    // public PlayerController[] gameObjArray;

    private void Awake()
    {

        // if (instance == null)
        // {
        instance = this;
        DontDestroyOnLoad(gameObject);
        // return;
        // }
        // else if (instance != this)
        // {
        //     print(instance);
        // Destroy(this.gameObject);
        //     print("destroy");
        // }
    }

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        // gunArm = GameObject.Find("Gun Hand").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
        foreach (Gun gun in availableGuns)
        {
            gun.gameObject.SetActive(false);
        }
        availableGuns[0].gameObject.SetActive(true);
        UpdateGunUI();
    }

    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            MovePlayer();
            AnimatePlayer();
            CheckDashing();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentGun++;
                SwitchGun();
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    private void CheckDashing()
    {
        isDashing = dashCounter > 0 ? true : false;
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0 && (theRB.velocity.x != 0 || theRB.velocity.y != 0))
            // if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                animator.SetTrigger("dash");
                AudioManager.instance.PlaySFX(8);
                PlayerHealthController.instance.MakeInvisible(dashInvincibility);
            }
        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
        theRB.velocity = moveInput * activeMoveSpeed;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);
        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
            foreach (Gun gun in availableGuns)
            {
                gun.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
            foreach (Gun gun in availableGuns)
            {
                gun.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
        //rotate arm
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angel = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angel);
    }

    private void AnimatePlayer()
    {
        if (moveInput == Vector2.zero)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }

    public void SwitchGun()
    {
        if (availableGuns.Count > 0)
        {
            foreach (Gun gun in availableGuns)
            {
                gun.gameObject.SetActive(false);
            }
            // availableGuns[currentGun].gameObject.SetActive(false);
            // currentGun++;
            if (currentGun >= availableGuns.Count)
                currentGun = 0;
            availableGuns[currentGun].gameObject.SetActive(true);
            UpdateGunUI();
        }
        else
        {
            Debug.LogError("Player has no guns!");
        }

    }

    private void UpdateGunUI()
    {
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
    }
}
