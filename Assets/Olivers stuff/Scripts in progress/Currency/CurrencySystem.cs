using UnityEngine;

// shared foundation, both currency types inherit from this
public abstract class CurrencyBase
{
    protected CurrencyManager manager; // where gold gets reported to

    // stores the manager reference on creation
    protected CurrencyBase(CurrencyManager manager)
    {
        this.manager = manager;
    }

    // each subclass makes gold its own way
    public abstract void Generate();

    // shared helper - sends gold up to the manager
    protected void ReportGold(int amount)
    {
        manager.AddGold(amount);
    }
}

// adds gold once per click, no cap
public class ManualCurrency : CurrencyBase
{
    int goldPerClick; // amount added per click

    // stores the click amount and hooks up the manager
    public ManualCurrency(CurrencyManager manager, int goldPerClick) : base(manager)
    {
        this.goldPerClick = goldPerClick;
    }

    // called once per click, sends the click amount to the manager
    public override void Generate()
    {
        ReportGold(goldPerClick);
        Debug.Log("Gold added from click"); // for testing and debugging
    }
}
