using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D theRB;
    private Vector3 moveDirection;
    private SpriteRenderer enemyBody;
    private Animator animator;
    private float fireCounter;
    private bool isEnemyOnScreen;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;

    public float shootRange;
    public float moveSpeed;
    public float rangeToChasePlayer;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyBody = GameObject.Find("Body").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateVariables();
        CheckIfEnemyIsOnScreen();
        if (isEnemyOnScreen)
        {
            EnemyMoving();
            Shoot();
            AnimatePlayer();
        }
    }

    private void UpdateVariables()
    {
        playerPosition = PlayerController.instance.transform.position;
        enemyPosition = transform.position;
    }

    private void CheckIfEnemyIsOnScreen()
    {
        isEnemyOnScreen = enemyBody.isVisible ? true : false;
    }

    private void Shoot()
    {
        if (shouldShoot && Vector3.Distance(enemyPosition, playerPosition) < shootRange)
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }

    private void EnemyMoving()
    {
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
