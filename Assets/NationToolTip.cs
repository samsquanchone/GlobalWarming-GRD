using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Was going to use polymorphism here an inherit the HoverTip, however, Ipointer enter and exit were not working for non UI, so decided to not inherit, may inherit this class for gameobject Tooltips 
public class NationToolTip : MonoBehaviour
{
    protected string tipToShow;
    protected float timeToWait = 0.5f;
    protected string dataToShow;

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

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        SetToolTipData();

        ShowMessage();
    }

    public void ShowMessage()
    {
        Debug.Log("Greetings");
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

    public void SetToolTipData()
    {
        string nationName;
        float gdp;
        int treesToHarvest;
        int lumberMillsPlaced;
        int docksPlaced;
        int trainStationsPlaced;

        nationName = gameObject.name;

        dataToShow = nationName;

    }

}
