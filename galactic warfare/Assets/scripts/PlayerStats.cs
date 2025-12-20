using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int lives;
    public int ammo;
    public string currentWeapon;
    public int score;
}

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public int lives = 3;
    public int ammo = 50;
    public string currentWeapon = "Normal";

    public int score = 0;

    private string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/savegame.json";
    }

    void Start()
    {
        // NÃO carrega automaticamente
        NewGame();
    }

    // ================= CONTROLE DE JOGO =================

    public void NewGame()
    {
        health = maxHealth;
        lives = 3;
        ammo = 50;
        currentWeapon = "Normal";
        score = 0;

        UpdateHUD();
        Debug.Log("Novo jogo iniciado");
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
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon);
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    // ================= GAMEPLAY =================

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

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
            return;
        }

        health = maxHealth;
        GameEvents.OnHealthChanged?.Invoke(health);
    }

    public void UseAmmo(int amount)
    {
        ammo -= amount;
        if (ammo < 0) ammo = 0;
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

    // ================= SAVE / LOAD =================

    public void SaveGame()
    {
        PlayerData data = new PlayerData
        {
            health = this.health,
            lives = this.lives,
            ammo = this.ammo,
            currentWeapon = this.currentWeapon,
            score = this.score
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Jogo salvo em: " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Nenhum save encontrado");
            return;
        }

        string json = File.ReadAllText(savePath);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        health = data.health;
        lives = data.lives;
        ammo = data.ammo;
        currentWeapon = data.currentWeapon;
        score = data.score;

        Debug.Log("Save carregado");
    }

    // ================= TESTE TEMPORÁRIO =================

    void Update()
    {
        // F5 → Salvar
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        // F9 → Continuar
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ContinueGame();
        }

        // F1 → Novo Jogo
        if (Input.GetKeyDown(KeyCode.F1))
        {
            NewGame();
        }
    }
}
