using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : SaveableObject
{
    //Variables that will passed into the save string variable to be serialized
    
    //Long hand getters and setters so values can be seen in the inspector
    
    [SerializeField]
    public int cost
    {
        get { return m_cost; }
        private set { m_cost = value; }
    }

    public int m_cost;

    [SerializeField]
    public int yield
    {
        get { return m_yield; }
        private set { m_yield = value; }
    }

    public int m_yield;

    [SerializeField]
    public float timeToGrow
    {
        get { return m_timeToGrow; }
        private set { m_timeToGrow = value; }
    }

    public float m_timeToGrow;

    
    private float GetGrowthTimeRemaining()
    {
        timeToGrow = GetComponent<TreeGrowth>().GetGrowthTimeRemaining();

        return timeToGrow;
    }
    // Update is called once per frame
    public void RemoveObject()
    {
        //This function will remove object, could also be used to add yield amount to a countries wood amount for example
        GetComponent<TreeGrowth>().HarvestTree();
        DestroySaveable();
    }
    public override void Save(int id)
    {
        //Set savestats of this object to be serialized, note if adding another variable add + "_" + newVar.ToString()
        timeToGrow = GetGrowthTimeRemaining();
        saveStats = timeToGrow.ToString();
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        //Getting data from saved object in list, first variable is index 4 of values array, if another varaible is added it would be index 5 and so on
        timeToGrow = int.Parse(values[4]);
        base.Load(values);
    }
}
