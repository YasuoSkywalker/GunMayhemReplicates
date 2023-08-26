using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class initPlayer : MonoBehaviour
{
    private int playerLifeCount = 10;

    [SerializeField]
    [Header("游戏角色预制体")]
    private GameObject playerGameObject;

    [SerializeField]
    [Header("鸭子炸弹预制体")]
    private GameObject duckyBoom;

    [SerializeField]
    [Header("角色出生点")]
    Vector3 tempPosition = new Vector3(0, 20, 0);

    [SerializeField]
    [Header("任务失败结算动画")]
    private GameObject failAnim;

    Quaternion tempQuaternion = new Quaternion(0, 0, 0, 0);

    private void Awake()
    {
        init();
    }

    /// <summary>
    /// 初始化角色
    /// </summary>
    public void init()
    {
        playerManager tempPlayerManager = GameObject.Find("playerManager").GetComponent<playerManager>();
        PlayerInfo tempPlayerInfo = playerGameObject.GetComponent<PlayerInfo>();
        //初始化角色信息
        tempPlayerInfo.setSkillNum(tempPlayerManager.skillNum);
        tempPlayerInfo.setFirstWeapon(tempPlayerManager.baseWeapon);
        //设置帽子
        playerGameObject.transform.Find("hat").GetComponent<SpriteResolver>().SetCategoryAndLabel("hat", tempPlayerManager.hatConfig.ToString());
        //设置表情
        playerGameObject.transform.Find("face").GetComponent<SpriteResolver>().SetCategoryAndLabel("face", tempPlayerManager.faceConfig.ToString());
        //设置衣服
        playerGameObject.transform.Find("clothes").GetComponent<SpriteResolver>().SetCategoryAndLabel("clothes", tempPlayerManager.clothConfig.ToString());
        //设置颜色
        playerGameObject.transform.Find("head").GetComponent<SpriteResolver>().SetCategoryAndLabel("head", tempPlayerManager.colorConfig.ToString());
        playerGameObject.transform.Find("body").GetComponent<SpriteResolver>().SetCategoryAndLabel("body", tempPlayerManager.colorConfig.ToString());
        playerGameObject.transform.Find("leftLag").GetComponent<SpriteResolver>().SetCategoryAndLabel("leftLag", tempPlayerManager.colorConfig.ToString());
        playerGameObject.transform.Find("rightLag").GetComponent<SpriteResolver>().SetCategoryAndLabel("rightLag", tempPlayerManager.colorConfig.ToString());
        //鸭子炸弹
        if(tempPlayerInfo.skillNum == 3)
        {
            playerGameObject.GetComponent<PlayerBehave>().boom = duckyBoom;
        }
        playerLifeCount = playerGameObject.GetComponent<PlayerInfo>().lifeCount;
        GameObject temp = Instantiate(playerGameObject, tempPosition, tempQuaternion);
    }

    /// <summary>
    /// lifeCount Set方法
    /// </summary>
    /// <param name="lifeCount"></param>
    public void setPlayerLifeCount(int lifeCount)
    {
        playerLifeCount = lifeCount;
    }

    /// <summary>
    /// lifeCount Get方法
    /// </summary>
    /// <returns></returns>
    public int getPlayerLifeCount()
    {
        return playerLifeCount;
    }

    /// <summary>
    /// 重生玩家
    /// </summary>
    /// <param name="player"></param>
    public void regeneratePlayer(GameObject player)
    {
        player.GetComponent<SoundManager>().playAudio("died");
        if(playerLifeCount>1)
        {
            playerLifeCount--;
            player.GetComponent<PlayerBehave>().setBaseWeapon();
            player.transform.position = tempPosition;
            player.GetComponent<PlayerBehave>().setIsDied(false);
        }
        else
        {
            gameLose();
        }
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    public void gameLose()
    {
        Time.timeScale = 0;
        failAnim.SetActive(true);
        //Debug.Log("gameLose");
    }
}
