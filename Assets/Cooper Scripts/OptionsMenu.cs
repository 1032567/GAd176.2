using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class OptionsMenu : MenuBase
{
    public static OptionsMenu Instance;

    public GameObject optionsPanel;

    private bool isHardMode = false;
    private bool isAudioOn = true;

    public string creditsSceneName = "Credits";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override List<MenuButtonData> GetButtonData()
    {
        return new List<MenuButtonData>
        {
            new MenuButtonData { label = "Hard Mode", onClick = ToggleHardMode },
            new MenuButtonData { label = "Audio", onClick = ToggleAudio },
            new MenuButtonData { label = "Credits", onClick = OpenCredits },
            new MenuButtonData { label = "Back", onClick = CloseOptions }
        };
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ToggleHardMode()
    {
        isHardMode = !isHardMode;
        Debug.Log("Hard Mode is now: " + (isHardMode ? "ON" : "OFF"));
    }

    public void ToggleAudio()
    {
        isAudioOn = !isAudioOn;
        Debug.Log("Audio is now: " + (isAudioOn ? "ON" : "OFF"));
    }

    public void OpenCredits()
    {
        CloseOptions();
        SceneManager.LoadScene(creditsSceneName);
    }
}