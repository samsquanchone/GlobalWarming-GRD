using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Tile_Data : ScriptableObject
{
    //Tile Inits
    public string Territory_Name;
    public Nation Occupiant_Nation;

    

    public int Climate_Support; //Between 0-100%
    /*Represents total max population allowance for the tile and life support. If the climate support gets reduced, pops on that tile starts to die.*/


    //Average Heat Level
    public float Average_Heat_Level; //Global Warming, Events and Neutral Disasters effect heat levels. Heat levels directly effects Habitability level. 

    //Pop
    public float Population;

    //Tree Type and Growth
    public Tree Tree_Plantation;
    public int Tree_Age;

    //Woodland Avaliable
    public int Avaliable_Woodland = 0;

    //Is Avaliable
    public bool Tera_Factory_Avaliable;
    public bool Harbor_Avaliable;

    //Buildigns and levels
    //Production Buildings
    public int Lumbermill_Level = 0;
    public int Tera_Factory_Level = 0;

    //Logistics Buildings
    public int Harbour_Level = 0;
    public int Railway_Level = 0;

}
