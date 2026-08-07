using UnityEngine;

public class CurrencyButton : MonoBehaviour
{
    [Header("DRAG IN: CurrencyManager from the Hierarchy")]
    [Tooltip("The object that has the CurrencyManager script on it")]
    public CurrencyManager manager;

    // called by the Unity Button component when clicked
    public void OnClick()
    {
        if (manager == null) return;
        manager.OnClickGold();
    }
}
