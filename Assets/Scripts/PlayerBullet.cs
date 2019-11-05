using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    private Rigidbody2D theRB;
    public GameObject impactEffect;
    private int damageToGive = 25;

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
        Instantiate(impactEffect, transform.position, transform.rotation);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
