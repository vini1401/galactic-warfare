using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text healthText;
    public Text livesText;
    public Text ammoText;
    public Text weaponText;
    public Text scoreText;

    void OnEnable()
    {
        GameEvents.OnHealthChanged += UpdateHealth;
        GameEvents.OnLivesChanged += UpdateLives;
        GameEvents.OnAmmoChanged += UpdateAmmo;
        GameEvents.OnWeaponChanged += UpdateWeapon;
        GameEvents.OnScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        GameEvents.OnHealthChanged -= UpdateHealth;
        GameEvents.OnLivesChanged -= UpdateLives;
        GameEvents.OnAmmoChanged -= UpdateAmmo;
        GameEvents.OnWeaponChanged -= UpdateWeapon;
        GameEvents.OnScoreChanged -= UpdateScore;
    }

    void UpdateHealth(int value)
    {
        healthText.text = "HP: " + value;
    }

    void UpdateLives(int value)
    {
        livesText.text = "Lives: " + value;
    }

    void UpdateAmmo(int value)
    {
        ammoText.text = "Ammo: " + value;
    }

    void UpdateWeapon(string value)
    {
        weaponText.text = "Weapon: " + value;
    }

    void UpdateScore(int value)
    {
        scoreText.text = "Score: " + value;
    }
}
