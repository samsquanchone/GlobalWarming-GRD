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
        if (gameObject.tag == "Tree")
        {
            var treeObj = gameObject.GetComponent<Tree>();
            string treeType = gameObject.name;
            string growthTimeRemaining; 
            string expectedYield;
            


            ShowMessage(treeType);
        }

        else
        {
            //Variables shared among infrasture
            string infrastructureName;
            string objectLevel;
            string capacity;
            bool productionActive;

            if (gameObject.GetComponent<LumbermillObject>() != null)
            {
                var infrastrctureObj = gameObject.GetComponent<LumbermillObject>();
                ShowMessage(infrastrctureObj.name); 

            }

            if (gameObject.GetComponent<FactoryObject>())
            {
                var infrastrctureObj = gameObject.GetComponent<FactoryObject>();
                ShowMessage(infrastrctureObj.name);
            }

            if (gameObject.GetComponent<DockObject>())
            {
                var infrastrctureObj = gameObject.GetComponent<DockObject>();
                ShowMessage(infrastrctureObj.name);
            }

            else if (gameObject.GetComponent<TrainStationObject>())
            {
                var infrastrctureObj = gameObject.GetComponent<TrainStationObject>();
                ShowMessage(infrastrctureObj.name);
            }


           
        }

    }
}
