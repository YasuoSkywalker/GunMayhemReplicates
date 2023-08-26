using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wander : Action
{
    protected detectiveScript detective;  //探测脚本
    protected enermyMovement enermyMov;  //敌人移动脚本
    protected Rigidbody2D enermyRb;  //敌人2D刚体
    public SharedGameObject playerObj;  //玩家物体

    private bool canVerticalJump = true; //移动方向
    private float localScaleX;  //记录玩家的loaclScale.x
    private bool isFixedUpdateFinish = false;

    public override void OnAwake()
    {
        detective = GetComponent<detectiveScript>();
        enermyMov = GetComponent<enermyMovement>();
        enermyRb = GetComponent<Rigidbody2D>();
    }

    public override void OnFixedUpdate()
    {
        Debug.Log("FixedUpdating");
        int x = Random.Range(0, 10);
        if(0<=x && x<=5 && enermyRb.velocity.y == 0 && detective.isOnGround)
        {
            Debug.Log("horizontalWander");
            horizontalWander();
        }
        else
        {
            if(detective.isOnGround && canVerticalJump)
            {
                Debug.Log("verticleWander");
                StartCoroutine(verticalJumpCD());
                verticalWander();
            }
                
        }

        if (searchPlayer())
        {
            Debug.Log("searchPlayerSuccess");
            isFixedUpdateFinish = true;
        }
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log("Updating,isFixedUpdateFinish:" + isFixedUpdateFinish);
        if (isFixedUpdateFinish)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
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
        int x = Random.Range(0, 10);
        if(0<=x && x<=5)
        {
            //向上
            if(detective.topFrontHasPlat && detective.topHasPlat)
            {
                //如果前上方和正上方都有平台
                enermyRb.velocity = new Vector2(enermyRb.velocity.x * 0.3f, 0);
                enermyMov.MoveToTop();
            }
            else if(detective.topFrontHasPlat && !detective.topHasPlat)
            {
                //如果前上方有平台但是正上方没有平台
                enermyRb.velocity = new Vector2(enermyRb.velocity.x * 0.5f, 0);
                enermyMov.MoveToTop();
            }
            else if(detective.topHasPlat)
            {
                //如果只有正上方有平台
                enermyRb.velocity = Vector2.zero;
                enermyMov.MoveToTop();
            }
        }
        else
        {
            //向下
            if (detective.botFrontHasPlat && detective.botHasPlat)
            {
                //如果前下方和正下方都有平台
                enermyRb.velocity = new Vector2(enermyRb.velocity.x * 0.3f, 0);
                enermyMov.MoveToBottom();
            }
            else if (detective.botFrontHasPlat && !detective.botHasPlat)
            {
                //如果前下方有平台但是正下方没有平台
                enermyRb.velocity = new Vector2(enermyRb.velocity.x * 0.5f, 0);
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
    /// 寻找玩家
    /// </summary>
    /// <returns></returns>
    private bool searchPlayer()
    {
        Debug.Log("searchingPlayer");
        if (-7.5f < playerObj.Value.transform.position.y && playerObj.Value.transform.position.y < 7.5f)
            return true;
        else
            return false;
    }

    IEnumerator verticalJumpCD()
    {
        canVerticalJump = false;
        yield return new WaitForSeconds(1.5f);
        canVerticalJump = true;
    }
}
