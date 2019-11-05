using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private SpriteRenderer enemyBody;
    private Animator animator;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyBody = GameObject.Find("Body").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMoving();
        AnimatePlayer();
        Shoot();
    }

    private void Shoot()
    {
        if (shouldShoot)
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    private void PlayerMoving()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        Vector3 enemyPosition = transform.position;
        if (Vector3.Distance(enemyPosition, playerPosition) < rangeToChasePlayer)
        {
            moveDirection = playerPosition - enemyPosition;
            enemyBody.flipX = playerPosition.x > enemyPosition.x ? true : false;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        moveDirection.Normalize();
        theRB.velocity = moveDirection * moveSpeed;
    }

    private void AnimatePlayer()
    {
        if (moveDirection != Vector3.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(hitEffect, transform.position, transform.rotation);
        if (health < 0)
        {
            Destroy(gameObject);
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));
        }
    }
}
