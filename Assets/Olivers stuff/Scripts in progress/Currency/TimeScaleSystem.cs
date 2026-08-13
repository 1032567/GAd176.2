using UnityEngine;

// adds gold over time, ticks on an interval
public class IdleCurrency : CurrencyBase
{
    int   goldPerTick;  // amount added per tick
    float tickInterval; // seconds between ticks
    float timer;        // counts up toward the next tick

    // stores the tick values and hooks up the manager
    public IdleCurrency(CurrencyManager manager, int goldPerTick, float tickInterval) : base(manager)
    {
        this.goldPerTick  = goldPerTick;
        this.tickInterval = tickInterval;
        timer             = 0f;
    }

    // one tick worth of gold, sends it to the manager
    public override void Generate()
    {
        ReportGold(goldPerTick);
        Debug.Log("Gold added from tick"); // for testing and debugging
    }

    // call this every frame from CurrencyManager.Update()
    public void Tick(float deltaTime)
    {
        if (tickInterval <= 0f) return; // guard - no ticking until tickInterval is set above 0

        timer += deltaTime; // add this frame's time to the timer

        // loop handles more than one tick in a big deltaTime
        while (timer >= tickInterval)
        {
            timer -= tickInterval;
            Generate();
        }
    }

    // raises goldPerTick, called by the shop
    public void AddGoldPerTick(int amount)
    {
        goldPerTick += amount;
    }

    // shrinks tickInterval, called by the shop
    public void ReduceTickInterval(float amount)
    {
        tickInterval = Mathf.Max(0.1f, tickInterval - amount); // guard - never let ticking stop completely
    }

    // returns the current gold-per-tick amount, for UI display
    public int GetGoldPerTick() => goldPerTick;

    // returns the current tick interval, for UI display
    public float GetTickInterval() => tickInterval;
}
