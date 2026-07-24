using UnityEngine;
using UnityEngine.Events;

// shared foundation, both progression types inherit from this
public abstract class ProgressionBase
{
    protected float currentXP;
    protected float target;
    protected int   level;

    // listeners subscribe here, progression never calls them directly
    public UnityEvent onLevelUp = new UnityEvent();

    public abstract void    GainXP(float amount);
    protected abstract void LevelUp();
}

public class LinearProgression : ProgressionBase
{
    int                 maxLevel;
    float[]             thresholds;
    OverloadProgression overload;

    public LinearProgression(float[] thresholds, int maxLevel, OverloadProgression overload) 
    {
        // store the values passed in from the ProgressionSystem
        this.thresholds = thresholds;
        this.maxLevel   = maxLevel;
        this.overload   = overload;
        level           = 1;
        target          = thresholds[0]; // set first target on startup
    }

    public override void GainXP(float amount)
    {
        // redirect to overload if already at cap
        if (IsAtCap()) { overload.GainXP(amount); return; }

        currentXP += amount;

        // loop handles multiple level-ups from one XP gain, cap checked every iteration
        while (currentXP >= target && !IsAtCap())
        {
            currentXP -= target;
            LevelUp();
        }

        if (IsAtCap() && currentXP > 0)
            HandOffLeftover();
    }
// public getters for other scripts to call
    public bool  IsAtCap()      => level >= maxLevel;
    public int   GetLevel()     => level;
    public float GetCurrentXP() => currentXP;
    public float GetTarget()    => target;

    protected override void LevelUp()
    {
        level++;
        int i  = level - 1;
        target = i < thresholds.Length ? thresholds[i] : thresholds[thresholds.Length - 1];
        Debug.Log("Linear level up: " + level);
        onLevelUp?.Invoke(); // fire event so any listener can react
    }

    void HandOffLeftover()
    {
        // leftover XP after hitting the cap passes to overload
        overload.GainXP(currentXP);
        currentXP = 0f;
    }
}

// handles XP past the cap - no ceiling, target grows each level
public class OverloadProgression : ProgressionBase
{
    int   overloadLevel;
    float growthRate;

    public OverloadProgression(float startTarget, float growthRate)
    {
        this.growthRate = growthRate;
        target          = startTarget; // sets first target on startup
        overloadLevel   = 0;
    }

    public override void GainXP(float amount)
    {
        currentXP += amount;
        Debug.Log("XP added to overload pool");

        // loop handles multiple level-ups from one XP gain
        while (currentXP >= target)
        {
            currentXP -= target;
            LevelUp();
        }
    }

    public int   GetOverloadLevel() => overloadLevel;
    public float GetCurrentXP()     => currentXP;
    public float GetTarget()        => target;

    protected override void LevelUp()
    {
        overloadLevel++;
        target *= growthRate; // multiply target to make each level harder
        Debug.Log("Overload level up: " + overloadLevel);
        onLevelUp?.Invoke(); // fire event so any listener can react
    }
}

public class ProgressionSystem : MonoBehaviour
{
    // SerializeField keeps these visible in the Inspector but blocks other scripts from changing them
    [SerializeField] private XPThresholds thresholds; 
    [SerializeField] private int          maxLevel          = 5;    // Inspector: linear level cap
    [SerializeField] private float        overloadGrowthRate = 1.5f; // Inspector: overload difficulty multiplier

    public UnityEvent onLevelUp;

    LinearProgression   linear;
    OverloadProgression overload;

    void Awake()
    {
        // Awake so pools exist before XPDisplay.Start() subscribes
        float[] xpList      = thresholds.xpList;
        float overloadStart = xpList[xpList.Length - 1];
        overload = new OverloadProgression(overloadStart, overloadGrowthRate);
        linear   = new LinearProgression(xpList, maxLevel, overload);
    }

    void Start()
    {
        // route both pool events through the single public event
        linear.onLevelUp.AddListener(()  => onLevelUp?.Invoke());
        overload.onLevelUp.AddListener(() => onLevelUp?.Invoke());
    }

// public methods for other scripts to call
    public void  GainXP(float amount) => linear.GainXP(amount);
    public int   GetLevel()           => linear.GetLevel();
    public float GetCurrentXP()       => linear.GetCurrentXP();
    public float GetTarget()          => linear.GetTarget();
    public bool  IsAtCap()            => linear.IsAtCap();
    public int   GetOverloadLevel()   => overload.GetOverloadLevel();
    public float GetOverloadXP()      => overload.GetCurrentXP();
    public float GetOverloadTarget()  => overload.GetTarget();
}
