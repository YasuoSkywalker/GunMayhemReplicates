using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed;
    [Header("跳跃力度")]
    public float jumpForce;
    [Header("跳跃次数")]
    public int jumpCount = 2;
    [Header("最大跳跃速度")]
    public float maxJumpSpeed;

    private Rigidbody2D playerRb;
    private BoxCollider2D playerCollider;
    private int jumpTime;  //当前可用的跳跃次数
    private bool isOnGround = false;  //是否触地
    private GameObject currentGround; //当前接触的地面

    [SerializeField][Header("左侧检查点")]
    private GameObject leftCheckPoint;
    [SerializeField][Header("右侧检查点")]
    private GameObject rightCheckPoint;
    [SerializeField][Header("动画组件")]
    private Animator anim;

    [SerializeField]private bool isHited = false;
    [SerializeField] private LayerMask ground;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //技能一，三段跳
        if(GetComponent<PlayerInfo>().getSkillNum() == 1)
        {
            jumpCount = 3;
        }
        jumpTime = jumpCount;
        //Physics2D.IgnoreLayerCollision(6, 6); //忽略玩家间的碰撞
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void Update()
    {
        PlayerJump();
        BetterFalling();
        CheckIsGround();
        animController();
        DownPlatform();
    }

    ///<summary>
    ///角色左右移动
    ///</summary>
    void PlayerMove()
    {
        if (!isHited)
        {
            //获取角色的移动方向 -1，0，1
            float movement = Input.GetAxisRaw("Horizontal");
            //对角色进行移动
            playerRb.velocity = new Vector2(movement * moveSpeed * Time.fixedDeltaTime, playerRb.velocity.y);
            //对角色进行转向
            if (movement != 0)
            {
                this.gameObject.transform.localScale = new Vector3(movement, 1, 1);
            }
        }
    }

    ///<summary>
    ///角色跳跃
    ///</summary>
    void PlayerJump()
    {
        if(jumpTime>0)
        {
            if(Input.GetButtonDown("Jump"))
            {
                jumpTime--;
                //清除y轴方向的速度，保证每次跳跃高度相同
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(Vector2.up*jumpForce);
                anim.SetBool("isJumping", true);//跳跃动画
                isOnGround = false;
            }
        }
        if(playerRb.velocity.y>maxJumpSpeed)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, maxJumpSpeed);
        }
    }

    /// <summary>
    /// 跳跃手感优化
    /// </summary>
    void BetterFalling()
    {
        if(playerRb.velocity.y<0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (3.0f)*Time.deltaTime;
        }
    }

    /// <summary>
    /// 检查是否触地面
    /// </summary>
    void CheckIsGround()
    {
        RaycastHit2D left = Physics2D.Raycast(leftCheckPoint.transform.position, Vector2.down*0.03f, 0.03f,ground);
        RaycastHit2D right = Physics2D.Raycast(rightCheckPoint.transform.position, Vector2.down*0.03f, 0.03f,ground);
        bool rayCheckResult = left||right;

        //触地检测
        if (playerRb.IsTouchingLayers(ground) || rayCheckResult) 
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        //落地恢复跳跃次数
        if (playerRb.velocity.y<0 && (playerRb.IsTouchingLayers(ground)||rayCheckResult)) 
        {
            jumpTime = jumpCount;
        }
    }

    /// <summary>
    /// 下降
    /// </summary>
    void DownPlatform()
    {
        if(isOnGround && currentGround!=null)
        {
            if (Input.GetKey(KeyCode.S))
            {
                currentGround.GetComponent<OneWayPassController>().downToPlatform(playerCollider);       
            }
        }
    }

    /// <summary>
    /// 碰撞体进入函数
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //检测是否触地
        if(collision.gameObject.layer == 3)
        {
            currentGround = collision.gameObject;
        }
    }

    /// <summary>
    /// 动画机控制
    /// </summary>
    void animController()
    {
        if(isOnGround)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if(playerRb.velocity.x!=0 && !anim.GetBool("isJumping"))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// 设置是否受击
    /// </summary>
    /// <param name="isHitted"></param>
    public void setIsHitted(bool isHitted)
    {
        this.isHited = isHitted;
    }


    /// <summary>
    /// 获取玩家当前所处的平台
    /// </summary>
    /// <returns></returns>
    public GameObject getCurrentGround()
    {
        return this.currentGround;
    }
}
