using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class OptionsMenu : MenuBase
{
    public static OptionsMenu Instance; // NEW - the single shared reference

    public GameObject optionsPanel;

    private bool isHardMode = false;
    private bool isAudioOn = true;

    public string creditsSceneName = "Credits";

    void Awake() // NEW
    {
        // If a copy already exists (e.g. we came back to MainMenu), destroy this new one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // survive scene changes
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

    public void ShowOptions() // NEW - renamed for clarity, called from other menus
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