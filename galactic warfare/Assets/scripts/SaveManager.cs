using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string savePath => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerStats player, PlayerShoot shoot)
    {
        SaveData data = new SaveData
        {
            health = player.health,
            lives = player.lives,
            ammo = player.ammo,
            score = player.score,
            weaponType = (int)shoot.currentWeapon
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Jogo salvo em: " + savePath);
    }

    public static bool Load(PlayerStats player, PlayerShoot shoot)
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Nenhum save encontrado");
            return false;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        player.health = data.health;
        player.lives = data.lives;
        player.ammo = data.ammo;
        player.score = data.score;

        shoot.SetWeapon((WeaponType)data.weaponType);

        // Atualiza HUD
        GameEvents.OnHealthChanged?.Invoke(player.health);
        GameEvents.OnLivesChanged?.Invoke(player.lives);
        GameEvents.OnAmmoChanged?.Invoke(player.ammo);
        GameEvents.OnScoreChanged?.Invoke(player.score);

        Debug.Log("Jogo carregado");
        return true;
    }
}
