using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public int life = 1;

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
