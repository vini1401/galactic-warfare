using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 0.5f;

    private bool canDamage = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDamage) return;

        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponentInParent<PlayerStats>();

            if (player != null)
            {
                player.TakeDamage(damage);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
