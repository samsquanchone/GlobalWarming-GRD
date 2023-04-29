using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelperLibrary.UI;  //Namespace for some static classes that provide various easy to access functionality

public enum DataSet {TREESPLANTED, MONEY, CO2, PYKRETEPRODUCED, POPULATION, TIMBER};

public class GraphDataManager : MonoBehaviour
{
    public static GraphDataManager instance => m_instance;
    private static GraphDataManager m_instance;

    public List<float> pykreteProducedValueList {get; private set;}
    public List<float> moneyValueList {get; private set;}
    public List<float> co2ValueList {get; private set;}
    public List<float> treesPlantedValueList {get; private set;}
    public List<float> populationValueList {get; private set;} 
    public List<float> timberValueList {get; private set;} 
    public List<float> pykereteValueList {get; private set;} 

    [SerializeField] private GameObject graphPanel;
    [SerializeField] private GameObject OpenGraphButtonObj;

    [SerializeField] private GameObject closeButtonObj;



    
    //Data Sets


    // Start is called before the first frame update
    void Start()
    {

        m_instance = this;
       
       JSONManager.CreateDirectorys();
       pykreteProducedValueList = new List<float>();
       moneyValueList = new List<float>();
       co2ValueList = new List<float>();
       treesPlantedValueList = new List<float>();
       populationValueList = new List<float>();
       timberValueList = new List<float>();

       closeButtonObj.GetComponent<Button_UI>().ClickFunc = () => { CloseGraph();}; //Set up delegate to close graph when the close button is pressed

        
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

           case DataSet.POPULATION:
           populationValueList.Add(valueToUse);
           break;

           case DataSet.TIMBER:
           timberValueList.Add(valueToUse);
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

    public void SaveGraphValues()
    {
        SaveGraphData pykreteValues = new SaveGraphData();
        pykreteValues.values = pykreteProducedValueList;
        JSONManager.SaveGraphJSON(pykreteValues, "PykreteValues");

        SaveGraphData moneyValues = new SaveGraphData();
        moneyValues.values = moneyValueList;
        JSONManager.SaveGraphJSON(moneyValues, "MoneyValues");

        SaveGraphData co2Values = new SaveGraphData();
        co2Values.values = co2ValueList;
        JSONManager.SaveGraphJSON(co2Values, "Co2Values");

        SaveGraphData populationValues = new SaveGraphData();
        populationValues.values = populationValueList;
        JSONManager.SaveGraphJSON(populationValues, "PopulationValues");

        SaveGraphData timberValues = new SaveGraphData();
        timberValues.values = timberValueList;
        JSONManager.SaveGraphJSON(timberValues, "TimberValues");

        SaveGraphData treesPlantedValues = new SaveGraphData();
        treesPlantedValues.values = treesPlantedValueList;
        JSONManager.SaveGraphJSON(treesPlantedValues, "TreesPlantedValues");


    }

    public void LoadGraphValues()
    {
        //Clear lists first for good memory management
        pykreteProducedValueList = new List<float>();
        moneyValueList = new List<float>();
        co2ValueList = new List<float>();
        populationValueList = new List<float>();
        timberValueList = new List<float>();
        treesPlantedValueList = new List<float>();

        //Populate lists
        SaveGraphData pykreteValues = JSONManager.LoadGraphData("PykreteValues");
        pykreteProducedValueList = pykreteValues.values;

        //Populate lists
        SaveGraphData moneyValues = JSONManager.LoadGraphData("MoneyValues");
        moneyValueList = moneyValues.values;

        //Populate lists
        SaveGraphData co2Values = JSONManager.LoadGraphData("Co2Values");
        co2ValueList = co2Values.values;

        //Populate lists
        SaveGraphData populationValues = JSONManager.LoadGraphData("PopulationValues");
        populationValueList = populationValues.values;

        //Populate lists
        SaveGraphData timberValues = JSONManager.LoadGraphData("TimberValues");
        timberValueList = timberValues.values;

         //Populate lists
        SaveGraphData treesPlantedValues = JSONManager.LoadGraphData("TreesPlantedValues");
        treesPlantedValueList = treesPlantedValues.values;

    }

    

   
}

//Class to serialize graph data when player saves game
[System.Serializable]
public class SaveGraphData
{
   public List<float> values;
}
