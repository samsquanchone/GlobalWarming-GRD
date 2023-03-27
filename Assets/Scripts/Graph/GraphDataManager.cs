using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataSet {TREESPLANTED, MONEY, CO2, PYKRETEPRODUCED};

public class GraphDataManager : MonoBehaviour
{
    public static GraphDataManager instance => m_instance;
    private static GraphDataManager m_instance;

    public List<float> pykreteProducedValueList {get; private set;}
    public List<float> moneyValueList {get; private set;}
    public List<float> co2ValueList {get; private set;}
    public List<float> treesPlantedValueList {get; private set;}

    [SerializeField] private GameObject graphPanel;
    [SerializeField] private GameObject OpenGraphButtonObj;



    
    //Data Sets


    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;

       pykreteProducedValueList = new List<float>();
       moneyValueList = new List<float>();
       co2ValueList = new List<float>();
       treesPlantedValueList = new List<float>();

        
    }

    //This is a generic function with its value argument being T on function call you can define data type (Saves having to rewrite for different data sets)
    //This should be tied into a time system, after a year get values in that year and pass to list 
    public void AddValueToDataSet<T>(DataSet dataSet, T value)
    {
        dynamic valueToUse = value;
        switch (dataSet)
        {
           case DataSet.TREESPLANTED:
           treesPlantedValueList.Add(valueToUse);

           break;

           case DataSet.MONEY:
           moneyValueList.Add(valueToUse);
           break;

           case DataSet.CO2:
           co2ValueList.Add(valueToUse);

           break;
           
           case DataSet.PYKRETEPRODUCED:
           pykreteProducedValueList.Add(valueToUse);

           break;
        }

    }

    public void OpenGraph()
    {
        OpenGraphButtonObj.SetActive(false);
        graphPanel.SetActive(true);
    }

    public void CloseGraph()
    {
        OpenGraphButtonObj.SetActive(true);
        graphPanel.SetActive(false);
    }

    

   
}
