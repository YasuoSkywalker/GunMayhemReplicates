using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testExit : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;

    private bool isPanelVisiabel = false;

    private void Start()
    {
        isPanelVisiabel = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPanelVisiabel)
            {
                VisiblePannel(true);
            }
            else
            {
                VisiblePannel(false);
            }
        }
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    public void VisiblePannel(bool isVisible)
    {
        if(isVisible)
        {
            SetTimeScale(0);
            settingPanel.GetComponent<CanvasGroup>().alpha = 1;
            settingPanel.GetComponent<CanvasGroup>().interactable = true;
            settingPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            isPanelVisiabel = true;
        }
        else
        {
            SetTimeScale(1);
            settingPanel.GetComponent<CanvasGroup>().alpha = 0;
            settingPanel.GetComponent<CanvasGroup>().interactable = false;
            settingPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            isPanelVisiabel = false;
        }
    }
}
