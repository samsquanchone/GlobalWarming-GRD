using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureUpgradeToolTip : HoverTip
{
     [SerializeField] private int cost;
     
    
    public override void ShowMessage(string dataToShow)
    {
        if(UIManager.instance.InspectedObjectTag() == "Tree")
        {
            
            UIHoverManager.OnMouseHover("Harvest and replant cost is 10% of the initial tree project price", Input.mousePosition);
        }

        else
        {
            UIHoverManager.OnMouseHover("Upgrade Cost: £" + cost + " Million", Input.mousePosition);
            
        }
    }

}
