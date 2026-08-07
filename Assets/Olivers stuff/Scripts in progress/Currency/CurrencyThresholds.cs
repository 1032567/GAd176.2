using UnityEngine;

// right-click in Project window > Currency > Currency Thresholds to create the asset
[CreateAssetMenu(fileName = "CurrencyThresholds", menuName = "Currency/Currency Thresholds")]
public class CurrencyThresholds : ScriptableObject
{
    public int   goldPerClick = 0;  // gold added per click, set in Inspector
    public int   goldPerTick  = 0;  // gold added per tick, set in Inspector
    public float tickInterval = 0f; // seconds between ticks, set in Inspector
}
