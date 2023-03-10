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
        m_instance = this;
        activeTreeList = new List<TreeGrowth>();
    }

    public void UpdateTreeGrowth()
    {
        //Called when a month passes
        foreach(TreeGrowth activeTree in activeTreeList)
        {
            activeTree.UpdateGrowthTimer();
        }

    }

    

    public void YearPassed()
    {
       DataSet dataSet = DataSet.TREESPLANTED;
       GraphDataManager.instance.AddValueToDataSet(dataSet, treesPlanted);
       treesPlanted = 0;
    }

    
    
}
