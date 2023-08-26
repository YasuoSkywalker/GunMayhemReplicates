using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectLevel : MonoBehaviour
{
    private levelManager levelMan;


    void Start()
    {
        levelMan = GameObject.Find("levelManager").GetComponent<levelManager>();
        onSelectLevel(levelMan.getCurrentLevel());
        GameObject.Find("level"+(levelMan.getCurrentLevel()+1).ToString()).GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSelectLevel(int level)
    {
        if (level >= 0 && level < levelMan.levelInfo.Length)
        {
            GameObject.Find("mapInfo").GetComponent<Image>().sprite = levelMan.levelInfo[level];
            GameObject.Find("mapInfo").GetComponent<CanvasGroup>().alpha = 1;
            levelMan.onSelectLevel(level);
        }
        else
        {
            Debug.LogWarning("level count error!");
        }
    }
}
