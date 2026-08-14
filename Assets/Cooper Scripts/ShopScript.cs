using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopScript : MenuBase
{
    public GameObject shopPanel;

    protected override List<MenuButtonData> GetButtonData()
    {
        return new List<MenuButtonData>
        {
            new MenuButtonData { label = "$5 Upgrade", onClick = Upgrade5 },
            new MenuButtonData { label = "$10 Upgrade", onClick = Upgrade10 },
            new MenuButtonData { label = "$50 Upgrade", onClick = Upgrade50 },
            new MenuButtonData { label = "$100 Upgrade", onClick = Upgrade100 },
            new MenuButtonData { label = "Back", onClick = HideShop }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShowShop();
        }
    }

    public void Upgrade5()
    {

    }

    public void Upgrade10()
    {

    }

    public void Upgrade50()
    {

    }
    public void Upgrade100()
    {

    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
    }
}
