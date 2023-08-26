using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayer : Action
{
    public SharedGameObject playerObj;

    public override void OnStart()
    {
        //获取玩家游戏物体
        Debug.Log("Get Player Obj");
        while(!playerObj.Value)
        {
            playerObj.Value = GameObject.Find("player(Clone)");
        }
    }
}
