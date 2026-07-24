using UnityEngine;

public class XPButton : MonoBehaviour
{
    // this area holds the visible text in  the Inspector, but other scripts cannot change it
    [Header("── DRAG IN: ProgressionManager from the Hierarchy ─────────────")]
    [Tooltip("The object that has the ProgressionSystem script on it")]
    public ProgressionSystem progression;

    [Header("── Settings ───────────────────────────────────────────────────")]
    [Tooltip("XP added each time this button is pressed")]
    public float xpPerClick = 25f;

    public void OnClick() // this function is called when the button is clicked
    {
        if (progression == null) return;
        progression.GainXP(xpPerClick);
    }
}
