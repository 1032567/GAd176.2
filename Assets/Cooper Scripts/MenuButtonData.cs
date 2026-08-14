using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MenuButtonData
{
    public string label;       // text shown on the button
    public UnityAction onClick; // what happens when clicked
}

