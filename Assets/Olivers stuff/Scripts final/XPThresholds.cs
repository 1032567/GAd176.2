using UnityEngine;

// right-click in Project window > Progression > XP Thresholds to create the asset
[CreateAssetMenu(fileName = "XPThresholds", menuName = "Progression/XP Thresholds")] //this makes the scriptable object appear in the Create menu
public class XPThresholds : ScriptableObject
{
    public float[] xpList = { 100, 200, 350, 500, 700 }; // default values, can be changed in the Inspector
}
