using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphToolTip : HoverTip
{
    [SerializeField] private string message;

    public override void ShowMessage(string dataToShow)
    {
        //Show message set in inspector for respective graph button
        UIHoverManager.OnMouseHover(message, Input.mousePosition);
        
    }
}
