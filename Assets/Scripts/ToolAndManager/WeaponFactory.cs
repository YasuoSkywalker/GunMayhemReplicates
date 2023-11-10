using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponFactory", menuName ="Factory/WeaponFactory")]
public class WeaponFactory : ScriptableObject
{
    #region pistol
    [SerializeField]
    private weapon bull;
    [SerializeField]
    private weapon glock;
    [SerializeField]
    private weapon gold;
    [SerializeField]
    private weapon m1911;
    #endregion

    #region rifle
    [SerializeField]
    private weapon ak47;
    [SerializeField]
    private weapon bizon;
    [SerializeField]
    private weapon m12;
    [SerializeField]
    private weapon p90;
    #endregion

    #region shotGun
    [SerializeField]
    private weapon ithaca;
    [SerializeField]
    private weapon ksg;
    [SerializeField]
    private weapon leaver;
    [SerializeField]
    private weapon sawnOff;
    #endregion

    #region sniper
    [SerializeField]
    private weapon sniper501;
    [SerializeField]
    private weapon awn;
    [SerializeField]
    private weapon hk;
    [SerializeField]
    private weapon m95;
    #endregion
}
