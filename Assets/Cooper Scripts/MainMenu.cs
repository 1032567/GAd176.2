using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MenuBase
{
    public string gameplaySceneName = "GameScene";

    protected override List<MenuButtonData> GetButtonData()
    {
        return new List<MenuButtonData>
        {
            new MenuButtonData { label = "Play", onClick = PlayGame },
            new MenuButtonData { label = "Options", onClick = OpenOptions },
            new MenuButtonData { label = "Exit", onClick = ExitGame }
        };
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenOptions()
    {
        Debug.Log("OpenOptions called, Instance is: " + OptionsMenu.Instance);
        OptionsMenu.Instance.ShowOptions();
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

