using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracingPlayer : Action
{
    protected detectiveScript detective;
    protected enermyMovement enermyMov;
    protected enermyBehavior enermyBehave;
    protected Rigidbody2D enermyRb;
    public SharedGameObject playerObj;

    private GameObject playerCurrentPlat;
    private float localScaleX;
    private bool canVerticalJump = true;

    private bool isFixedUpdateFinish = false;

    public override void OnAwake()
    {
        detective = GetComponent<detectiveScript>();
        enermyMov = GetComponent<enermyMovement>();
        enermyBehave = GetComponent<enermyBehavior>();
        enermyRb = GetComponent<Rigidbody2D>();
    }

    public override void OnStart()
    {
        isFixedUpdateFinish = false;
    }

    public override void OnFixedUpdate()
    {
        //Debug.Log("Fixed");
        playerCurrentPlat = playerObj.Value.GetComponent<PlayerMovement>().getCurrentGround();
        if (!enermyMov.getIsHitted())
        {
            faceToPlayer();
            int temp = Random.Range(0, 10);
            if (0 < temp && temp < 4 && !enermyMov.getIsHitted())
            {
                wander();
            }
            else if(!enermyMov.getIsHitted())
            {
                keepHorizontal();
            }

            keepDistance();
            attackPlayer();
        }
        isFixedUpdateFinish = true;
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log("updating");
        if (!isFixedUpdateFinish)
            return TaskStatus.Running;
        else
            return TaskStatus.Success;
    }

    /// <summary>
    /// 朝向玩家
    /// </summary>
    void faceToPlayer()
    {
        if(Mathf.Abs(playerObj.Value.transform.position.y - transform.position.y) <= 1.5f && 
            Mathf.Abs(playerObj.Value.transform.position.x - transform.position.x) >= 1f)
        {
            //玩家在右边
            if (transform.position.x <= playerObj.Value.transform.position.x)
            {
                enermyMov.FaceToRight();
            }
            else
            {
                enermyMov.FaceToLeft();
            }
        }
    }

    /// <summary>
    /// 与玩家保持水平
    /// </summary>
    void keepHorizontal()
    {
        //与玩家处在同一平台上
        if(detective.currentPlat.Equals(playerCurrentPlat))
        {
            if(playerObj.Value.transform.position.y > transform.position.y &&
                Mathf.Abs(playerObj.Value.transform.position.y-transform.position.y)>=0.5f &&
                detective.isOnGround)
            {
                enermyMov.JumpOnce();
            }
        }
        else //不处在同一平台上
        {
            if (playerObj.Value.transform.position.y > transform.position.y && 
                Mathf.Abs(playerObj.Value.transform.position.y - transform.position.y) >= 0.5f)
            {
                if(detective.topHasPlat && detective.isOnGround)
                {
                    if(canVerticalJump)
                    {
                        StartCoroutine(verticalJumpCD());
                        enermyRb.velocity = Vector2.zero;
                        enermyMov.MoveToTop();
                    }
                }
                else if(detective.isOnGround)
                {
                    wander();
                }
            }
            else if(playerObj.Value.transform.position.y < transform.position.y && 
                Mathf.Abs(playerObj.Value.transform.position.y - transform.position.y) >= 0.5f)
            {
                if (detective.botHasPlat && detective.isOnGround)
                {
                    if(canVerticalJump)
                    {
                        StartCoroutine(verticalJumpCD());
                        enermyRb.velocity = Vector2.zero;
                        enermyMov.MoveToBottom();
                    }
                }
                else if (detective.isOnGround)
                {
                    wander();
                }
            }
        }
    }

    /// <summary>
    /// 巡逻总函数
    /// </summary>
    void wander()
    {
        int x = Random.Range(0, 10);
        if (0 <= x && x <= 5 && enermyRb.velocity.y == 0 && detective.isOnGround)
        {
            horizontalWander();
        }
        else
        {
            if (detective.isOnGround && canVerticalJump)
            {
                StartCoroutine(verticalJumpCD());
                verticalWander();
            }

        }
    }

    /// <summary>
    /// 水平巡逻
    /// </summary>
    private void horizontalWander()
    {
        localScaleX = transform.localScale.x;
        if (localScaleX == 1)
        {
            if (!detective.isRightCliff)
                enermyMov.MoveToRight();
            else
                enermyMov.MoveToLeft();
        }
        if (localScaleX == -1)
        {
            if (!detective.isRightCliff)
                enermyMov.MoveToLeft();
            else
                enermyMov.MoveToRight();
        }
    }

    /// <summary>
    /// 垂直巡逻
    /// </summary>
    private void verticalWander()
    {
        if (playerObj.Value.transform.position.y > transform.position.y)
        {
            //向上
            if (detective.topFrontHasPlat && detective.topHasPlat)
            {
                //如果前上方和正上方都有平台
                enermyRb.velocity = new Vector2(enermyMov.getMoveSpeed() * 0.3f * Time.fixedDeltaTime * transform.localScale.x, 0);
                enermyMov.MoveToTop();
            }
            else if (detective.topFrontHasPlat && !detective.topHasPlat)
            {
                //如果前上方有平台但是正上方没有平台
                enermyRb.velocity = new Vector2(enermyMov.getMoveSpeed() * 0.5f * Time.fixedDeltaTime * transform.localScale.x, 0);
                enermyMov.MoveToTop();
            }
            else if (detective.topHasPlat)
            {
                //如果只有正上方有平台
                enermyRb.velocity = Vector2.zero;
                enermyMov.MoveToTop();
            }
        }
        else
        {
            //向下
            if (playerObj.Value.transform.position.y < transform.position.y)
            {
                //如果前下方和正下方都有平台
                enermyRb.velocity = new Vector2(enermyMov.getMoveSpeed() * 0.3f * Time.fixedDeltaTime * transform.localScale.x, 0);
                enermyMov.MoveToBottom();
            }
            else if (detective.botFrontHasPlat && !detective.botHasPlat)
            {
                //如果前下方有平台但是正下方没有平台
                enermyRb.velocity = new Vector2(enermyMov.getMoveSpeed() * 0.5f * Time.fixedDeltaTime * transform.localScale.x, 0);
                enermyMov.MoveToBottom();
            }
            else if (detective.botHasPlat)
            {
                //如果只有正下方有平台
                enermyRb.velocity = Vector2.zero;
                enermyMov.MoveToBottom();
            }
        }
    }

    /// <summary>
    /// 缩短并保持与玩家的距离
    /// </summary>
    void keepDistance()
    {
        //远离悬崖
        if(detective.isLeftCliff && !detective.isRightCliff && detective.isOnGround && !enermyMov.getIsHitted())
        {
            int temp = Random.Range(0, 10);
            if(temp >= 6)
            {
                if (transform.localScale.x > 0)
                    enermyMov.MoveToRight();
                else
                    enermyMov.MoveToLeft();
            }
        }

        if(Mathf.Abs(playerObj.Value.transform.position.x-transform.position.x)>=10f
            && playerCurrentPlat.Equals(detective.currentPlat) && !enermyMov.getIsHitted())
        {
            faceToPlayer();
            if(transform.localScale.x == 1)
            {
                enermyMov.MoveToRight();
            }
            else
            {
                enermyMov.MoveToLeft();
            }
        }
    }

    /// <summary>
    /// 攻击玩家
    /// </summary>
    void attackPlayer()
    {
        if(Mathf.Abs(playerObj.Value.transform.position.y - transform.position.y) <= 1.0f)
            enermyBehave.enermyAttack();
    }

    IEnumerator verticalJumpCD()
    {
        canVerticalJump = false;
        yield return new WaitForSeconds(1.5f);
        canVerticalJump = true;
    }
}
