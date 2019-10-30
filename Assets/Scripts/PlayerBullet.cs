using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    private Rigidbody2D theRB;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
