using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Was going to use polymorphism here an inherit the HoverTip, however, Ipointer enter and exit were not working for non UI, so decided to not inherit, may inherit this class for gameobject Tooltips 
public class NationToolTip : MonoBehaviour
{
    protected string tipToShow;
    protected float timeToWait = 0.5f;
    private string dataToShow;


    int lumberMillsPlaced = 0;
    int activeTreeProjects = 0;
    int docksPlaced = 0;
    int trainStationsPlaced = 0;
    

    public void OnMouseEnter()
    {
        
        StopAllCoroutines();
        StartCoroutine(StartTimer());
        
    }

    public void OnMouseExit()
    {
        StopAllCoroutines();
        UIHoverManager.OnLoseFocus();
        lumberMillsPlaced = 0;
        activeTreeProjects = 0;
        docksPlaced = 0;
        trainStationsPlaced = 0;
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        SetToolTipData();

       
    }

    public void ShowMessage(string dataToShow)
    {
        Debug.Log("Greetings");
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

    public virtual void SetToolTipData()
    {
        string nationName;
        float gdp;
        

        nationName = gameObject.name;

        //Off the cuff way of doing this, probs not best to iterate over list every time mouse of UI - REFACTOR / CHANGE later
        foreach (GameObject obj in gameObject.GetComponent<Tile>().nationPlacedObjectsList)
        {
            Debug.Log(obj.name);

            if (obj.name == "Lumbermill(Clone)")
            {
                lumberMillsPlaced++;
            }


            if (obj.name == "Dock(Clone)")
            {
                docksPlaced++;
            }

            if (obj.name == "TrainStation(Clone)")
            {
                trainStationsPlaced++;
            }

            if (obj.name == "Oak(Clone)" || obj.name == "Bamboo(Clone)" || obj.name == "Willow(Clone)")
            {
                activeTreeProjects++;
            }
        }

        dataToShow = nationName + "\n" + "Active Tree Projects: " + activeTreeProjects + "\n" + "Lumbermills: " + lumberMillsPlaced + "\n" + "Docks: " + docksPlaced + "\n" + "Train Stations: " + trainStationsPlaced;

        ShowMessage(dataToShow);
    }

}
