using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    

    //Sam addition Singleton decleration example (shorthand): You will be able to call a public function in the script from anywhere with Player.instance.functionName/variableName
    public static Player instance => m_instance;
    private static Player m_instance; //This needs to be initialised as this within Start(): m_instance = this;    


    [SerializeField] Player_Data Attached_Player_Data;

    [SerializeField] Nation[] All_Nations;
    [SerializeField] Tile Antartica_Tile;

    #region Currencies, Stockpile and Logistics init
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space]
    [Header("DEBUG")]

    public int Money;       //in dollars //in millions
    public int Political_Power;

    public int Timber;        //in tons
    public int Pykerete {get; private set;} //Sam access modifier: strictly Public variables scare me    //in tons


    public double Monthly_Heat_Level_Increase;

    public int Ships;
    public int Trains;

    public int Transported_Timber_Waiting_To_Be_Processed;

    //Sam add: should not need to serailize as with my object placement system, it should detect on reload
    public int numberOfDocks;
    public int numberOfTrainStations;

    private int maxShipIncrement = 5;
    private int maxTrainIncrement = 2;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion


    #region UI Links
    
    [Header("UI Connections")]
    [SerializeField] TMP_Text Money_UI;
    [SerializeField] TMP_Text Political_Power_UI;

    [SerializeField] TMP_Text Wood_UI;
    [SerializeField] TMP_Text Pykerete_UI;

    [SerializeField] TMP_Text Logistics_from_Train_UI;
    [SerializeField] TMP_Text Logistics_from_Ship_UI;
    [SerializeField] TMP_Text Logistics_Provided_and_Need;
    [SerializeField] TMP_Text Logistics_Total_UI;

    [SerializeField] TMP_Text Train_Count_UI;
    [SerializeField] TMP_Text Ship_Count_UI;
    [SerializeField] Button Purcase_Ship_UI_Button;
    [SerializeField] Button Purcase_Train_UI_Button;

    #region On Month Pass Calculations
     //Calculation Values
    
    public int Logistics_Capacity_in_tons_from_Ships {get; private set;} //Sam edit: adding getter setter modifiers to access values
    public int Logistics_Capacity_in_tons_from_Trains {get; private set;} //Sam edit: adding getter setter modifiers to access values
    private int Ships_Logistics_Coverage; //Percentage slice from the 100% logistics coverage for ships.
    private int Trains_Logistics_Coverage; //Percentage slice from the 100% logistics coverage for trains.
    public int Total_Logistics_Capacity {get; private set;} //Total monthly shipment capacity in tons.
    private int Logistics_Coverage_For_Timber_Shipment; //Which percentage of timber produced can be send monthly to antratica.
    #endregion

    private void Start()
    {
       

        if(!MenuData.GetGameType()) //Sam edit: IS A LOAD GAME: instantiate local variables from player data json save file
        {
            Load();
        }

        else
        {    
            //Sam: IS A NEW GAME: Can use SO values to easily set variables in inspector for debug puproses
            
            this.Money = Attached_Player_Data.Money;
            this.Political_Power = Attached_Player_Data.Political_Power;
            this.Timber = Attached_Player_Data.Timber;
            this.Pykerete = Attached_Player_Data.Pykerete;
            this.Ships = Attached_Player_Data.Ships;
            this.Trains = Attached_Player_Data.Trains;
            this.Monthly_Heat_Level_Increase = Attached_Player_Data.Monthly_Heat_Level_Increase;
            this.Transported_Timber_Waiting_To_Be_Processed = Attached_Player_Data.Transported_Timber_Waiting_To_Be_Processed;
             
        }

        Date_and_Time_System.instance.Month_Pass_Event.AddListener(Calculate_On_Month_Pass);
    }
    //Button Load_Button;
   // Button Save_Button;

    private void Awake()
    {
         //Sam addition: initialize singleton as this instance of script (singleton as in the name (single), can only have one instance, but this script seems to just be a manager, and deffo suits use case of singleton pattern)
        m_instance = this;

        Purcase_Ship_UI_Button.onClick.AddListener(Purchase_Ship);
        Purcase_Train_UI_Button.onClick.AddListener(Purchase_Train);
    }
    
   

    public void Calculate_On_Month_Pass()
    {
        //Update
        Calculate_Timber_Gain_Rate();
        Set_Boundaries();


        //Political Power
        Gain_Political_Power();

        //Logistics
        Calculate_All_Logistics();
        Tranport_Timber_To_Antratica();
        Produce_Pykerete();

        //Check Win State
        Check_Win_State();

    }
    
    private void Purchase_Ship()
    {
        if(this.Money > 1000 && Ships < maxShipIncrement)
        {
            this.Money -= 1000;
            this.Ships++;
            NodeManager.instance.Add_Ship(); //Changed reference to singleton
            AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.transportationPurchased, null); //Sam: trigger purchase audio
        }
        
        //Sam addition: adding to tooltip to show message when player does not have enough funds
        if (Money < 1000 && Ships < maxShipIncrement)
        {
            UIHoverManager.instance.ShowTip("Insufficient Funds!", Input.mousePosition);
            AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.cantPurchase, null);
        }

        else if(Ships >= maxShipIncrement)
        {
            UIHoverManager.instance.ShowTip("More Docks Required!", Input.mousePosition);
            AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.cantPurchase, null);
        }
    }


    private void Purchase_Train()
    {
        if (this.Money > 50 && Trains < maxTrainIncrement )
        {
            this.Money -= 50;
            this.Trains++;
            NodeManager.instance.Add_Train(); //Sam: changed to use singleton reference 
            AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.transportationPurchased, null); //Sam: trigger purchase audio
        }

         //Sam addition: adding to tooltip to show message when player does not have enough funds
        if(Money < 50 && Trains < maxTrainIncrement)
        {
            UIHoverManager.instance.ShowTip("Insufficient Funds!", Input.mousePosition);
            AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.cantPurchase, null);
        }

        else if(Trains >= maxTrainIncrement)
        {
            UIHoverManager.instance.ShowTip("More Train Stations Required!", Input.mousePosition);
            AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.cantPurchase, null);
        }
    }

    [NonSerialized] private int Last_Months_Timber = 0;
    [NonSerialized] private int Timber_Gained_Lost_Between_Months = 0;
    [NonSerialized] private bool Logistics_Cover_All_Timber_Transports;
    public void Calculate_Timber_Gain_Rate()
    {
        Timber_Gained_Lost_Between_Months = Timber - Last_Months_Timber;
        Last_Months_Timber = Timber;

        if(Timber_Gained_Lost_Between_Months < 0)
        {
            Logistics_Cover_All_Timber_Transports = true;
        }
    }

    public void Gain_Political_Power()
    {
        this.Political_Power += 1;
    }

    private int Total_Harbour_Levels;
    private int Total_Railway_Levels;

    public void Calculate_All_Logistics()
    {
        Total_Harbour_Levels = 1;
        Total_Railway_Levels = 1;

        //Total Buildings in the Chain
        for (int i = 0; i < All_Nations.Length; i++)
        {
            Total_Harbour_Levels += All_Nations[i].Harbour_Level;
            Total_Railway_Levels += All_Nations[i].Railway_Level;
        }

        //Vehicle and Building Contribution Values
        int Logistics_Increase_per_Train = 1; //in tons
        int Logistics_Increase_per_Ship = 50;
        float Logistics_Multiplayer_for_Ship_per_Harbour = 1.005f; // 0.5%
        float Logistics_Multiplayer_for_Train_per_Railway = 1.005f; // 0.5%


        //Train Logistics Coverage
        Logistics_Capacity_in_tons_from_Trains = (int)((Total_Railway_Levels * Logistics_Multiplayer_for_Train_per_Railway) * Trains * Logistics_Increase_per_Train); 

        //Ship Logistics Coverage
        Logistics_Capacity_in_tons_from_Ships = (int)((Total_Harbour_Levels * Logistics_Multiplayer_for_Ship_per_Harbour) * (Ships * Logistics_Increase_per_Ship));

        //Total Logistics Coverage
        Total_Logistics_Capacity = Logistics_Capacity_in_tons_from_Trains + Logistics_Capacity_in_tons_from_Ships;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Balance between ship and train
        /*
        1-	Because Pykrete must be transported to the Antarctica by ships after the inland transportation, at least 80% of the total logistic capacity must be provided by ships.
                a.	If the designated limit is not reached, player loses 0 to 100% of its logistics level from 80% to 0% capacity of ships.
        2-	Because pykrete and woods must be transported inland to the ships and harbors, at least 20% of the total logistic level must be provided by trains and trucks.
                a.	If the designated limit is not reached, player loses 0 to 50% of its logistics level from 20% to 0% capacity of planes and ships.

        -> So if the player is 100% percent preceise about the distribution of the logistical assets there will be timber and logistics capacity lost to the logistical simmulation.
        */

  
       
        int Hundred_Percent_of_Logistical_Capacity = Logistics_Capacity_in_tons_from_Trains + Logistics_Capacity_in_tons_from_Ships; // 100%
        if(Hundred_Percent_of_Logistical_Capacity != 0)
        {
            Trains_Logistics_Coverage = 100 * Logistics_Capacity_in_tons_from_Trains / Hundred_Percent_of_Logistical_Capacity;
            Ships_Logistics_Coverage = 100 * Logistics_Capacity_in_tons_from_Ships / Hundred_Percent_of_Logistical_Capacity;
        }


        int Logistics_Inefficiency_from_Trains = 100;
        int Logistics_Inefficiency_from_Ships = 100;
        if (Trains_Logistics_Coverage <= 20)
        {
            // 20 = 100%
            Logistics_Inefficiency_from_Trains = 20 * Trains_Logistics_Coverage / 50; 
            //Debug.Log("Percentage effect from inneficieny of the trains : " + Logistics_Inefficiency_from_Trains);

        }

        if (Ships_Logistics_Coverage <= 80)
        {
            // 80 = 100%
            Logistics_Inefficiency_from_Ships = 80 * Ships_Logistics_Coverage / 100;
            //Debug.Log("Percentage effect from inneficieny of the ships : " + Logistics_Inefficiency_from_Ships);
        }

        //Inefficiency effect from wrong logistical planning. Reduced from total logistics capacity as a percentile.

        Total_Logistics_Capacity = Total_Logistics_Capacity * Logistics_Inefficiency_from_Trains / 100;
        Total_Logistics_Capacity = Total_Logistics_Capacity * Logistics_Inefficiency_from_Ships / 100;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //How much logistics cover produced timber shipment monthly
        if (Logistics_Cover_All_Timber_Transports || Timber_Gained_Lost_Between_Months < 0) { Logistics_Coverage_For_Timber_Shipment = 100; } //100%

        if (Timber_Gained_Lost_Between_Months > 0)
        {
            Logistics_Coverage_For_Timber_Shipment = Total_Logistics_Capacity * 100 / Timber_Gained_Lost_Between_Months; //Range reduced between 0% to 100%

        }


        //UI Updates
        Logistics_from_Train_UI.text = this.Trains_Logistics_Coverage.ToString() + "%";
        Logistics_from_Ship_UI.text = this.Ships_Logistics_Coverage.ToString() + "%";
        Logistics_Provided_and_Need.text = this.Logistics_Coverage_For_Timber_Shipment.ToString() + "%";
        Logistics_Total_UI.text = this.Total_Logistics_Capacity.ToString();

    }

    public int Monthly_Timber_Send;
    public void Tranport_Timber_To_Antratica()
    {
        
        if (Timber > 0)
        {
            if (Total_Logistics_Capacity <= Timber)
            {
                this.Timber -= Total_Logistics_Capacity;
                Monthly_Timber_Send = Total_Logistics_Capacity;
            }
            else
            {
                Monthly_Timber_Send = Timber;
                this.Timber -= Monthly_Timber_Send;
            }
        }

        if(Timber < 0)
        {
            Timber = 0;
        }
        Transported_Timber_Waiting_To_Be_Processed += Monthly_Timber_Send;


    }

    public void Produce_Pykerete()
    {
        int Monthly_Pykerete_Production_per_Tera_Factory_Count = 100;

        //Tera factory count
        int Pykerete_Production_Capacity = Antartica_Tile.Tera_Factory_Level * Monthly_Pykerete_Production_per_Tera_Factory_Count;
/*        Debug.Log("Produce Pykerete Function");*/
        if(Transported_Timber_Waiting_To_Be_Processed > 0  && Pykerete_Production_Capacity < Transported_Timber_Waiting_To_Be_Processed)
        {
            this.Pykerete += Pykerete_Production_Capacity;
            Transported_Timber_Waiting_To_Be_Processed -= Pykerete_Production_Capacity;
/*            Debug.Log("if 1");
            Debug.Log("Pykerete production capacity : " + Pykerete_Production_Capacity);*/
        }
        else if(Transported_Timber_Waiting_To_Be_Processed > 0 && Pykerete_Production_Capacity >= Transported_Timber_Waiting_To_Be_Processed)
        {
            this.Pykerete += Transported_Timber_Waiting_To_Be_Processed;
            Transported_Timber_Waiting_To_Be_Processed = 0;
/*            Debug.Log("if 2");*/
        }
    }

    private void Check_Win_State()
    {
        //Sam addition: just wanted to implement win / loss state, feel free to change values.
        if (Monthly_Heat_Level_Increase <= 0)
        {
            //Win State
            GameManager.instance.GameWon();
            

        }

        else if (Monthly_Heat_Level_Increase > 0.1)
        {
           
           GameManager.instance.GameLost();
           
        }
    }

    #endregion


    public void Save()
    {   //Sam: overhall of saving sytem, utilising JSON instead of SO serialization
        SavePlayerData player = new SavePlayerData();
        
        player.money = Money;
        player.politicalPower = Political_Power;
        player.timber = Timber;
        player.pykrete = Pykerete;
        player.ships = Ships;
        player.trains = Trains;
        player.monthlyHeatLevelIncrease = Monthly_Heat_Level_Increase;
        player.trasportatedTimberWaitingToBeProcessed = Transported_Timber_Waiting_To_Be_Processed;

        JSONManager.SavePlayerJSON(player, "PlayerData");
    }
    public void Load()
    {
        //Sam: deserializing save file into local variables
        SavePlayerData player = JSONManager.LoadPlayerData("PlayerData");

        this.Money = player.money;
        this.Political_Power = player.politicalPower;
        this.Timber = player.timber;
        this.Pykerete = player.pykrete;
        this.Ships =  player.ships;
        this.Trains =  player.trains;
        this.Monthly_Heat_Level_Increase =  player.monthlyHeatLevelIncrease;
        this.Transported_Timber_Waiting_To_Be_Processed =  player.trasportatedTimberWaitingToBeProcessed;
        
        //Sam: Cheesy way of serializing your boat and ship system, just get the number of ships and trains from player file and call respective 
        //add function that many times 
        for (int i = 0; i < Trains; i++)
        {
            NodeManager.instance.Add_Train();
        }

        for (int i = 0; i < Ships; i++)
        {
            NodeManager.instance.Add_Ship();
        }
    }

    private void Set_Boundaries()
    {
        if(Money < 0) { Money = 0; }
        if (Political_Power < 0) { Political_Power = 0; }
        if (Timber < 0) { Timber = 0; }
        if (Ships < 0) { Ships = 0; }
        if (Trains < 0) { Trains = 0; }
        if (Pykerete < 0) { Pykerete = 0; }
    }



    private void Update()
    {
        Money_UI.text = this.Money.ToString();
        Political_Power_UI.text = this.Political_Power.ToString();

        Wood_UI.text = this.Timber.ToString();
        Pykerete_UI.text = this.Pykerete.ToString();

        Train_Count_UI.text = this.Trains.ToString();
        Ship_Count_UI.text = this.Ships.ToString();
    }
    
//////////////////////////////////////////////////////////  Sam Addition: Getters / setters for variables within this script ///////////////////////////////////
    //Sam addition: set pykrete access to only be writeable within this scipt, to avoid bad memory management + potential data leaks. 
    //Therefore this function exists to get pykrete number + made class singleton so i can access this function without reference to script
    public int GetPkyreteStockPile()
    {
        int _pykrete = Pykerete;

        return _pykrete;
    }

    public int GetTimberStockPile()
    {
        int _timber = Timber;

        return _timber;
    }

    public int GetPlayerWealth()
    {
        int _wealth = Money;

        return _wealth;
    }

    public float GetMonthlyHeatRise()
    {
        float _monthlyHeatRise = (float) Monthly_Heat_Level_Increase;

        return _monthlyHeatRise;
    }

    public void RemoveAmountFromTimber(int amount)
    { 
        Timber -= amount;
    }


    public void RemoveAmountFromPykereteStockPile(int amount)
    {
        Pykerete -= amount;
    }

    public void RemoveAmountFromPlayerWealth(int amount)
    {
        Money -= amount;
    }

    public void MinusFromMonthlyHeatLevel(double value)
    {
        Monthly_Heat_Level_Increase -= (float) value;
    }

    public void DockPlaced()
    {
        maxShipIncrement += maxShipIncrement;
    }

    public void TrainStationPlaced()
    {
        maxTrainIncrement += maxTrainIncrement;
    }

    void OnDestory()
    {
        Date_and_Time_System.instance.Month_Pass_Event.RemoveListener(Calculate_On_Month_Pass);
    }
    

}

//Sam addition: none of the scriptable object stuff is serializing in build, having to add json
[System.Serializable]
public class SavePlayerData
{
    public int money;
    public int politicalPower;
    public int timber;
    public int pykrete;
    public int ships;
    public int trains;
    public double monthlyHeatLevelIncrease;
    public int trasportatedTimberWaitingToBeProcessed;

}
