using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //Singleton
    public static TimeManager instance => m_instance;
    private static TimeManager m_instance;
    

    //For tracking active tree growth
    public List<TreeGrowth> activeTreeList;

    //For storing values over a certain time to pass to graph data manager
    public int treesPlanted = 0;

    void Awake()
    {
        //Initialize singleton
        
        activeTreeList = new List<TreeGrowth>();
    }

    void Start()
    {
        m_instance = this;
        DataSet dataSetPlayerWealth = DataSet.MONEY;
        GraphDataManager.instance.AddValueToDataSet(dataSetPlayerWealth, Player.instance.GetPlayerWealth());

        DataSet dataSetMonthlyHeatRise = DataSet.CO2;
        GraphDataManager.instance.AddValueToDataSet(dataSetMonthlyHeatRise, Player.instance.GetMonthlyHeatRise());

        DataSet dataSetTreesPlanted = DataSet.TREESPLANTED;
        GraphDataManager.instance.AddValueToDataSet(dataSetTreesPlanted, 0);

        DataSet dataSetPopulation = DataSet.POPULATION;
        GraphDataManager.instance.AddValueToDataSet(dataSetPopulation, GetPopulation());

        DataSet dataSetTimber = DataSet.TIMBER;
        GraphDataManager.instance.AddValueToDataSet(dataSetTimber, Player.instance.GetTimberStockPile());

        DataSet dataSetPykerete = DataSet.PYKRETEPRODUCED;
        GraphDataManager.instance.AddValueToDataSet(dataSetPykerete, Player.instance.GetPkyreteStockPile());
    }


    public void UpdateTreeGrowth()
    {
        //Called when a month passes
        foreach(TreeGrowth activeTree in activeTreeList)
        {
            activeTree.UpdateGrowthTimer();
        }

    }

    private float GetPopulation()
    {
        float population = 0;
         //Get all tiles and add population onto local population 
        Tile[] tiles = FindObjectsOfType(typeof(Tile)) as Tile[];
        foreach(var t in tiles)
        {
            population += t.GetPopulation();
        } 

        return population;
    }

    

    public void YearPassed()
    {
       DataSet dataSetTreesPlanted = DataSet.TREESPLANTED;
       GraphDataManager.instance.AddValueToDataSet(dataSetTreesPlanted, treesPlanted);
       treesPlanted = 0;

       DataSet dataSetPlayerWealth = DataSet.MONEY;
       GraphDataManager.instance.AddValueToDataSet(dataSetPlayerWealth, Player.instance.GetPlayerWealth());
       
       DataSet dataSetMonthlyHeatRise = DataSet.CO2;
       GraphDataManager.instance.AddValueToDataSet(dataSetMonthlyHeatRise, Player.instance.GetMonthlyHeatRise());
       
       DataSet dataSetPopulation = DataSet.POPULATION;
       GraphDataManager.instance.AddValueToDataSet(dataSetPopulation, GetPopulation());

       DataSet dataSetTimber = DataSet.TIMBER;
       GraphDataManager.instance.AddValueToDataSet(dataSetTimber, Player.instance.GetTimberStockPile());

       DataSet dataSetPykerete = DataSet.PYKRETEPRODUCED;
       GraphDataManager.instance.AddValueToDataSet(dataSetPykerete, Player.instance.GetPkyreteStockPile());
       
       //Only bit of manual garbage collection in the code base, thought it best to be done each year
       System.GC.Collect(); 
       System.GC.WaitForPendingFinalizers(); 

    }

    
    
}
