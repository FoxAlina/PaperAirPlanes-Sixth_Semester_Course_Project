using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void pauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void openSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void closeSettings()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
