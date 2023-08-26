using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotBullet : bullet
{
    protected override void Update()
    {
        
    }

    public void destroyFunc()
    {
        GameObject.Destroy(this.gameObject);
    }
}
