using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerToolTip : HoverTip
{


    public override void ShowMessage(string dataToShow)
    {
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

  

    
}
