using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportTip : HoverTip
{
    [SerializeField] string tip;

  


    public override void ShowMessage(string _dataToShow)
    {
        tipToShow = tip;
        UIHoverManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

   
}
