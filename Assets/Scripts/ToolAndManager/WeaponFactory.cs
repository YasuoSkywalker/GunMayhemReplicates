using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponFactory", menuName = "Factory/WeaponFactory")]
public class WeaponFactory : ScriptableObject
{
    #region pistol
    [SerializeField]
    private GameObject bull;
    [SerializeField]
    private GameObject glock;
    [SerializeField]
    private GameObject gold;
    [SerializeField]
    private GameObject m1911;
    #endregion

    #region rifle
    [SerializeField]
    private GameObject ak47;
    [SerializeField]
    private GameObject bizon;
    [SerializeField]
    private GameObject m12;
    [SerializeField]
    private GameObject p90;
    #endregion

    #region shotGun
    [SerializeField]
    private GameObject ithaca;
    [SerializeField]
    private GameObject ksg;
    [SerializeField]
    private GameObject leaver;
    [SerializeField]
    private GameObject sawnOff;
    #endregion

    #region sniper
    [SerializeField]
    private GameObject sniper501;
    [SerializeField]
    private GameObject awn;
    [SerializeField]
    private GameObject hk;
    [SerializeField]
    private GameObject m95;
    #endregion

    public GameObject GetWeapon(string GameObjectName)
    {
        switch (GameObjectName)
        {
            case "bull":
                return bull;
            case "glock":
                return glock;
            case "gold":
                return gold;
            case "m1911":
                return m1911;
            case "ak47":
                return ak47;
            case "bizon":
                return bizon;
            case "m12":
                return m12;
            case "p90":
                return p90;
            case "ithaca":
                return ithaca;
            case "ksg":
                return ksg;
            case "leaver":
                return leaver;
            case "sawnOff":
                return sawnOff;
            case "501":
                return sniper501;
            case "awn":
                return awn;
            case "hk":
                return hk;
            case "m95":
                return m95;
            default:
                Debug.LogWarning("GameObject:" + GameObjectName + "Not Found");
                return bull;
        }
    }
}
