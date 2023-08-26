using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    /// <summary>
    /// 枪械数值
    /// </summary>
    [Header("攻击间隔")]
    public float attackSpacing;
    [Header("子弹速度")]
    public float bulletSpeed;
    [Header("子弹击退")]
    public float bulletRepel;
    [Header("子弹容量")]
    public int bulletTotalNumber;
    [Header("后坐力")]
    public float recoil;
    [Header("是否为基础武器")]
    public bool isBase;
    [Header("枪械所属队伍")]
    public string team;

    /// <summary>
    /// 枪械控制
    /// </summary>
    protected bool canAttack;  //是否能开火
    protected int bulletNowNumber; //当前的子弹数量
    protected GameObject firePoint; //子弹生成点

    [SerializeField]
    [Header("子弹预制体")]
    protected GameObject bullet;

    [SerializeField] 
    [Header("开火点")]
    protected GameObject attackPoint;

    [SerializeField]
    [Header("火光特效")]
    protected GameObject fireSpark;

    [SerializeField]
    [Header("火光特效持续时间")]
    protected float sparkTime;

    protected SoundManager soundManager;

    protected ParticleSystem slotParticle;
    protected Animator anim; //枪械动画机
    protected bool needChange = true;

    protected virtual void Start()
    {
        canAttack = true;
        if(transform.parent.GetComponent<PlayerInfo>().getSkillNum() == 4)
        {
            needChange = false;
        }
        bulletNowNumber = bulletTotalNumber;
        anim = GetComponent<Animator>();
        fireSpark.SetActive(false);
        slotParticle = transform.Find("slotParticle").GetComponent<ParticleSystem>();
        soundManager = GetComponent<SoundManager>();
    }

    protected void Update()
    {
        
    }

    //开火函数
    public virtual void onAttack()
    {
        if (canAttack && bulletNowNumber > 0)
        {
            soundManager.playAudio("shoot");
            createEffect();
            if(bulletNowNumber>0)
            {
                StartCoroutine("attackCD");
            }
            else if(isBase && needChange)
            {
                soundManager.playAudio("change");
                anim.SetBool("isChanging", true);
            }
            else if(isBase && !needChange)
            {
                //技能四，基础武器无限子弹
                bulletNowNumber = bulletTotalNumber;
            }
            else
            {
                //丢弃武器
                transform.parent.GetComponent<PlayerBehave>().setBaseWeapon();
            }
        }
    }

    //创建子弹
    protected void createBullet()
    {
        createSlot();
        GameObject temp =  ObjectPoolManager.SpawnObject(bullet,attackPoint.transform.position, Quaternion.identity);
        if(transform.lossyScale.x>0)
        {
            temp.GetComponent<bullet>().setProp(Vector3.right, bulletSpeed, bulletRepel, team);
        }
        else
        {
            temp.GetComponent<bullet>().setProp(Vector3.left, bulletSpeed, bulletRepel, team);
        }
        //slotParticle.Stop();
        bulletNowNumber--;
    }

    //射击间隔
    protected virtual IEnumerator attackCD()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpacing);
        canAttack = true;
    }

    //创建火光
    protected IEnumerator createSpark()
    {
        fireSpark.SetActive(true);
        yield return new WaitForSeconds(sparkTime);
        fireSpark.SetActive(false);
    }

    //开火效果的创建：创建子弹和火光,添加后坐力
    public void createEffect()
    {
        if (bulletNowNumber > 0)
        {
            createBullet();
            addRecoil();
            StartCoroutine("createSpark");
        }
    }

    //弹壳抛射效果
    protected void createSlot()
    {
        if(!slotParticle.isPlaying)
        {
            slotParticle.Play();
        }
        else
        {
            slotParticle.Stop();
            slotParticle.Play();
        }
    }

    //添加后坐力
    protected void addRecoil()
    {
        //技能二，武器没有后坐力
        if(transform.parent.GetComponent<PlayerInfo>().getSkillNum() != 2)
        {
            Vector2 recoilForce = new Vector2(transform.lossyScale.x * (-recoil), 1);
            transform.parent.GetComponent<Rigidbody2D>().AddForce(recoilForce);
        }
    }

    //退出换弹状态
    public virtual void exitChanging()
    {
        anim.SetBool("isChanging", false);
        bulletNowNumber = bulletTotalNumber;
    }

    public void setTeam(string team)
    {
        this.team = team;
    }

    //bulletNowNumber get方法
    public int getWeaponNowBullet()
    {
        return this.bulletNowNumber;
    }
}
