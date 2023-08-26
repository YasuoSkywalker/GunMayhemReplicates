using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectiveScript : MonoBehaviour
{
    private Rigidbody2D enermyRb;

    [SerializeField]
    [Header("平台所在图层")]
    protected LayerMask ground;

    [SerializeField]
    [Header("左侧悬崖检查点")]
    protected GameObject leftCliffPoint;
    [SerializeField]
    [Header("右侧悬崖检查点")]
    protected GameObject rightCliffPoint;
    public bool isRightCliff = false; //是否在右侧悬崖
    public bool isLeftCliff = false; //是否在左侧悬崖

    [SerializeField]
    [Header("左侧检查点")]
    protected GameObject leftCheckPoint;
    [SerializeField]
    [Header("右侧检查点")]
    protected GameObject rightCheckPoint;
    public bool isOnGround = false;  //是否触地

    [SerializeField]
    [Header("上方平台检测点")]
    protected GameObject topPlatformPoint;
    [SerializeField]
    [Header("下方平台检测点")]
    protected GameObject bottomPlatformPoint;
    public bool topHasPlat = false;  //正上方是否有平台
    public bool topFrontHasPlat = false;  //前上方是否有平台
    public bool botHasPlat = false;  //正下方是否有平台
    public bool botFrontHasPlat = false;  //前下方是否有平台
    public GameObject currentPlat = null;  //当前所处平台
    public GameObject topPlat = null;  //正上方平台
    public GameObject topFrontPlat = null;  //前上方平台
    public GameObject botPlat = null;  //正下方平台
    public GameObject botFrontPlat = null;  //前下方平台

    private void Start()
    {
        enermyRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        checkIsGround();
        checkIsCliff();
        checkPlatform();
    }

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// 检测悬崖
    /// </summary>
    void checkIsCliff()
    {
        RaycastHit2D left = Physics2D.Raycast(leftCliffPoint.transform.position, Vector2.down, 0.1f, ground);
        RaycastHit2D right = Physics2D.Raycast(rightCliffPoint.transform.position, Vector2.down, 0.1f, ground);

        if(isOnGround && !left)
            isLeftCliff = true;
        else
            isLeftCliff = false;

        if(isOnGround && !right)
            isRightCliff = true;
        else
            isRightCliff = false;

        //打印结果
        //Debug.Log("是否在左侧悬崖:" + isLeftCliff + " " + "是否在右侧悬崖:" + isRightCliff);
    }

    /// <summary>
    /// 检查是否触地面
    /// </summary>
    void checkIsGround()
    {
        RaycastHit2D left = Physics2D.Raycast(leftCheckPoint.transform.position, Vector2.down * 0.03f, 0.03f, ground);
        RaycastHit2D right = Physics2D.Raycast(rightCheckPoint.transform.position, Vector2.down * 0.03f, 0.03f, ground);
        bool rayCheckResult = left || right;

        //触地检测
        if (enermyRb.IsTouchingLayers(ground) || rayCheckResult)
        {
            /*if(left || right)
                currentPlat = left.collider.gameObject ?? right.collider.gameObject;*/
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        /*//落地恢复跳跃次数
        if (enermyRb.velocity.y < 0 && (enermyRb.IsTouchingLayers(ground) || rayCheckResult))
        {
            jumpTime = jumpCount;
        }*/
    }

    /// <summary>
    /// 检测周围平台
    /// </summary>
    void checkPlatform()
    {
        RaycastHit2D top = Physics2D.Raycast(topPlatformPoint.transform.position, Vector3.up * 2f, 2f, ground);
        RaycastHit2D bot = Physics2D.Raycast(bottomPlatformPoint.transform.position, Vector3.down * 2.5f, 2.5f, ground);

        RaycastHit2D topFront = Physics2D.Raycast(topPlatformPoint.transform.position, new Vector3(1.2f * transform.lossyScale.x, 0.7f, 0).normalized * 3f, 3f, ground);
        RaycastHit2D botFront = Physics2D.Raycast(bottomPlatformPoint.transform.position, new Vector3(0.9f * transform.lossyScale.x, -0.7f, 0).normalized * 3f, 3f, ground);

        if(top)
        {
            topPlat = top.collider.gameObject;
            topHasPlat = true;
        }
        else
            topHasPlat = false;

        if (bot)
        {
            botPlat = bot.collider.gameObject;
            botHasPlat = true;
        }
        else
            botHasPlat = false;

        if (topFront)
        {
            topFrontPlat = topFront.collider.gameObject;
            topFrontHasPlat = true;
        }
        else
            topFrontHasPlat = false;

        if (botFront)
        {
            botFrontPlat = botFront.collider.gameObject;
            botFrontHasPlat = true;
        }
        else
            botFrontHasPlat = false;
    }

    /// <summary>
    /// 碰撞体进入函数
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //检测是否触地
        if (collision.gameObject.layer == 3)
        {
            currentPlat = collision.gameObject;
        }
    }

    /// <summary>
    /// 绘制辅助线条
    /// </summary>
    private void OnDrawGizmos()
    {
        Debug.DrawRay(leftCheckPoint.transform.position, Vector3.down * 0.03f, Color.red);
        Debug.DrawRay(rightCheckPoint.transform.position, Vector3.down * 0.03f, Color.red);

        Debug.DrawRay(leftCliffPoint.transform.position, Vector3.down * 0.03f, Color.red);
        Debug.DrawRay(rightCliffPoint.transform.position, Vector3.down * 0.03f, Color.red);


        Debug.DrawRay(topPlatformPoint.transform.position, Vector3.up * 2f, Color.blue);
        Debug.DrawRay(bottomPlatformPoint.transform.position, Vector3.down * 2.5f, Color.green); //正上/下方平台
        Debug.DrawRay(topPlatformPoint.transform.position, new Vector3(1.2f*transform.lossyScale.x,0.7f,0).normalized * 3f, Color.blue);
        Debug.DrawRay(bottomPlatformPoint.transform.position, new Vector3(0.9f * transform.lossyScale.x, -0.7f, 0).normalized * 3f, Color.green); //前上/下方平台
    }
}
