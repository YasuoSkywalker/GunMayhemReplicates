using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getParentFunc : MonoBehaviour
{
    //调用父物体中weapon脚本的exitChanging方法
    public void getExitChanging()
    {
        transform.GetComponentInParent<weapon>().exitChanging();
    }


    public void getCreateEffect()
    {
        transform.GetComponentInParent<weapon>().createEffect();
    }
}
