using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataSet {TREESPLANTED, MONEY, CO2, PYKRETEPRODUCED};

public class GraphDataManager : MonoBehaviour
{
    public static GraphDataManager instance => m_instance;
    private static GraphDataManager m_instance;

    public List<int> pykreteProducedValueList {get; private set;}
    public List<int> moneyValueList {get; private set;}
    public List<int> co2ValueList {get; private set;}
    public List<int> treesPlantedValueList {get; private set;}



    
    //Data Sets


    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;

       pykreteProducedValueList = new List<int>();
       moneyValueList = new List<int>();
       co2ValueList = new List<int>();
       treesPlantedValueList = new List<int>();

        
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

    

   
}
