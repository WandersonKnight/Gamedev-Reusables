using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI weaponNameText;

    GameObject player;
    WeaponMaster weaponMaster;
    int lastAmmo;
    int totalAmmo;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponMaster = player.GetComponentInChildren<ChargeWeapon>();
        
        totalAmmo = weaponMaster.ammo;
        lastAmmo = totalAmmo;

        ammoText.SetText("<size=68><color=red>" + lastAmmo + "<size=55><color=white> /" + totalAmmo);
        weaponNameText.text = "B.L.U.E";
    }

    void Update()
    {
        Debug.Log("Start " + weaponMaster.currentAmmo);
        //weaponMaster.currentAmmo++;

        if (lastAmmo != weaponMaster.currentAmmo)
        {
            lastAmmo = weaponMaster.currentAmmo;
            Debug.Log("Update" + lastAmmo);

            ammoText.SetText("<size=68><color=red>" + lastAmmo + "<size=55><color=white> /" + totalAmmo);
        }
    }
}
