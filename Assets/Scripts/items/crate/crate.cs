using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{
    private string weaponName = "bull";
    public event Action<bool> onCrateDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerBehave>().setWeapon(weaponName);
            onCrateDestroyed(false);
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    public void setWeaponName(string weaponName)
    {
        this.weaponName = weaponName;
    }
}
