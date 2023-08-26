using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSetting : MonoBehaviour
{
    //public float volumeValue = 50f;
    [SerializeField][Header("静音图标")]
    private Sprite nonVolumeSprite;
    [SerializeField][Header("非静音图标")]
    private Sprite volumeSprite;

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    public void setVolume(float value)
    {
        AudioListener.volume = value;
        if(value == 0)
        {
            GameObject.Find("volumeIcon").GetComponent<Image>().sprite = nonVolumeSprite;
        }
        else
        {
            GameObject.Find("volumeIcon").GetComponent<Image>().sprite = volumeSprite;
        }
    }
}
