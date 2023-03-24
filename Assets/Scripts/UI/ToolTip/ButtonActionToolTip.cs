using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is for tera factor tool tip info
public class ButtonActionToolTip : HoverTip
{
    [SerializeField] private int cost;
     
    
    public override void ShowMessage(string dataToShow)
    {
        
        UIHoverManager.OnMouseHover("Cost: " + cost + " GigaTons of Pykrete", Input.mousePosition);
    }

}
