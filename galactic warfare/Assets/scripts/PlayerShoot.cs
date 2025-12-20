using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    public WeaponType currentWeapon = WeaponType.Normal;

    private float nextFireTime;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon.ToString());
    }

    void Update()
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

        // Teste: trocar arma com tecla
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(WeaponType.Normal);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(WeaponType.Double);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(WeaponType.Spread);
    }

    void Shoot()
    {
        switch (currentWeapon)
        {
            case WeaponType.Normal:
                ShootNormal();
                break;

            case WeaponType.Double:
                ShootDouble();
                break;

            case WeaponType.Spread:
                ShootSpread();
                break;
        }
    }

    void ShootNormal()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    void ShootDouble()
    {
        Instantiate(bulletPrefab, firePoint.position + Vector3.up * 0.2f, Quaternion.identity);
        Instantiate(bulletPrefab, firePoint.position + Vector3.down * 0.2f, Quaternion.identity);
    }

    void ShootSpread()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 15));
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, -15));
    }

    void ChangeWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        GameEvents.OnWeaponChanged?.Invoke(currentWeapon.ToString());
    }
}
