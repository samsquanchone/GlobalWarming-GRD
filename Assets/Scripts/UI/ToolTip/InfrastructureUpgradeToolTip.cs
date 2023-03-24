using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureUpgradeToolTip : HoverTip
{
     [SerializeField] private int cost;
     
    
    public override void ShowMessage(string dataToShow)
    {
        
        UIHoverManager.OnMouseHover("Upgrade Cost: £" + cost + " Million", Input.mousePosition);
    }

}
