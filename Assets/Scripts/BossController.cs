using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public static BossController instance;

    private int currentAction;
    private float
        actionCounter,
        shotCounter;
    private Vector2 moveDirection;

    public BossAction[] actions;
    public Rigidbody2D theRB;
    public int currentHealth;
    public GameObject
        deathEffect,
        levelExit,
        hitEffect;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            HandleMovement();
            HandleShooting();
        }
        else
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;
        }
    }

    private void HandleMovement()
    {
        moveDirection = Vector2.zero;
        if (actions[currentAction].shouldMove)
        {
            if (actions[currentAction].shouldChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
            }
            if (actions[currentAction].moveToPoint)
            {
                moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
            }
        }
        theRB.velocity = moveDirection * actions[currentAction].moveSpeed;
    }

    private void HandleShooting()
    {
        if (actions[currentAction].shouldShoot)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = actions[currentAction].timeBetweenShots;
                foreach (Transform shotPoint in actions[currentAction].shotPoints)
                {
                    Instantiate(actions[currentAction].itemToShoot, shotPoint.position, shotPoint.rotation);
                }
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);
            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }
            levelExit.SetActive(true);
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public float moveSpeed;
    public float timeBetweenShots;
    public bool
        shouldMove,
        shouldChasePlayer,
        moveToPoint,
        shouldShoot;
    public Transform pointToMoveTo;
    public Transform[] shotPoints;
    public GameObject itemToShoot;
}

