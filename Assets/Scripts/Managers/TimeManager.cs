using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //Singleton
    public static TimeManager instance => m_instance;
    private static TimeManager m_instance;

    public List<TreeGrowth> activeTreeList;

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
    
}
