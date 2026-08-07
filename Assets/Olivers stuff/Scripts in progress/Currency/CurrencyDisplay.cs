using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    [Header("DRAG IN: CurrencyManager from the Hierarchy")]
    [Tooltip("The object that has the CurrencyManager script on it")]
    public CurrencyManager manager;

    [Header("DRAG IN: TMP Text objects from your Canvas")]
    [Tooltip("Displays the current gold total")]
    public TMP_Text goldText;
    [Tooltip("Displays gold generated per second")]
    public TMP_Text rateText;

    // subscribes to the gold event and shows the starting values
    void Start()
    {
        if (manager == null) return;
        manager.onGoldChanged.AddListener(OnGoldChanged); // subscribe to the event
        Refresh();
    }

    // keeps the text up to date every frame
    void Update()
    {
        if (manager != null)
            Refresh();
    }

    // runs whenever the gold total changes
    void OnGoldChanged()
    {
        Debug.Log("UI received gold-changed event"); // for testing and debugging
    }

    // writes the latest values into the text fields
    void Refresh()
    {
        if (goldText != null)
            goldText.text = "Gold: " + manager.GetGold();

        if (rateText != null)
            rateText.text = manager.GetGoldPerSecond().ToString("F1") + " / sec"; // rounds to 1 decimal place
    }
}
