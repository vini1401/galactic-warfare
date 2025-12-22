using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int lives;
    public int ammo;
    public WeaponType currentWeapon;
    public int score;
}

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public int lives = 3;
    public int ammo = 50;
    public WeaponType currentWeapon = WeaponType.Normal;

    public int score = 0;

    private string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/savegame.json";
    }

    void Start()
    {
        NewGame();
    }

    // ================= CONTROLE DE JOGO =================

    public void NewGame()
    {
        health = maxHealth;
        lives = 3;
        ammo = 50;
        currentWeapon = WeaponType.Normal;
        score = 0;

        UpdateHUD();
    }

    public void ContinueGame()
    {
        LoadGame();
        UpdateHUD();
    }

    // ================= HUD =================

    void UpdateHUD()
    {
        GameEvents.OnHealthChanged?.Invoke(health);
        GameEvents.OnLivesChanged?.Invoke(lives);
        GameEvents.OnAmmoChanged?.Invoke(ammo);
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon.ToString());
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    // ================= GAMEPLAY =================

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameEvents.OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        lives--;
        GameEvents.OnLivesChanged?.Invoke(lives);

        // Desativa o player via Pool
        PlayerPool.Instance.DespawnPlayer();

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            return;
        }

        // Respawn
        Invoke(nameof(Respawn), 1.5f);
    }

    void Respawn()
    {
        health = maxHealth;
        ammo = 50;

        PlayerPool.Instance.SpawnPlayer();
        UpdateHUD();
    }

    public void UseAmmo(int amount)
    {
        ammo -= amount;
        if (ammo < 0) ammo = 0;
        GameEvents.OnAmmoChanged?.Invoke(ammo);
    }

    public void ChangeWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon.ToString());
    }

    public void AddScore(int value)
    {
        score += value;
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    // ================= SAVE / LOAD =================

    public void SaveGame()
    {
        PlayerData data = new PlayerData
        {
            health = health,
            lives = lives,
            ammo = ammo,
            currentWeapon = currentWeapon,
            score = score
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
            return;

        string json = File.ReadAllText(savePath);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        health = data.health;
        lives = data.lives;
        ammo = data.ammo;
        currentWeapon = data.currentWeapon;
        score = data.score;
    }
}
