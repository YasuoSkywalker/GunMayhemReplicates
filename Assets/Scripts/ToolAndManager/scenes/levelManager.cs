using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{   
    [Header("关卡简介")]
    public Sprite[] levelInfo;

    private int currentLevel = 0;

    public void onSelectLevel(int level)
    {
        currentLevel = level;
        Debug.Log("currentLevel:"+currentLevel);
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }
}
