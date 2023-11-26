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
                return Instantiate(bull);
            case "glock":
                return Instantiate(glock);
            case "gold":
                return Instantiate(gold);
            case "m1911":
                return Instantiate(m1911);
            case "ak47":
                return Instantiate(ak47);
            case "bizon":
                return Instantiate(bizon);
            case "m12":
                return Instantiate(m12);
            case "p90":
                return Instantiate(p90);
            case "ithaca":
                return Instantiate(ithaca);
            case "ksg":
                return Instantiate(ksg);
            case "leaver":
                return Instantiate(leaver);
            case "sawnOff":
                return Instantiate(sawnOff);
            case "501":
                return Instantiate(sniper501);
            case "awn":
                return Instantiate(awn);
            case "hk":
                return Instantiate(hk);
            case "m95":
                return Instantiate(m95);
            default:
                Debug.LogWarning("GameObject:" + GameObjectName + "Not Found");
                return Instantiate(bull);
        }
    }
}
