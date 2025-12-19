using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public int life = 1;
    public int scoreValue = 100;

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            if (player != null)
            {
                player.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }
    }
}
