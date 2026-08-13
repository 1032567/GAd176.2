using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Shop : MenuBase
{
    [Header("DRAG IN: CurrencyManager from the Hierarchy")]
    [Tooltip("The object that has the CurrencyManager script on it")]
    public CurrencyManager currency;

    [Header("DRAG IN: the panel that shows and hides the shop")]
    public GameObject shopPanel;

    [Header("Upgrade Costs (rise after each purchase)")]
    [Tooltip("Cost of the next gold-per-click upgrade")]
    public int clickUpgradeCost = 10;
    [Tooltip("Cost of the next gold-per-tick upgrade")]
    public int tickUpgradeCost = 10;
    [Tooltip("Cost of the next tick-speed upgrade")]
    public int speedUpgradeCost = 10;
    [Tooltip("Cost multiplier applied after each purchase")]
    public float costGrowthRate = 1.5f;

    [Header("Upgrade Amounts")]
    [Tooltip("How much goldPerClick rises per purchase")]
    public int clickUpgradeAmount = 1;
    [Tooltip("How much goldPerTick rises per purchase")]
    public int tickUpgradeAmount = 1;
    [Tooltip("How much tickInterval shrinks per purchase, in seconds")]
    public float speedUpgradeAmount = 0.1f;

    List<TMP_Text> buttonLabels = new List<TMP_Text>(); // cached so costs can refresh after a purchase

    // builds the shop buttons, then keeps their cost labels up to date
    protected override void Start()
    {
        base.Start(); // builds the buttons using GetButtonData()
        CacheButtonLabels();
        RefreshLabels();

        if (currency != null)
            currency.onGoldChanged.AddListener(RefreshLabels); // costs shown never go stale
    }

    // defines the shop's buttons - required by MenuBase
    protected override List<MenuButtonData> GetButtonData()
    {
        return new List<MenuButtonData>
        {
            new MenuButtonData { label = "Gold/Click", onClick = BuyClickUpgrade },
            new MenuButtonData { label = "Gold/Tick",  onClick = BuyTickUpgrade  },
            new MenuButtonData { label = "Tick Speed", onClick = BuySpeedUpgrade },
            new MenuButtonData { label = "Back",       onClick = CloseShop      }
        };
    }

    // shows the shop
    public void OpenShop()
    {
        if (shopPanel != null) shopPanel.SetActive(true);
    }

    // hides the shop
    public void CloseShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false);
    }

    // spends gold to raise gold-per-click
    public void BuyClickUpgrade()
    {
        if (currency == null || currency.GetGold() < clickUpgradeCost)
        {
            Debug.Log("Not enough gold for click upgrade"); // for testing and debugging
            return;
        }

        currency.SpendGold(clickUpgradeCost);
        currency.UpgradeGoldPerClick(clickUpgradeAmount);
        clickUpgradeCost = Mathf.RoundToInt(clickUpgradeCost * costGrowthRate); // next purchase costs more
        RefreshLabels();
    }

    // spends gold to raise gold-per-tick
    public void BuyTickUpgrade()
    {
        if (currency == null || currency.GetGold() < tickUpgradeCost)
        {
            Debug.Log("Not enough gold for tick upgrade"); // for testing and debugging
            return;
        }

        currency.SpendGold(tickUpgradeCost);
        currency.UpgradeGoldPerTick(tickUpgradeAmount);
        tickUpgradeCost = Mathf.RoundToInt(tickUpgradeCost * costGrowthRate); // next purchase costs more
        RefreshLabels();
    }

    // spends gold to make ticks happen more often
    public void BuySpeedUpgrade()
    {
        if (currency == null || currency.GetGold() < speedUpgradeCost)
        {
            Debug.Log("Not enough gold for speed upgrade"); // for testing and debugging
            return;
        }

        currency.SpendGold(speedUpgradeCost);
        currency.UpgradeTickSpeed(speedUpgradeAmount);
        speedUpgradeCost = Mathf.RoundToInt(speedUpgradeCost * costGrowthRate); // next purchase costs more
        RefreshLabels();
    }

    // grabs each button's text so costs can be updated later
    void CacheButtonLabels()
    {
        buttonLabels.Clear();
        foreach (Transform child in buttonContainer)
            buttonLabels.Add(child.GetComponentInChildren<TMP_Text>());
    }

    // writes the current cost into each upgrade button
    void RefreshLabels()
    {
        if (buttonLabels.Count < 3) return;
        buttonLabels[0].text = "Click +" + clickUpgradeAmount + " (" + clickUpgradeCost + "g)"; // shorter text so it fits the button
        buttonLabels[1].text = "Tick +"  + tickUpgradeAmount  + " (" + tickUpgradeCost  + "g)";
        buttonLabels[2].text = "Speed ("  + speedUpgradeCost  + "g)";
    }
}
