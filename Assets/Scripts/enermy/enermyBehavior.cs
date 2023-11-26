using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyBehavior : MonoBehaviour
{
    private enermyMovement enermyMov;

    private PlayerInfo playerInfo; //角色信息脚本
    private Transform weaponPoint; //角色手持武器的位置
    private string firstWeapon;  //初始武器
    private bool isDied = false;  //角色是否死亡
    private float diedPositionY = -8f; //角色Y轴低于此值时判定为死亡
    private WeaponFactory weaponFactory;
    [SerializeField]
    [Header("敌人重生点")]
    private Vector3 enermyRegeneratePoint;
    
    [SerializeField]
    private float hittedTime;

    private void Start()
    {
        weaponFactory = Resources.Load<WeaponFactory>("Factory/WeaponFactory");
        enermyMov = GetComponent<enermyMovement>();
        playerInfo = GetComponent<PlayerInfo>();
        firstWeapon = playerInfo.getFirstWeapon();
        weaponPoint = transform.Find("weaponPoint").transform;
        setBaseWeapon();
    }

    private void Update()
    {
        onPlayerDied();
    }

    /// <summary>
    /// 敌人开火方法
    /// </summary>
    public void enermyAttack()
    {
        transform.Find("weapon").GetComponent<weapon>().onAttack();
    }

    /// <summary>
    /// 敌人受击方法
    /// </summary>
    public void onHitted()
    {
        StartCoroutine("hittedCounter");
    }

    /// <summary>
    /// 设置初始武器
    /// </summary>
    public void setBaseWeapon()
    {
        if (transform.Find("weapon") != null)
            Destroy(transform.Find("weapon").gameObject);

        GameObject temp = weaponFactory.GetWeapon(firstWeapon);
        temp.transform.SetParent(transform);
        temp.transform.position = weaponPoint.position;
        temp.transform.localScale = Vector3.one;  //修复武器反向的Bug
        temp.name = "weapon";

        //设置武器阵营
        if (transform.Find("weapon") != null)
            transform.Find("weapon").GetComponent<weapon>().setTeam(playerInfo.getTeam());
        else Debug.Log(gameObject.name + " Weapon not found!");
    }

    /// <summary>
    /// 敌人死亡处理方法
    /// </summary>
    public void onPlayerDied()
    {
        if (transform.position.y < diedPositionY && !isDied)
        {
            isDied = true;
            //播放死亡音效
            GetComponent<SoundManager>().playAudio("onDied");
            playerInfo.lifeCount--;
            if(playerInfo.lifeCount > 0)
            {
                transform.position = enermyRegeneratePoint;
                isDied = false;
            }
            else
            {
                GameObject.Find("enermyManager").GetComponent<enermyManager>().decreaseEnermyCount();
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator hittedCounter()
    {
        enermyMov.setIsHitted(true);
        yield return new WaitForSeconds(hittedTime);
        enermyMov.setIsHitted(false);
    }
}
