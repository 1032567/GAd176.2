using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MenuBase
{
    public GameObject pausePanel;
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    protected override List<MenuButtonData> GetButtonData()
    {
        return new List<MenuButtonData>
        {
            new MenuButtonData { label = "Resume", onClick = ResumeGame },
            new MenuButtonData { label = "Main Menu", onClick = ReturnToMainMenu },
            new MenuButtonData { label = "Quit", onClick = QuitGame }
        };
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
