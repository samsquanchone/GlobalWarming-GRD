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
         //Get enum value for object type of the buildingType for the respective building UIButton
        ObjectType obj = GetComponent<SaveableObject>().objectType;
        string objectName = GetComponent<SaveableObject>().objectType.ToString();

        switch(obj)
        {
            case ObjectType.Lumbermill:
            ShowMessage(objectName);
            break;

            case ObjectType.Factory:
            ShowMessage(objectName);
            break;

            case ObjectType.Dock:
            ShowMessage(objectName);
            break;

            case ObjectType.TrainStation:
            ShowMessage(objectName);
            break;

            //Is a tree
            default:
            ShowMessage(objectName);
            break;
        }
    }
}
