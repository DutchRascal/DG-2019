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
    public int
        currentHealth,
        currentSequence;
    public GameObject
        deathEffect,
        levelExit,
        hitEffect;
    public BossSequence[] sequences;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        UIController.instance.bossHealthBar.maxValue = currentHealth;
        UIController.instance.bossHealthBar.value = currentHealth;
        UIController.instance.UpdateUIElements();
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
            }
            if (actions[currentAction].moveToPoint && Vector3.Distance(transform.position, actions[currentAction].pointToMoveTo.position) > .5f)
            {
                moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
            }
            moveDirection.Normalize();
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
            UIController.instance.bossHealthBar.gameObject.SetActive(false);
        }
        else
        {
            if (currentHealth <= sequences[currentSequence].endSequenceHealth && currentSequence < sequences.Length - 1)
            {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
        UIController.instance.bossHealthBar.value = currentHealth;
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

[System.Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossAction[] actions;

    public int endSequenceHealth;
}

