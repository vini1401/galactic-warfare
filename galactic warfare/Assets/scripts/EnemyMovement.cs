using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
