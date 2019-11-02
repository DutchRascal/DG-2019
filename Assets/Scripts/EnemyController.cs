using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    private Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private SpriteRenderer enemyBody;
    private Animator animator;
    public int health = 150;

    private void Awake()
    {
        instance = this;
    }

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
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
