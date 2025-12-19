using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public int lives = 3;
    public int ammo = 50;
    public string currentWeapon = "Normal";

    public int score = 0;

    void Start()
    {
        health = maxHealth;

        // Dispara eventos iniciais
        GameEvents.OnHealthChanged?.Invoke(health);
        GameEvents.OnLivesChanged?.Invoke(lives);
        GameEvents.OnAmmoChanged?.Invoke(ammo);
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon);
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    // ====== MÉTODOS ======

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameEvents.OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        lives--;
        GameEvents.OnLivesChanged?.Invoke(lives);

        health = maxHealth;
        GameEvents.OnHealthChanged?.Invoke(health);
    }

    public void UseAmmo(int amount)
    {
        ammo -= amount;
        GameEvents.OnAmmoChanged?.Invoke(ammo);
    }

    public void ChangeWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon);
    }

    public void AddScore(int value)
    {
        score += value;
        GameEvents.OnScoreChanged?.Invoke(score);
    }
}
