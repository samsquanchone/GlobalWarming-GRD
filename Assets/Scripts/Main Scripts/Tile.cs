using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Tile Inits
    [SerializeField] public string Territory_Name;
    [SerializeField] public Nation Occupiant_Nation;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("DEBUG - TAKE on New Game")]
    [SerializeField] public string Territory_Name;
    [SerializeField] public int Avaliable_Woodland = 0;
    [SerializeField] public int Climate_Support;
    [SerializeField] public int Average_Heat_Level;
    [SerializeField] public int Population;
    [SerializeField] public bool Tera_Factory_Avaliable;
    [SerializeField] public bool Harbor_Avaliable;
    [SerializeField] public int Lumbermill_Level = 0;
    [SerializeField] public int Tera_Factory_Level = 0;
    [SerializeField] public int Harbour_Level = 0;
    [SerializeField] public int Railway_Level = 0;
    //Sam addition: when an object is spawned it will add itself to this list 
    public List<GameObject> nationPlacedObjectsList;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("DEBUG - NEUTRAL on New Game")]
    [SerializeField] public Tree Tree_Plantation;
    [SerializeField] public int Tree_Age;

    //Average Heat Level
    [SerializeField] public int Average_Heat_Level; //Global Warming, Events and Neutral Disasters effect heat levels. Heat levels directly effects Habitability level. 

    //Pop
    [SerializeField] public int Population;

    //Tree Type and Growth
    [SerializeField] public Tree tree;
    [SerializeField] public int Tree_Age;

    }


}
