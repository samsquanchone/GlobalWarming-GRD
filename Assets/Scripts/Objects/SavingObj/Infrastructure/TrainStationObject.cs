using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStationObject : SaveableObject
{
    //Variables that will passed into the save string variable to be serialized
    public int cost;
    public int transportationCapacityKG;
    public int numberOfTrains;

    // Update is called once per frame
    void RemoveObject()
    {
        //This function will remove object, could also be used to add yield amount to a countries wood amount for example
        DestroySaveable();
    }

    public override void Save(int id)
    {
        //Set savestats of this object to be serialized, note if adding another variable add + "_" + newVar.ToString()
        saveStats = transportationCapacityKG.ToString();
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        //Getting data from saved object in list, first variable is index 4 of values array, if another varaible is added it would be index 5 and so on
        transportationCapacityKG = int.Parse(values[4]);
        base.Load(values);
    }
}
