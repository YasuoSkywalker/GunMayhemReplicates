using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    public static WeaponPool instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("武器池")]
    [AYellowpaper.SerializedCollections.SerializedDictionary("name","weaponObject")]
    public AYellowpaper.SerializedCollections.SerializedDictionary<string, GameObject> weaponsPool = new AYellowpaper.SerializedCollections.SerializedDictionary<string, GameObject>();

    public GameObject getWeapon(string name)
    {
        if (weaponsPool.ContainsKey(name))
        {
            return weaponsPool[name];
        }
        else
        {
            Debug.LogError("error: "+name+" Weapon did not exit!");
            return null;
        }
    }

    public static WeaponPool getInstance()
    {
        return instance;
    }
}
