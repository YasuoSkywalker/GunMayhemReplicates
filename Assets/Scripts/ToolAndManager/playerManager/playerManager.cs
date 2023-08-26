using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public int hatConfig = 0;
    public int faceConfig = 0;
    public int colorConfig = 0;
    public int clothConfig = 0;
    public string baseWeapon = "m1911";
    public string team = "A";
    public int skillNum;

    private GameObject playerModule;

    [SerializeField]
    [Header("颜色块图片数组")]
    private Sprite[] colorSprite;

    [SerializeField]
    [Header("武器图片数组")]
    private Sprite[] weaponSprite;

    [SerializeField]
    [Header("技能图片数组")]
    private Sprite[] skillSprite;

    private void Awake()
    {
        //playerModule = GameObject.Find("player");
        //setPlayerStyle();
    }

    /// <summary>
    /// 设置玩家角色的样式
    /// </summary>
    public void setPlayerStyle()
    {
        if(!playerModule)
        {
            playerModule = GameObject.Find("player");
        }
        if (SceneManager.GetActiveScene().name == "charactorConfig")
        {
            playerModule.transform.Find("hat").GetComponent<SpriteResolver>().SetCategoryAndLabel("hat", hatConfig.ToString());
            playerModule.transform.Find("face").GetComponent<SpriteResolver>().SetCategoryAndLabel("face", faceConfig.ToString());
            playerModule.transform.Find("clothes").GetComponent<SpriteResolver>().SetCategoryAndLabel("clothes", clothConfig.ToString());
            playerModule.transform.Find("body").GetComponent<SpriteResolver>().SetCategoryAndLabel("body", colorConfig.ToString());
            playerModule.transform.Find("head").GetComponent<SpriteResolver>().SetCategoryAndLabel("head", colorConfig.ToString());
            playerModule.transform.Find("leftLag").GetComponent<SpriteResolver>().SetCategoryAndLabel("leftLag", colorConfig.ToString());
            playerModule.transform.Find("rightLag").GetComponent<SpriteResolver>().SetCategoryAndLabel("rightLag", colorConfig.ToString());
            setBaseWeapon(baseWeapon);
        }
    }

    public void setHatConfig(int config)
    {
        this.hatConfig = config;
        setPlayerStyle();
    }

    public void setFaceConfig(int config)
    {
        this.faceConfig = config;
        setPlayerStyle();
    }

    public void setColorConfig(int config)
    {
        this.colorConfig = config;
        GameObject.Find("colorConfig").transform.Find("color").GetComponent<Image>().sprite = colorSprite[colorConfig];
        setPlayerStyle();
    }

    public void setSkillNum(int num)
    {
        this.skillNum = num;
        GameObject.Find("skillConfig").transform.Find("skill").GetComponent<Image>().sprite = skillSprite[skillNum];
        setPlayerStyle();
    }

    public void setBaseWeapon(string weapon)
    {
        this.baseWeapon = weapon;
        int num = 0;
        switch (weapon)
        {
            case "1911":
                num = 0;
                break;
            case "bull":
                num = 1;
                break;
            case "glock":
                num = 2;
                break;
            case "gold":
                num = 3;
                break;
            default:
                num = 0;
                break;
        }
        GameObject.Find("weaponConfig").transform.Find("weapon").GetComponent<Image>().sprite = weaponSprite[num];
        //setPlayerStyle();
    }

    public void setClothConfig(int config)
    {
        this.clothConfig = config;
        setPlayerStyle();
    }
}
