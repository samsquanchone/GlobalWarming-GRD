using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Random = UnityEngine.Random;

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
    [SerializeField] public float Average_Heat_Level;
    [SerializeField] public float Population;
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
     
    //[NonSerialized] private NationUIManager NationUIManager; //Sam edit: this is breaking your system, changing it to a singleton. Why make a copy of this for every nation when ui manager can be one script as singleton?

    [SerializeField] private Canvas Nation_Wiew_Canvas;
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
    //[SerializeField] public int unprocessedWoodStockpile_Amount {get; private set;} = 0;

   // Player PlayerManager; //Sam edit: again should be used through a static reference not create a instance for every nation or tile, will use singleton static references instead
    [SerializeField] Button Load_Button;
    [SerializeField] Button Save_Button;
    private void Awake()
    {
       // Load_Button = GameObject.Find("LoadButton").GetComponent<Button>();
        //Save_Button = GameObject.Find("SaveButton").GetComponent<Button>();

        //Load_Button.onClick.AddListener(Load);
        //Save_Button.onClick.AddListener(Save);
    }
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
        //NationUIManager = GameObject.Find("(!)Nation UI Manager").GetComponent<NationUIManager>(); //Sam edit: again these should be single scripts not be intantiated for every nations: making both of them singletons
       //PlayerManager = GameObject.Find("(!)Player Manager").GetComponent<Player>();

       //Sam edit: changed to singleton instance reference for better memory management 
        Date_and_Time_System.instance.Month_Pass_Event.AddListener(Calculate_On_Month_Pass);


        //RANDOMIZED
        //Randomize_Values();
    }

    public void Randomize_Values()
    {
        int randomized_avarage_heat_level = Random.Range(5, 26);
        this.Average_Heat_Level = randomized_avarage_heat_level;

        int randomized_pop = Random.Range(100000, 30000000);
        this.Population = randomized_pop;

        int randomized_building = Random.Range(0, 30);
        this.Lumbermill_Level = randomized_building;


    }
    public void Calculate_On_Month_Pass()
    {
        Population_Growth_and_Shrink();
        Heat_Levels_Rise_and_Fall();
    }

    public void Population_Growth_and_Shrink()
    {
        //Neutral Population Growth

        /*The current global RNI is estimated to be around 1.1% per year, which translates to approximately 0.092% per month*/
        this.Population += (int)(this.Population * 0.001f);
        

        //Effects of Heat

        //Normal avarage heat level of europe is 13 Celsius

        if(this.Average_Heat_Level > 20)  //High
        {
            this.Population -= (int)(this.Population * 0.015f);
        }

        if (this.Average_Heat_Level > 23) //Very High
        {
            this.Population -= (int)(this.Population * 0.02f);
        }

        //Deserts have an avarage heat level of 25 Celsius
        if (this.Average_Heat_Level > 26) //Unhabitable
        {
            this.Population -= (int)(this.Population * 0.1f);
        }
    }

    public void Heat_Levels_Rise_and_Fall()
    {
        //Effects Everything

        /*
        The Intergovernmental Panel on Climate Change (IPCC) has stated that the global temperature has increased by about 1.1 degrees Celsius since the pre-industrial era (1850-1900), and is expected to continue to rise if greenhouse gas emissions are not reduced.
        Becouse of the main indistrulization happened after 1940s, we can estimate global warming increases by 1 for each 70 to 100 years.
        To determine how much something increases per month if it increases 1 per 70 years, we need to first convert 70 years into months. Since there are 12 months in a year, 70 years is equal to 840 months (70 years x 12 months/year = 840 months).
        Next, we can divide the total increase of 1 by the total number of months (1/840) to find the increase per month.

        1/840 = 0.00119048 (+0.0012 Celsius per month)

        Therefore, if something is increasing 1 per 70 years, it would increase by approximately 0.0012 per month.
         */
        //MONTHLY Heat Level Rise /// Sam edit: removed reference and used singleton reference for better memory management + used the getter in the singleton for safer memory usage 
        this.Average_Heat_Level += Player.instance.GetMonthlyHeatRise();

        //Effects of Heat Level Fall
        //According to the PYKERETE SEND TO ANTRATICA

        this.Average_Heat_Level -= 0;
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
    }

    private void OnMouseDown()
    {
        Debug.Log("Tile Pressed");
        Debug.Log("Nation Pressed is: " + Occupiant_Nation.Nation_Name);
        NationUIManager.instance.Tile_Pressed = true;
        NationUIManager.instance.Show_Nation_UI(this.Occupiant_Nation);
    }
    //Sam Addition: Used for infrastructure, example test variables, do with cases for nation tie in as you see fit.
    // Not sure if you want to limit to 1 infrastructure per tile or be able to track specific objects for their levels.
    // If so could use your level variable an add 1 when spawned and 1 when upgraded

    public void AddObject(ObjectType objectType)
    {
         switch(objectType)
         {
         case ObjectType.Lumbermill:
                Lumbermill_Level++;
                lumbermill_Amount += 1;
         break;

         case ObjectType.Factory:
                Debug.Log("Tera Factory Added");
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
        Avaliable_Woodland += treeYield;
        this.Occupiant_Nation.Woodland_Count += treeYield;
        //Testing as using short hand getters and setters, so serialize field doesnt show in inspector
        Debug.Log(Occupiant_Nation.Nation_Name + "Has unprocessed wood stockpile of: " + Avaliable_Woodland + "Giga tons");
          
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
