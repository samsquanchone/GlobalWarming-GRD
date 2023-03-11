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
    

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("DEBUG - NEUTRAL on New Game")]
    [SerializeField] public Tree Tree_Plantation;
    [SerializeField] public int Tree_Age;

    [NonSerialized] private NationUIManager NationUIManager;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

     ////////////  Sam addition, linking object placement to tile, Enis feel free to link to nation / tweak //////////////////
     
     //Example of getters and setters, any data that could break gameplay should have set to private, however this limits accessability, thus getters and setters coming in handy
     // this example is quick hand getters and setters. Note you won't be able to see values in the inspector like this, unless done in long hand, but it is fully working, and objects being added to these variable counts

     [SerializeField] public int lumbermill_Amount {get; private set;} = 0;
     [SerializeField] public int dock_Amount {get; private set;} = 0;
     [SerializeField] public int trainStation_Amount {get; private set;} = 0;
     [SerializeField] public int pykreteFactory_Amount {get; private set;} = 0;
     [SerializeField] public int activeTree_Amount {get; private set;} = 0;

     //Sam: This variable probably should be in nation, not sure, just getting all the stuff you need from objects for you to tie to your functions for game data
    [SerializeField] public int unprocessedWoodStockpile_Amount {get; private set;} = 0;


    private void Start()
    {
        //Pull Tile Data
        if(Attached_Tiles_Data != null)
        {
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
        }
  
        //Occupiant Nation is given at the start of the game by the occupiant nation.

        //Nation UI Connection
        NationUIManager = GameObject.Find("(!)Nation UI Manager").GetComponent<NationUIManager>();
        GameObject.Find("(!)Date & Time System").GetComponent<Date_and_Time_System>().Month_Pass_Event.AddListener(Calculate_On_Month_Pass);
    }

    public void Calculate_On_Month_Pass()
    {
        Population_Growth_and_Shrink();
    }

    public void Population_Growth_and_Shrink()
    {
        this.Population += (int)(this.Population * 0.01f);
        

        //Effects of Heat

        if(this.Average_Heat_Level > 20)  //High Normal
        {
            this.Population -= (int)(this.Population * 0.015f);
        }

        if (this.Average_Heat_Level > 25) //Very High
        {
            this.Population -= (int)(this.Population * 0.02f);
        }

        if (this.Average_Heat_Level > 25) //Unhabitable
        {
            this.Population -= (int)(this.Population * 0.1f);
        }
    }


    public void Save()
    {
        if (Attached_Tiles_Data != null)
        {
            this.Attached_Tiles_Data.Territory_Name = Territory_Name;
            this.Attached_Tiles_Data.Avaliable_Woodland =Avaliable_Woodland;
            this.Attached_Tiles_Data.Climate_Support = Climate_Support;
            this.Attached_Tiles_Data.Average_Heat_Level = Average_Heat_Level;
            this.Attached_Tiles_Data.Population = Population;
            this.Attached_Tiles_Data.Tera_Factory_Avaliable = Tera_Factory_Avaliable;
            this.Attached_Tiles_Data.Harbor_Avaliable = Harbor_Avaliable;
            this.Attached_Tiles_Data.Lumbermill_Level = Lumbermill_Level = 0;
            this.Attached_Tiles_Data.Tera_Factory_Level = Tera_Factory_Level = 0;
            this.Attached_Tiles_Data.Harbour_Level = Harbour_Level = 0;
            this.Attached_Tiles_Data.Railway_Level = Railway_Level = 0;
        }
    }
    public void Load()
    {
        if (Attached_Tiles_Data != null)
        {
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
        }
    }


    //There is already input mouse button down on PlayerInput, best to abstract functionality unrelated to tiles
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Tile Pressed");
            NationUIManager.Show_Nation_UI(this.Occupiant_Nation) ;
        }
    }

    //Sam Addition: Used for infrastructure, example test variables, do with cases for nation tie in as you see fit.
    // Not sure if you want to limit to 1 infrastructure per tile or be able to track specific objects for their levels.
    // If so could use your level variable an add 1 when spawned and 1 when upgraded

    public void AddObject(ObjectType objectType)
    {
         switch(objectType)
         {
         case ObjectType.Lumbermill:
            lumbermill_Amount += 1;
                Lumbermill_Level++;

         break;

         case ObjectType.Factory:
            pykreteFactory_Amount += 1;
                Tera_Factory_Level++;
         break;

         case ObjectType.Dock:
            dock_Amount += 1;
                Harbour_Level++;
         break;

         case ObjectType.TrainStation:
         trainStation_Amount += 1;
                Railway_Level++;
         break;
         
         default:
         //Is a tree
         activeTree_Amount += 1;

         break;


         }

         Debug.Log(objectType);
    }
 
    //Sam: Feel free to move this function or change variables, just getting the data here for the tree that has been harvested yeild amount, for your data calculations
    public void AddToUnprocessedWoodStockPile(int treeYield)
    {
        unprocessedWoodStockpile_Amount += treeYield;

        //Testing as using short hand getters and setters, so serialize field doesnt show in inspector
        Debug.Log(Occupiant_Nation.Nation_Name + "Has unprocessed wood stocpile of: " + unprocessedWoodStockpile_Amount + "Kgs");
          
    }
 
    //Sam addition: Currently all infastructure apart from pykrete factory can be upgraded, as for factory just going with berg size and not upgrade
    public void UpgradeInfrastructure(InfrastructureType infrastuctureType)
    {
        
        switch(infrastuctureType)
        {
            case InfrastructureType.Lumbermill:
            Lumbermill_Level += 1;
            Debug.Log(Occupiant_Nation.Nation_Name + "Has a " + infrastuctureType.ToString() + " of level" + Lumbermill_Level);
            break;

            case InfrastructureType.Dock:
            Harbour_Level += 1;
            Debug.Log(Occupiant_Nation.Nation_Name + "Has a " + infrastuctureType.ToString() + " of level" + Harbour_Level);

            break;

            case InfrastructureType.TrainStation:
            Railway_Level += 1;
            Debug.Log(Occupiant_Nation.Nation_Name + "Has a " + infrastuctureType.ToString() + " of level" + Railway_Level);

            break;
        }

        
    }

}
