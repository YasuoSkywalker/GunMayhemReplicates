using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyManager : MonoBehaviour
{
    private int enermyCount;
    [SerializeField]
    [Header(" §¿˚Ω·À„∂Øª≠")]
    private GameObject successAnim;

    private void Start()
    {
        enermyCount = GameObject.FindGameObjectsWithTag("enermy").Length;
    }

    public void decreaseEnermyCount(int decreaseNum = 1)
    {
        enermyCount -= decreaseNum;
        if(enermyCount<=0)
        {
            Time.timeScale = 0;
            successAnim.SetActive(true);
            //Debug.Log("game win");
        }
    }
}
