using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D theRB;
    private Transform gunArm;

    void Start()
    {
        theRB = FindObjectOfType<Rigidbody2D>();
        gunArm = GameObject.Find("Gun Hand").GetComponent<Transform>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        theRB.velocity = moveInput * moveSpeed;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        //rotate arm
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angel = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angel);
    }
}
