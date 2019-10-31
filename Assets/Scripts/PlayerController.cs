using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D theRB;
    private Transform gunArm;
    private Camera theCam;
    private Animator animator;
    private SpriteRenderer spriteRendererHand;
    public GameObject bulletToFire;
    public Transform firePoint;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        gunArm = GameObject.Find("Gun Hand").GetComponent<Transform>();
        theCam = Camera.main;
        animator = GetComponent<Animator>();
        spriteRendererHand = GameObject.Find("Gun Hand").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        theRB.velocity = moveInput * moveSpeed;
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
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
        }
        if (moveInput == Vector2.zero)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }
}
