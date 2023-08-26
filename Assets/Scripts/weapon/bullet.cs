using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private bool isSetted = false;

    private Vector3 dir;
    private float speed;
    private float repel;
    private string team;

    protected virtual void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    //离开屏幕范围时销毁
    protected void OnBecameInvisible()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    public void setProp(Vector3 newDir,float newSpeed,float newRepel,string newTeam)
    {
        this.dir = newDir;
        this.speed = newSpeed;
        this.repel = newRepel;
        this.team = newTeam;
        if(newDir == Vector3.right)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,0);
        }
        isSetted = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //击中敌对玩家
        if (collision.tag == "Player" && collision.GetComponent<PlayerInfo>().teams != team)
        {
            Debug.Log("击中玩家");
            collision.GetComponent<PlayerBehave>().onHitted();
            collision.GetComponent<Rigidbody2D>().AddForce(dir * repel);
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
        //击中AI敌人
        else if (collision.tag == "enermy" && collision.GetComponent<PlayerInfo>().teams != team)
        {
            Debug.Log("击中敌人");
            collision.GetComponent<enermyBehavior>().onHitted();
            collision.GetComponent<Rigidbody2D>().AddForce(dir * repel);
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }
}
