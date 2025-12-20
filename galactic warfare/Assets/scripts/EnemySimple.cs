using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public EnemyData data;

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

            Destroy(gameObject);
        }
    }
}
