using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] Color highlightedColor;
    [SerializeField] Color equipedColor;
    [SerializeField] Dialog toolTip;
    
    public Color HighlightedColor => highlightedColor;
    public Color EquipedColor => equipedColor;

    public Dialog ToolTip => toolTip;

    public static GlobalSettings i { get; private set; }

    private void Awake()
    {
        i = this;
    }
}
