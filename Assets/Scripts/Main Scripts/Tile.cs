using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Tile Inits
    [SerializeField] public string Territory_Name;
    [SerializeField] public Nation Occupiant_Nation;

    

    [SerializeField] public int Habitability; //Between 0-100%
        /*
        Continental (Canada, Northern America, Europe, Northern China, and Japan): 95% Habitability	
        Temperate or Tropical (Part of Europe, China, Most of Russia, Most of America, South Africa, Part of Mexico, Japan and Part of Canada): 100% Habitability
        Dry (Northern Africa, Most of Mexica, Southern Africa): 80% Habitability
        Polar or Desert (Northern Africa, Most of Mexica, Southern Africa): 50% Habitability
        */

    //Average Heat Level
    [SerializeField] public int Average_Heat_Level; //Global Warming, Events and Neutral Disasters effect heat levels. Heat levels directly effects Habitability level. 

    //Pop
    [SerializeField] public int Population;

    //Tree Type and Growth
    [SerializeField] public Tree tree;
    [SerializeField] public int Tree_Age;

    //Buildings (Is built or not)
    [SerializeField] public bool Harbor_Avaliable;
    [SerializeField] public bool Autobahn;
    [SerializeField] public bool Railway;
    [SerializeField] public bool Airport;
}
