using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PykreteBergObject : SaveableObject
{
     // Update is called once per frame
    public void RemoveObject()
    {
       
        DestroySaveable();
    }
    public override void Save(int id)
    {
        
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        //Getting data from saved object in list, first variable is index 4 of values array, if another varaible is added it would be index 5 and so on
        
        base.Load(values);
    }
}
