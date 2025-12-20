using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float nextFireTime;

    public WeaponType currentWeapon = WeaponType.Normal;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void ShootDouble()
    {
        Instantiate(bulletPrefab, firePoint.position + Vector3.up * 0.2f, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position + Vector3.down * 0.2f, firePoint.rotation);
    }

    void ShootSpread()
    {
        for (int i = -1; i <= 1; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Quaternion.Euler(0, 0, i * 15) * Vector2.right * 10f;
            }
        }
    }

    // 🔥 ESTE MÉTODO RESOLVE O ERRO
    public void SetWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;

        // Atualiza HUD
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.ChangeWeapon(weapon.ToString());
        }
    }
}
