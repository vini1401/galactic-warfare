using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public EnemyData data;
    public GameObject explosionParticlePrefab;

    private int currentLife;

    void Start()
    {
        currentLife = data.life;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            if (player != null)
                player.AddScore(data.scoreValue);

            SpawnExplosion();
            Destroy(gameObject);
        }
    }

    void SpawnExplosion()
    {
        if (explosionParticlePrefab != null)
        {
            GameObject explosion = Instantiate(
                explosionParticlePrefab,
                transform.position,
                Quaternion.identity
            );

            Destroy(explosion, 1f);
        }
    }
}
