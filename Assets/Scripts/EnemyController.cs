using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D theRB;
    public SpriteRenderer enemyBody;
    private Animator animator;
    private float
            fireCounter,
            wanderCounter,
            pauseCounter;
    private bool isEnemyOnScreen;
    private Vector3
            playerPosition,
            enemyPosition,
            moveDirection,
            wanderDirection;
    private int currentPatrolPoint;
    private GameObject dropItem;

    public float
            shootRange,
            moveSpeed,
            rangeToChasePlayer,
            fireRate,
            runawayRange,
            wanderLength,
            pauseLength;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject
            hitEffect,
            bullet;
    public bool
            shouldShoot,
            shouldRunAway,
            shouldChasePlayer,
            shouldWander,
            shouldPatrol;
    public Transform firePoint;
    public Transform[] patrolPoints;

    public bool shouldDropItem;
    public GameObject[]
            itemsToDrop;
    public float itemDropPercentage;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (shouldWander)
            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
    }

    void Update()
    {
        UpdateVariables();
        CheckIfEnemyIsOnScreen();
        if (isEnemyOnScreen)
        {
            EnemyMoving();
            if (PlayerController.instance.gameObject.activeInHierarchy)
            {
                Shoot();
            }
            else
            {
                moveDirection = Vector3.zero;
                theRB.velocity = moveDirection;
            }
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
                AudioManager.instance.PlaySFX(17);
            }
        }
    }

    private void EnemyMoving()
    {
        moveDirection = Vector3.zero;
        if (Vector3.Distance(enemyPosition, playerPosition) < rangeToChasePlayer && shouldChasePlayer)
        {
            moveDirection = playerPosition - enemyPosition;
            enemyBody.flipX = playerPosition.x > enemyPosition.x ? true : false;
        }
        else
        {
            if (shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;
                    //move the enemy
                    moveDirection = wanderDirection;
                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                    }
                }
                if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;
                    if (pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    }
                }
            }
            if (shouldPatrol)
            {
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                        currentPatrolPoint = 0;
                }
            }
        }
        if (shouldRunAway && Vector3.Distance(enemyPosition, playerPosition) < runawayRange)
        {
            moveDirection = enemyPosition - playerPosition;
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
        AudioManager.instance.PlaySFX(2);
        Instantiate(hitEffect, transform.position, transform.rotation);
        if (health < 0)
        {
            AudioManager.instance.PlaySFX(1);
            Destroy(gameObject);
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));
            //drop items
            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);
                if (dropChance < itemDropPercentage)
                {
                    dropItem = itemsToDrop[Random.Range(0, itemsToDrop.Length)];
                    Instantiate(dropItem, transform.position, transform.rotation);
                }
            }
        }
    }
}
