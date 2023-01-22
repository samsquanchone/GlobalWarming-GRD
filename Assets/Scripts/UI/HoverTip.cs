using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



//Maybe inherit this class for Nation UI hover ability, then get different components from Object: E.g. Name, GDP, Pykrete Production
public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected string tipToShow;
    protected float timeToWait = 0.5f;
    protected string dataToShow;


    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        UIHoverManager.OnLoseFocus();
    }

    protected virtual void ShowMessage()
    {
        tipToShow = gameObject.name + " Project" + dataToShow;
        UIHoverManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }


    //This is set on initialization, essentially creating tooltip data to be able to show object placement info from hovering over buttons
    // NOTE: THIS IS JUST AN OFF THE CUFF WAY OF DOING THIS, SOME DATA IS SIMILAR, COULD BUNCH SIMILAR CLASSES UP INTO MORE GENERAL OBJECT TYPE, SIMILAR TO THE TREE OBJECT TYPE CLASS
    public void SetToolTipData(BuildingTypeSO buildingType)
    {
        float cost;
        int transportationCapacity;

        if (buildingType.buildingPrefab.GetComponent<TreeObject>() != null)
        {

            int yieldKg;
            int timeToGrow;

            //Note currently this is being done off the saveable objects script, for object placement UI, may not want to do this, although as values are only set on initialization, it may be ok
            cost = buildingType.buildingPrefab.GetComponent<TreeObject>().cost;
            yieldKg = buildingType.buildingPrefab.GetComponent<TreeObject>().yield;
            timeToGrow = buildingType.buildingPrefab.GetComponent<TreeObject>().timeToGrow;

            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Yield: " + yieldKg.ToString() + "Kg" + "\n" + "Time To Grow: " + timeToGrow.ToString() + " Years";
        }

        if (buildingType.buildingPrefab.GetComponent<LumbermillObject>() != null)
        {
            int productionPerYearKg;
            int maxCapacityKg;

            cost = buildingType.buildingPrefab.GetComponent<LumbermillObject>().cost;
            productionPerYearKg = buildingType.buildingPrefab.GetComponent<LumbermillObject>().pulverisedWoodProductionRate;
            maxCapacityKg = buildingType.buildingPrefab.GetComponent<LumbermillObject>().woodCapacityKg;

            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Production Per Year: " + productionPerYearKg.ToString() + "Kg" + "\n" + "Capacity: " + maxCapacityKg.ToString() + "Kg";
        }

        if (buildingType.buildingPrefab.GetComponent<FactoryObject>() != null)
        {
            int pykretePergsProducedPerYear;
            int pykreteBergCapacity;

            cost = buildingType.buildingPrefab.GetComponent<FactoryObject>().cost;
            pykretePergsProducedPerYear = buildingType.buildingPrefab.GetComponent<FactoryObject>().pykreteProductionRate;
            pykreteBergCapacity = buildingType.buildingPrefab.GetComponent<FactoryObject>().pykreteCapacity;

            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Production Per Year: " + pykretePergsProducedPerYear.ToString() + "Bergs" + "\n" + "Capacity: " + pykreteBergCapacity.ToString() + "Bergs";
        }

        if (buildingType.buildingPrefab.GetComponent<DockObject>() != null)
        {
            int numberOfShips;

            cost = buildingType.buildingPrefab.GetComponent<DockObject>().cost;
            transportationCapacity = buildingType.buildingPrefab.GetComponent<DockObject>().transportationCapacityKG;
            numberOfShips = buildingType.buildingPrefab.GetComponent<DockObject>().numberOfShips;

            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Transportation Capacity: " + transportationCapacity.ToString() + "Kg" + "\n" + "Number Of Ships: " + numberOfShips.ToString();
        }

        else if (buildingType.buildingPrefab.GetComponent<TrainStationObject>() != null)
        {
            int numberOfTrains;

            cost = buildingType.buildingPrefab.GetComponent<TrainStationObject>().cost;
            transportationCapacity = buildingType.buildingPrefab.GetComponent<TrainStationObject>().transportationCapacityKG;
            numberOfTrains = buildingType.buildingPrefab.GetComponent<TrainStationObject>().numberOfTrains;

            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Transportation Capacity: " + transportationCapacity.ToString() + "Kg" + "\n" + "Number Of Ships: " + numberOfTrains.ToString();

        }

    }
}
