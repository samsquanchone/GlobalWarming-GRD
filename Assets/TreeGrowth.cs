using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    int monthsRemaining;
    // Start is called before the first frame update
    void Start()
    {
        //Get components and the trees initial growth time
        TimeManager.instance.activeTreeList.Add(this.GetComponent<TreeGrowth>());
        monthsRemaining = GetComponent<TreeObject>().m_timeToGrow; //Maybe divide this by a heat factor on placed country
    }

    public void UpdateGrowthTimer()
    {
        monthsRemaining -= 1;
    }

    public int GetGrowthTimeRemaining()
    {
        int timeRemaining = monthsRemaining;

        return timeRemaining;
    }
}
