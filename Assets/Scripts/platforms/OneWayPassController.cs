using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWayPassController : MonoBehaviour
{
    [SerializeField][Header("是否可以下降")]
    private bool canDown = true;
    
    [SerializeField] 
    private float delay = 0.5f;

    private BoxCollider2D platformCollider;


    void Start()
    {
        platformCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 关闭碰撞检测
    /// </summary>
    public void downToPlatform(BoxCollider2D collider)
    {
        //Debug.Log("ignore");
        if(canDown)
            StartCoroutine("closeCollider", collider);
    }

    IEnumerator closeCollider(BoxCollider2D collider)
    {
        Physics2D.IgnoreCollision(platformCollider, collider, true);
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(platformCollider, collider, false);
    }

}
