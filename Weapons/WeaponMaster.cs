using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMaster : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public float reloadTime = 3f;
    public float fireRate = 4f;
    public int projectilePoolSize = 25;
    public int currentAmmo;
    public int ammo = 20;

    public bool reloading = false;
    public bool canShoot = true;

    public void ListenButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ButtonUp();
        }
    }

    public virtual void ButtonUp()
    {

    }

    public void ListenShootInput()
    {
        if (Input.GetMouseButton(0))
        {
            canShoot = true;
        }
    }

    public void ShootCheck()
    {
        if (canShoot == true && reloading == false)
        {
            Shoot();
            canShoot = false;
        }
    }

    public virtual void Shoot()
    {

    }

    public void ListenReloadInput()
    {
        if (Input.GetKey("r"))
        {
            reloading = true;
        }
    }

    public void ReloadCheck()
    {
        if(reloading == true)
        {
            Reload();
        }
    }

    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammo;
        reloading = false;
    }
}
