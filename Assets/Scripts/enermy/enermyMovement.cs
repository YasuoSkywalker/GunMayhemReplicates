using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyMovement : MonoBehaviour
{
    [SerializeField]
    [Header("移动速度")]
    private float moveSpeed;
    [SerializeField]
    [Header("跳跃力度")]
    private float jumpForce;
    [SerializeField]
    [Header("最大跳跃速度")]
    private float maxJumpSpeed;

    private detectiveScript detective;
    private Rigidbody2D enermyRb;
    private Animator anim;
    [SerializeField]private bool isHited = false;

    void Start()
    {
        detective = GetComponent<detectiveScript>();
        enermyRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        BetterJump();
        animController();
    }

    /// <summary>
    /// 朝向左边
    /// </summary>
    public void FaceToLeft()
    {
        this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }

    /// <summary>
    /// 朝向右边
    /// </summary>
    public void FaceToRight()
    {
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// 向左移动
    /// </summary>
    public void MoveToLeft()
    {
        FaceToLeft();
        if(!isHited)
        {
            enermyRb.velocity = new Vector2(-1 * moveSpeed * Time.fixedDeltaTime, enermyRb.velocity.y);
        }
    }

    /// <summary>
    /// 向右移动
    /// </summary>
    public void MoveToRight()
    {
        FaceToRight();
        if (!isHited)
        {
            enermyRb.velocity = new Vector2(1 * moveSpeed * Time.fixedDeltaTime, enermyRb.velocity.y);
        }
    }

    /// <summary>
    /// 向上移动
    /// </summary>
    public void MoveToTop()
    {
        StartCoroutine(JumpUp());
    }

    /// <summary>
    /// 向下移动
    /// </summary>
    public void MoveToBottom()
    {
        if(detective.isOnGround)
        {
            detective.currentPlat.GetComponent<OneWayPassController>().downToPlatform(GetComponent<BoxCollider2D>());
        }
    }

    /// <summary>
    /// 跳跃一次
    /// </summary>
    public void JumpOnce()
    {
        enermyRb.velocity = new Vector2(enermyRb.velocity.x, 0);
        enermyRb.AddForce(Vector2.up * jumpForce);
    }

    /// <summary>
    /// 跳跃手感优化
    /// </summary>
    void BetterJump()
    {
        if (enermyRb.velocity.y > maxJumpSpeed)
        {
            enermyRb.velocity = new Vector2(enermyRb.velocity.x, maxJumpSpeed);
        }
        if (enermyRb.velocity.y < 0)
        {
            enermyRb.velocity += Vector2.up * Physics2D.gravity.y * (3.0f) * Time.deltaTime;
        }
    }

    /// <summary>
    /// 跳跃协程函数
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpUp()
    {
        enermyRb.velocity = new Vector2(enermyRb.velocity.x, 0);
        enermyRb.AddForce(Vector2.up * jumpForce);
        yield return new WaitForSeconds(0.5f);
        enermyRb.velocity = new Vector2(enermyRb.velocity.x, 0);
        enermyRb.AddForce(Vector2.up * jumpForce);
    }

    /// <summary>
    /// 设置是否受击
    /// </summary>
    /// <param name="isHitted"></param>
    public void setIsHitted(bool isHitted)
    {
        //Debug.Log("isHitted is true");
        this.isHited = isHitted;
    }

    public bool getIsHitted()
    {
        return this.isHited;
    }

    /// <summary>
    /// Speed Get方法
    /// </summary>
    /// <returns></returns>
    public float getMoveSpeed()
    {
        return this.moveSpeed;
    }

    /// <summary>
    /// 动画机控制
    /// </summary>
    void animController()
    {
        if (detective.isOnGround)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (enermyRb.velocity.x != 0 && !anim.GetBool("isJumping"))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
