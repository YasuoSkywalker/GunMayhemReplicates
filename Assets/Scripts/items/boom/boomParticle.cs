using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomParticle : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        transform.parent.GetComponent<SpriteRenderer>().enabled = true;
        ObjectPoolManager.ReturnObjectToPool(transform.parent.gameObject);
    }
}
