using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tile : MonoBehaviour
{
    [SerializeField] Tile_Data Attached_Tiles_Data;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space]
    [Header("DEBUG - GIVE on New Game")]
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

    [NonSerialized] private NationUIManager NationUIManager;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void Start()
    {
        //Pull Tile Data
        this.Territory_Name = Attached_Tiles_Data.Territory_Name;
        this.Avaliable_Woodland = Attached_Tiles_Data.Avaliable_Woodland;
        this.Climate_Support = Attached_Tiles_Data.Climate_Support;
        this.Average_Heat_Level = Attached_Tiles_Data.Average_Heat_Level;
        this.Population = Attached_Tiles_Data.Population;
        this.Tera_Factory_Avaliable = Attached_Tiles_Data.Tera_Factory_Avaliable;
        this.Harbor_Avaliable = Attached_Tiles_Data.Harbor_Avaliable;
        this.Lumbermill_Level = Attached_Tiles_Data.Lumbermill_Level = 0;
        this.Tera_Factory_Level = Attached_Tiles_Data.Tera_Factory_Level = 0;
        this.Harbour_Level = Attached_Tiles_Data.Harbour_Level = 0;
        this.Railway_Level = Attached_Tiles_Data.Railway_Level = 0;

        //Occupiant Nation is given at the start of the game by the occupiant nation.

        //Nation UI Connection
        NationUIManager = GameObject.Find("(!)Nation UI Manager").GetComponent<NationUIManager>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tile Pressed");
            NationUIManager.Show_Nation_UI(this.Occupiant_Nation) ;
        }
    }

}
