using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject bulletPrefab;

    public float fireRate;
    public int damage;
    public float bulletSpeed;
}
