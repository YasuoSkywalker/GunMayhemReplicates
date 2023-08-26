using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("玩家所属阵营")]
    public string teams = "A";
    [Header("玩家技能编号")]
    public int skillNum = 0;
    [Header("玩家初始武器")]
    public string firstWeapon;
    [Header("玩家生命值")]
    public int lifeCount = 10;

    public void Start()
    {
        //setWeapon(this.firstWeapon);
    }

    /// <summary>
    /// set方法
    /// </summary>
    public void setTeams(string team)
    {
        this.teams = team;
    }

    public void setSkillNum(int skillNum)
    {
        this.skillNum = skillNum;
    }

    public void setFirstWeapon(string firstWeapon)
    {
        this.firstWeapon = firstWeapon;
    }

    public void setLifeCount(int lifeCount)
    {
        this.lifeCount = lifeCount;
    }

    public void addLifeCounnt(int addNum)
    {
        this.lifeCount += addNum;
    }

    /// <summary>
    /// get方法
    /// </summary>
    public string getTeam()
    {
        return this.teams;
    }

    public int getSkillNum()
    {
        return this.skillNum;
    }

    public string getFirstWeapon()
    {
        return this.firstWeapon;
    }

    public int getLifeCount()
    {
        return this.lifeCount;
    }
}
