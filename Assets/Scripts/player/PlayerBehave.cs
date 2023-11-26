using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    [Header("僵直时间")][SerializeField]
    private float hittedTime;

    [Header("炸弹预制体")]
    public GameObject boom;

    private bool isDied = false;  //角色是否死亡
    private PlayerMovement playerMovement;  //角色移动脚本
    private PlayerInfo playerInfo; //角色信息脚本
    private Transform weaponPoint; //角色手持武器的位置
    private string firstWeapon;  //初始武器
    private float throwForce = 0f;  //投掷炸药的力度
    private Vector2 maxForce = new Vector2(250f, 150f);
    private Vector2 minForce = new Vector2(150f, 150f);
    private WeaponFactory weaponFactory;

    private float diedPositionY = -8f; //角色Y轴低于此值时判定为死亡

    void Start()
    {
        weaponFactory = Resources.Load<WeaponFactory>("Factory/WeaponFactory");
        playerMovement = GetComponent<PlayerMovement>();
        playerInfo = GetComponent<PlayerInfo>();
        firstWeapon = playerInfo.getFirstWeapon();
        weaponPoint = transform.Find("weaponPoint").transform;
        setBaseWeapon();
    }

    private void FixedUpdate()
    {
        onAttack();
        onThrowBoom();
    }

    void Update()
    {
        onPlayerDied();
    }

    /// <summary>
    /// 开火函数
    /// </summary>
    void onAttack()
    {
        if(Input.GetKey(KeyCode.J))
        {
            transform.Find("weapon").GetComponent<weapon>().onAttack();
        }    
    }

    /// <summary>
    /// 投掷炸药
    /// </summary>
    void onThrowBoom()
    {
        if(Input.GetKey(KeyCode.K))
        {
            throwForce += 150f * Time.fixedDeltaTime;
        }
        else if(throwForce != 0)
        {
            Vector3 tempScale = transform.lossyScale;
            Vector2 tempForce = new Vector2(1, 0.5f) * throwForce;
            Vector2 finForce = Vector2.Min(Vector2.Max(minForce, tempForce), maxForce);
            finForce = finForce * tempScale;
            Debug.Log(finForce);
            ObjectPoolManager.SpawnObject(boom, weaponPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(finForce);
            throwForce = 0;
        }
    }

    /// <summary>
    /// 受击函数
    /// </summary>
    public void onHitted()
    {
        StartCoroutine("hittedCounter");
    }

    IEnumerator hittedCounter()
    {
        playerMovement.setIsHitted(true);
        yield return new WaitForSeconds(hittedTime);
        playerMovement.setIsHitted(false);
    }

    /// <summary>
    /// 设置武器
    /// </summary>
    /// <param name="name"></param>
    public void setWeapon(string name)
    {
        if (transform.Find("weapon") != null)
            Destroy(transform.Find("weapon").gameObject);

        GameObject temp = weaponFactory.GetWeapon(name);
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
    /// 设置初始武器
    /// </summary>
    public void setBaseWeapon()
    {
        setWeapon(firstWeapon);
        /*if (transform.Find("weapon") != null)
            Destroy(transform.Find("weapon").gameObject);

        GameObject temp = Instantiate(weaponFactory.GetWeapon(firstWeapon), this.transform);
        temp.transform.position = weaponPoint.position;
        temp.name = "weapon";

        //设置武器阵营
        if (transform.Find("weapon") != null)
            transform.Find("weapon").GetComponent<weapon>().setTeam(playerInfo.getTeam());
        else Debug.Log(gameObject.name + " Weapon not found!");*/
    }

    /// <summary>
    /// isDied Set方法
    /// </summary>
    public void setIsDied(bool isDied)
    {
        this.isDied = isDied;
    }

    public void onPlayerDied()
    {
        if(transform.position.y<diedPositionY && !isDied)
        {
            isDied = true;
            this.GetComponent<PlayerInfo>().lifeCount--;
            GameObject initPlayer = GameObject.Find("initPlayer");
            initPlayer.GetComponent<initPlayer>().regeneratePlayer(this.gameObject);
        }
    }
}
