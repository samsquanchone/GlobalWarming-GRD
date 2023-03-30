using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Was going to use polymorphism here an inherit the HoverTip, however, Ipointer enter and exit were not working for non UI, so decided to not inherit, may inherit this class for gameobject Tooltips 
public class NationToolTip : MonoBehaviour
{   
    //Tool tip variables
    protected string tipToShow;
    protected float timeToWait = 0.5f;
    private string dataToShow;

    //Nation data
    int gdp;
    int lumberMillsPlaced = 0;
    int activeTreeProjects = 0;
    int docksPlaced = 0;
    int trainStationsPlaced = 0;
    float averageHeatLevel;
    

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

       
    }

    public virtual void ShowMessage(string dataToShow)
    {
        
        UIHoverManager.OnMouseHover(dataToShow, Input.mousePosition);
    }

    public virtual void SetToolTipData()
    {
        string nationName;
        
        

        nationName = gameObject.name;
        //gdp = gameObject.GetComponent<Tile>();
        

        if(gameObject.GetComponent<Tile>() != null)
        {
           gdp = gameObject.GetComponent<Tile>().Occupiant_Nation.GDP;
           averageHeatLevel = gameObject.GetComponent<Tile>().Average_Heat_Level;
           lumberMillsPlaced = gameObject.GetComponent<Tile>().lumbermill_Amount;
           docksPlaced = gameObject.GetComponent<Tile>().dock_Amount;
           trainStationsPlaced = gameObject.GetComponent<Tile>().trainStation_Amount;
           activeTreeProjects = gameObject.GetComponent<Tile>().activeTree_Amount;

        }

        dataToShow = nationName + "\n" + "GDP: £" + gdp.ToString() + " Million" + "\n" + "Average Heat level: " + averageHeatLevel + "\n" + "Active Tree Projects: " + activeTreeProjects + "\n" + "Lumbermills: " + lumberMillsPlaced + "\n" + "Docks: " + docksPlaced + "\n" + "Train Stations: " + trainStationsPlaced;

        ShowMessage(dataToShow);
        
    }

}
