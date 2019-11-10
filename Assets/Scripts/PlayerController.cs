using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private float shotCounter;
    private Vector2 moveInput;
    private Rigidbody2D theRB;
    private Transform gunArm;
    private Camera theCam;
    private Animator animator;
    private SpriteRenderer spriteRendererHand;
    private float activeMoveSpeed;

    public float
        moveSpeed,
        timeBetweenShots,
        dashCounter,
        dashCoolCounter,
        dashSpeed = 8f,
        dashLength = .5f,
        dashCooldown = 1f,
        dashInvisibility = .5f;
    public GameObject bulletToFire;
    public Transform firePoint;
    public SpriteRenderer bodySR;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        gunArm = GameObject.Find("Gun Hand").GetComponent<Transform>();
        theCam = Camera.main;
        animator = GetComponent<Animator>();
        spriteRendererHand = GameObject.Find("Gun Hand").GetComponent<SpriteRenderer>();
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        MovePlayer();
        FireBullet();
        AnimatePlayer();
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
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
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
            spriteRendererHand.sortingOrder = 0;
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
            spriteRendererHand.sortingOrder = 2;
        }
        //rotate arm
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angel = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angel);
    }

    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void AnimatePlayer()
    {
        if (moveInput == Vector2.zero)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }
}
