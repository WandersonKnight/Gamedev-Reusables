using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : WeaponMaster
{
    PoolHandler poolMethods;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject output;

    List<GameObject> projectilePool = new List<GameObject>();

    public float chargeTime = 1f;
    float chargingTime;

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
        ListenButtonUp();
    }

    private void FixedUpdate()
    {
        ShootCheck();
        ReloadCheck();
    }

    public override void ButtonUp()
    {
        if (currentAmmo > 0 && chargingTime > chargeTime)
        {
            GameObject newProjectile = poolMethods.GetPool(projectilePool);
            newProjectile.SetActive(true);
            newProjectile.transform.position = output.transform.position;

            Rigidbody rigidbody = newProjectile.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(output.transform.forward.normalized * projectileSpeed, ForceMode.Impulse);

            chargingTime = 0;
            currentAmmo--;
        }
        else
        {
            chargingTime = 0;
        }
    }

    public override void Shoot()
    {
        chargingTime += Time.deltaTime;
    }
}
