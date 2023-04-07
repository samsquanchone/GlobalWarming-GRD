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

    public virtual void ShowMessage(string _dataToShow)
    {
        tipToShow = gameObject.name + " Project" + dataToShow;
        UIHoverManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage(null);
    }


    //Shows object info for building types for object placement UI buttons: buildingtype accesses enum in the abstract SaveableObject class to be able to identify objects
    public void SetToolTipData(BuildingTypeSO buildingType)
    {
        //Get enum value for object type of the buildingType for the respective building UIButton
        ObjectType obj = buildingType.buildingPrefab.GetComponent<SaveableObject>().objectType;
        float cost = buildingType.cost;
        int transportationCapacity;

        switch(obj)
        {
            case ObjectType.Lumbermill:
            int productionPerYearKg;
            int maxCapacityKg;
            productionPerYearKg = buildingType.buildingPrefab.GetComponent<LumbermillObject>().pulverisedWoodProductionRate;
            maxCapacityKg = buildingType.buildingPrefab.GetComponent<LumbermillObject>().woodCapacityKg;
            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Production Per Year: " + productionPerYearKg.ToString() + "Kg" + "\n" + "Capacity: " + maxCapacityKg.ToString() + "Kg";
            break;

            case ObjectType.Factory:
            int pykretePergsProducedPerYear;
            int pykreteBergCapacity;
            pykretePergsProducedPerYear = buildingType.buildingPrefab.GetComponent<FactoryObject>().pykreteProductionRate;
            pykreteBergCapacity = buildingType.buildingPrefab.GetComponent<FactoryObject>().pykreteCapacity;
            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Production Per Year: " + pykretePergsProducedPerYear.ToString() + "Bergs" + "\n" + "Capacity: " + pykreteBergCapacity.ToString() + "Bergs";
            break;

            case ObjectType.Dock:
            int numberOfShips;
            transportationCapacity = buildingType.buildingPrefab.GetComponent<DockObject>().transportationCapacityKG;
            numberOfShips = buildingType.buildingPrefab.GetComponent<DockObject>().numberOfShips;
            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Transportation Capacity: " + transportationCapacity.ToString() + "Kg" + "\n" + "Number Of Ships: " + numberOfShips.ToString();
            break;

            case ObjectType.TrainStation:
            int numberOfTrains;
            transportationCapacity = buildingType.buildingPrefab.GetComponent<TrainStationObject>().transportationCapacityKG;
            numberOfTrains = buildingType.buildingPrefab.GetComponent<TrainStationObject>().numberOfTrains;
            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Transportation Capacity: " + transportationCapacity.ToString() + "Kg" + "\n" + "Number Of Ships: " + numberOfTrains.ToString();
            break;

            //Is a tree
            default:
            int yieldKg;
            float timeToGrow;
            float optumumTemp;
            yieldKg = buildingType.buildingPrefab.GetComponent<TreeObject>().yield;
            timeToGrow = buildingType.buildingPrefab.GetComponent<TreeObject>().timeToGrow;
            optumumTemp = buildingType.buildingPrefab.GetComponent<TreeGrowth>().optimumTemperature;
            dataToShow = "\n" + "Cost: £" + cost.ToString() + " Million" + "\n" + "Yield: " + yieldKg.ToString() + "Kg" + "\n" + "Time To Grow: " + timeToGrow.ToString("F2") + " Years" +  "\n" + "Optimum Heat level: " + optumumTemp.ToString() + "Celcius";
            break;

        }
    }
}
