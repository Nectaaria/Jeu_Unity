using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool isGamePaused = false;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Canvas pauseMenu;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;

    private float timeScale;
    void Start()
    {
        resumeButton.onClick.AddListener(() =>PauseGame(isGamePaused));
        optionsButton.onClick.AddListener(OptionMenu);
        backButton.onClick.AddListener(OptionMenu);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(isGamePaused);
        }
    }

    public void PauseGame(bool isPaused)
    {
        if (!isGamePaused)
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0f;
            isGamePaused = true;
            pauseMenu.enabled = true;
            uiCanvas.enabled = false;
            pausePanel.SetActive(true);
            optionPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = timeScale;
            pauseMenu.enabled = false;
            uiCanvas.enabled = true;
            isGamePaused = false;
        }
    }
    public void OptionMenu()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        optionPanel.SetActive(!optionPanel.activeInHierarchy);
    }
    public void Quit()
    {
        
    }
}
