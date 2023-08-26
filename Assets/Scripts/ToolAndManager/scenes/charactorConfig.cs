using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class charactorConfig : MonoBehaviour
{
    private playerManager playerMan;
    //private GameObject playerModule;

    private void Awake()
    {
        playerMan = GameObject.Find("playerManager").GetComponent<playerManager>();
        //playerModule = GameObject.Find("player");
    }

    // Start is called before the first frame update
    void Start()
    {
        setPlayerStyle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 设置玩家角色的样式
    /// </summary>
    void setPlayerStyle()
    {
        playerMan.setPlayerStyle();
    }

    public void setHatConfig(int config)
    {
        playerMan.setHatConfig(config);
    }

    public void setFaceConfig(int config)
    {
        playerMan.setFaceConfig(config);
    }

    public void setColorConfig(int config)
    {
        playerMan.setColorConfig(config);
    }

    public void setSkillNum(int num)
    {
        playerMan.setSkillNum(num);
    }

    public void setBaseWeapon(string weapon)
    {
        playerMan.setBaseWeapon(weapon);
    }

    public void setClothConfig(int config)
    {
        playerMan.setClothConfig(config);
    }
}
