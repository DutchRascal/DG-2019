using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{

    private Vector3 moveDirection;
    private float
        moveSpeed = 3f,
        deceleration = 5f,
        lifeTime = 5f,
        fadeSpeed = 2.5f;

    public SpriteRenderer theSR;

    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (theSR.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
