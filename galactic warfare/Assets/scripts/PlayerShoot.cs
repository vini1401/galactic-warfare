using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float nextFireTime;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            if (playerStats != null && playerStats.ammo > 0)
            {
                Shoot();
                playerStats.UseAmmo(1);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
