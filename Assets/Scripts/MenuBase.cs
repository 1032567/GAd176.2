using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public abstract class MenuBase : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    protected List<MenuButtonData> menuButtons;

    protected virtual void Start()
    {
        menuButtons = GetButtonData();
        BuildMenu();
    }

    protected abstract List<MenuButtonData> GetButtonData();

    protected void BuildMenu()
    {
        foreach (MenuButtonData data in menuButtons)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = data.label;
            newButton.GetComponent<Button>().onClick.AddListener(data.onClick);
        }
    }
}
