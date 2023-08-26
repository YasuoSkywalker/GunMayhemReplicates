using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    [Header("爆炸时间")]
    [SerializeField] private float explodeTime;

    [Header("推力")]
    [SerializeField] private float explodeForce;

    [Header("爆炸范围")]
    [SerializeField] private float explodeRange;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine("startExplode");
    }

    IEnumerator startExplode()
    {
        yield return new WaitForSeconds(explodeTime);
        Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, explodeRange);
        foreach (var item in temp)
        {
            if(item.tag == "Player" || item.tag == "enermy")
            {
                Transform tempTransform = item.transform;
                Vector2 tempX = new Vector3(tempTransform.position.x - transform.position.x, 0).normalized;
                Vector2 tempY = new Vector3(0, tempTransform.position.y - transform.position.y).normalized*0.3f;
                Vector2 dir = tempX + tempY;
                item.GetComponent<Rigidbody2D>().AddForce(dir*explodeForce);
            }
        }
        transform.Find("explosion").GetComponent<ParticleSystem>().Play();
        GetComponent<SoundManager>().playAudio("boom");
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRange);
    }
}
