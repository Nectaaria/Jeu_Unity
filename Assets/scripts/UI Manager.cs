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
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject changeJobButtonsLayout;
    [SerializeField] private GameObject buildButtonsLayout;

    [SerializeField] private Button playButton;
    [SerializeField] private Button mainMenuQuitButton;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseMenuQuitButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button optionMenuBackButton;

    [SerializeField] private Button changeJobButton;

    [SerializeField] private Button replayButton;

    private float timeScale = 1f;

    void Start()
    {
        resumeButton.onClick.AddListener(() => PauseGame(isGamePaused));
        pauseMenuQuitButton.onClick.AddListener(BackToMainMenu);
        mainMenuQuitButton.onClick.AddListener(BackToMainMenu);
        playButton.onClick.AddListener(PlayGame);
        changeJobButton.onClick.AddListener(ChangeJob);
        optionButton.onClick.AddListener(OptionMenu);
        optionMenuBackButton.onClick.AddListener(OptionMenu);
        replayButton.onClick.AddListener(Replay);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(isGamePaused);
        }
    }

    private void PauseGame(bool isPaused)
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

    private void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
        isGamePaused = true;
        Time.timeScale = 0f;
    }
    private void ChangeJob()
    {
        changeJobButtonsLayout.SetActive(!changeJobButtonsLayout.activeInHierarchy);
        buildButtonsLayout.SetActive(!buildButtonsLayout.activeInHierarchy);
    }
    private void PlayGame()
    {
        Time.timeScale = timeScale;
        isGamePaused = false;
        mainMenuPanel.SetActive(false);
        pauseMenu.enabled = false;
        uiCanvas.enabled = true;
    }
    private void OptionMenu()
    {
        optionPanel.SetActive(!optionPanel.activeInHierarchy);
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }
    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
