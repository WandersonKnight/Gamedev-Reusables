using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : WeaponMaster
{
    PoolHandler poolMethods;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject output;

    List<GameObject> projectilePool = new List<GameObject>();

    float nextTimeToFire = 0f;

    private void Awake()
    {
        poolMethods = GetComponent<PoolHandler>();
        projectilePool = poolMethods.CreatePool(projectile, projectilePoolSize);
        currentAmmo = ammo;
    }

    private void Update()
    {
        ListenShootInput();
        ListenReloadInput();
    }

    private void FixedUpdate()
    {
        ShootCheck();
        ReloadCheck();
    }

    public override void Shoot()
    {
        if(currentAmmo > 0 && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;

            GameObject newProjectile = poolMethods.GetPool(projectilePool);
            newProjectile.SetActive(true);
            newProjectile.transform.position = output.transform.position;

            Rigidbody rigidbody = newProjectile.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(output.transform.forward.normalized * projectileSpeed, ForceMode.Impulse);

            currentAmmo--;
        }
    }
}
