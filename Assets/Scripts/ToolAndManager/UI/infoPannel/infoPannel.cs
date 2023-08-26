using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class infoPannel : MonoBehaviour
{
    [SerializeField]
    [Header("跟踪物体名称")]
    private string lookingName;

    [SerializeField]
    [Header("文本显示区域")]
    private TextMeshProUGUI text;

    private GameObject lookingObj;

    private int lifeCount;
    private int bulletCount;

    private PlayerInfo playerInfo;


    // Start is called before the first frame update
    void Start()
    {
        while(!lookingObj)
        {
            Debug.Log("looking:" + lookingName);
            if (lookingName.Equals("player"))
            {
                lookingObj = GameObject.FindGameObjectWithTag("Player");
            }
            else
                lookingObj = GameObject.Find(lookingName);
        }
        playerInfo = lookingObj.GetComponent<PlayerInfo>();
        lifeCount = playerInfo.lifeCount;
        //bulletCount = lookingObj.transform.Find("weapon").GetComponent<weapon>().getWeaponNowBullet();
    }

    // Update is called once per frame
    void Update()
    {
        RenderToPannel();
    }

    /// <summary>
    /// 将信息显示到面板上
    /// </summary>
    void RenderToPannel()
    {
        if(playerInfo.lifeCount <= 0)
        {
            Destroy(gameObject);
            return;
        }
        lifeCount = playerInfo.lifeCount;
        bulletCount = lookingObj.transform.Find("weapon").GetComponent<weapon>().getWeaponNowBullet();
        //显示到面板上
        text.text = lookingName + '\n' + "生命值: " + lifeCount + '\n' + "子弹: " + bulletCount;
    }
}
