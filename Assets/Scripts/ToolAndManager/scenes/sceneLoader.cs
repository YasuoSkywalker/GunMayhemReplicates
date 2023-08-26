using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneLoader : MonoBehaviour
{
    public static sceneLoader instance;
    private GameObject currentScene = null;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //DontDestroyOnLoad(this);   
    }

    void Update()
    {
        
    }

    public void loadScene(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void activePannel(string name)
    {
        if(currentScene != null)
        {
            CanvasGroup current = currentScene.GetComponent<CanvasGroup>();
            current.alpha = 0;
            current.interactable = false;
            current.blocksRaycasts = false;
        }
        currentScene = GameObject.Find(name);
        CanvasGroup temp =  GameObject.Find(name).GetComponent<CanvasGroup>();
        temp.alpha = 1;
        temp.interactable = true;
        temp.blocksRaycasts = true;
    }

    public void inactivePannel(string name)
    {
        CanvasGroup temp = GameObject.Find(name).GetComponent<CanvasGroup>();
        temp.alpha = 0;
        temp.interactable = false;
        temp.blocksRaycasts = false;
        currentScene = null;
    }

    public sceneLoader getInstance()
    {
        return instance;
    }

    public void loadLevel()
    {
        int currentLevel = GameObject.Find("levelManager").GetComponent<levelManager>().getCurrentLevel();
        switch (currentLevel)
        {
            case 0:
                SceneManager.LoadScene("snowMountain");
                break;
            case 1:
                SceneManager.LoadScene("plain");
                break;
            default:
                SceneManager.LoadScene("snowMountain");
                break;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
