using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D theRB;

    void Start()
    {
        theRB = FindObjectOfType<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        theRB.velocity = moveInput * moveSpeed;
    }
}
