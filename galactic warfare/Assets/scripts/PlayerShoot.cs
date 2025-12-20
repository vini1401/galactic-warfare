using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float nextFireTime;
    private PlayerStats playerStats;

    public WeaponType currentWeapon = WeaponType.Normal;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        SetWeapon(currentWeapon);
    }

    void Update()
    {
        HandleWeaponSwitch();
        HandleShoot();
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetWeapon(WeaponType.Normal);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetWeapon(WeaponType.Double);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetWeapon(WeaponType.Spread);
    }

    // 🔴 AGORA É PUBLIC
    public void SetWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;

        if (playerStats != null)
            playerStats.ChangeWeapon(weapon);
    }

    void HandleShoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            if (playerStats.ammo > 0)
            {
                Shoot();
                playerStats.UseAmmo(1);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        switch (currentWeapon)
        {
            case WeaponType.Normal:
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                break;

            case WeaponType.Double:
                Instantiate(bulletPrefab, firePoint.position + Vector3.up * 0.2f, Quaternion.identity);
                Instantiate(bulletPrefab, firePoint.position + Vector3.down * 0.2f, Quaternion.identity);
                break;

            case WeaponType.Spread:
                Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, -15));
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 15));
                break;
        }
    }
}
