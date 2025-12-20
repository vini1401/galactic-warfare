using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    public GameObject impactParticle; // PREFAB

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemySimple enemy = other.GetComponent<EnemySimple>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            SpawnImpact();
            Destroy(gameObject);
        }
    }

    void SpawnImpact()
    {
        if (impactParticle != null)
        {
            GameObject impact = Instantiate(
                impactParticle,
                transform.position,
                Quaternion.identity
            );

            Destroy(impact, 0.5f);
        }
    }
}
