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

