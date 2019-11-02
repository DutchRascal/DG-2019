using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private SpriteRenderer enemyBody;
    private Animator animator;


    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyBody = GameObject.Find("Body").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        Vector3 enemyPosition = transform.position;
        if (Vector3.Distance(enemyPosition, playerPosition) < rangeToChasePlayer)
        {
            moveDirection = playerPosition - enemyPosition;
            enemyBody.flipX = playerPosition.x > enemyPosition.x ? true : false;
            animator.SetBool("isWalking", true);
        }
        else
        {
            moveDirection = Vector3.zero;
            animator.SetBool("isWalking", false);
        }
        moveDirection.Normalize();
        theRB.velocity = moveDirection * moveSpeed;
    }
}
