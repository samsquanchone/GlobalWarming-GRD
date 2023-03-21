using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerToolTip : HoverTip
{


    public void ShowMessage(string dataToShow)
    {
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

  

    
}
