using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToolTip : NationToolTip
{
    public void OnMouseEnter()
    {

        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnMouseExit()
    {
        StopAllCoroutines();
        UIHoverManager.OnLoseFocus();

    }

    public override void ShowMessage(string dataToShow)
    {
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        SetToolTipData();

    }

    public override void SetToolTipData()
    {
        if (gameObject.GetComponent<TreeObject>() != null)
        {
            ShowMessage("TREE! Show tree related data here!");
        }

        else
        {
            ShowMessage("INFRASTRUCTURE! Place infrastrcture related data here!");
        }

    }
}
