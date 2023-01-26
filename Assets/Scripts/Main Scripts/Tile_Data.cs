using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Tile_Data : ScriptableObject
{
    //Tile Inits
    [SerializeField] public string Territory_Name;
    [SerializeField] public Nation Occupiant_Nation;

    

    [SerializeField] public int Climate_Support; //Between 0-100%
    /*Represents total max population allowance for the tile and life support. If the climate support gets reduced, pops on that tile starts to die.*/


    //Average Heat Level
    [SerializeField] public int Average_Heat_Level; //Global Warming, Events and Neutral Disasters effect heat levels. Heat levels directly effects Habitability level. 

    //Pop
    [SerializeField] public int Population;

    //Tree Type and Growth
    [SerializeField] public Tree Tree_Plantation;
    [SerializeField] public int Tree_Age;

    //Woodland Avaliable
    [SerializeField] public int Avaliable_Woodland = 0;

    //Is Avaliable
    [SerializeField] public bool Tera_Factory_Avaliable;
    [SerializeField] public bool Harbor_Avaliable;

    //Buildigns and levels
    //Production Buildings
    [SerializeField] public int Lumbermill_Level = 0;
    [SerializeField] public int Tera_Factory_Level = 0;

    //Logistics Buildings
    [SerializeField] public int Harbour_Level = 0;
    [SerializeField] public int Railway_Level = 0;

}
