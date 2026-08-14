using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Thresholds (base values)")]
    [Tooltip("Holds goldPerClick, goldPerTick, and tickInterval")]
    [SerializeField] private CurrencyThresholds thresholds;

    [Header("Event (wire extra listeners here if needed)")]
    [Tooltip("Fires whenever the gold total changes")]
    public UnityEvent onGoldChanged;

    int currentGold; // the one true gold total

    ManualCurrency manual; // handles click gains
    IdleCurrency   idle;   // handles tick gains

    // builds the click and tick systems on startup
    void Awake()
    {
        if (thresholds == null)
        {
            Debug.LogError("CurrencyManager: no CurrencyThresholds asset set"); // stops here so nothing crashes
            return;
        }

        currentGold = 0;
        manual = new ManualCurrency(this, thresholds.goldPerClick);
        idle   = new IdleCurrency(this, thresholds.goldPerTick, thresholds.tickInterval);
    }

    // drives the idle tick every frame
    void Update()
    {
        if (idle == null) return;
        idle.Tick(Time.deltaTime);
    }

    // public methods for other scripts to call

    // called by ManualCurrency and IdleCurrency to add gold to the total
    public void AddGold(int amount)
    {
        currentGold += amount;
        onGoldChanged?.Invoke(); // fire event so any listener can react
    }

    // called by the shop when the player buys something
    public void SpendGold(int amount)
    {
        currentGold -= amount;
        onGoldChanged?.Invoke(); // fire event so any listener can react
    }

    // called by CurrencyButton when the player clicks
    public void OnClickGold()
    {
        if (manual == null) return;
        manual.Generate();
    }

    // called by the shop to raise gold per click
    public void UpgradeGoldPerClick(int amount)
    {
        if (manual == null) return;
        manual.AddGoldPerClick(amount);
    }

    // called by the shop to raise gold per tick
    public void UpgradeGoldPerTick(int amount)
    {
        if (idle == null) return;
        idle.AddGoldPerTick(amount);
    }

    // called by the shop to make ticks happen more often
    public void UpgradeTickSpeed(float amount)
    {
        if (idle == null) return;
        idle.ReduceTickInterval(amount);
    }

    // returns the current gold total
    public int GetGold() => currentGold;

    // returns gold generated per second, for UI display
    public float GetGoldPerSecond()
    {
        if (idle == null || idle.GetTickInterval() <= 0f) return 0f;
        return idle.GetGoldPerTick() / idle.GetTickInterval();
    }
}
