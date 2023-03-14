using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nation : MonoBehaviour
{
    [Header("Nation Data")]
    [SerializeField] Nation_Data Attached_Nations_Data;
    [SerializeField] Player Player_Stockpile;
    [Space]
    [Header("Tiles")]
    //Pull tiles from Tiles -> Tile Data and give it to Nation Data 
    [SerializeField] public Tile[] Nations_Territories;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space]
    [Header("DEBUG - GIVE on New Game")]
    //Calculate from tiles and give to nation data at the start of the game
    [SerializeField] public int Woodland_Count;
    [SerializeField] public int Starting_Population;
    [SerializeField] public int Cumilative_Population_From_Territories;
    //Calculate from awareness and give to nation data at the start of the game
    [SerializeField] public int GDP_Contribution;

    //Also Tiles

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("DEBUG - TAKE on New Game")]
    [SerializeField] public string Nation_Name;
    [SerializeField] public Color32 Nation_Colour;
    [SerializeField] public int GDP;
    [SerializeField] public float Awareness;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("DEBUG - NEUTRAL on New Game")]
    [SerializeField] public int National_Pykerete_Production;
    [SerializeField] public int Lumbermill_Level;
    [SerializeField] public int Tera_Factory_Level;
    [SerializeField] public int Harbour_Level;
    [SerializeField] public int Railway_Level;


   

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    private void Start()
    {
        //Pull Nation Data
        Attached_Nations_Data.Nation_Name = Attached_Nations_Data.name;
        this.Nation_Name = Attached_Nations_Data.Nation_Name; //Sam edit: you have already named the SO's so why not just get the name of SO's and not name them all by hand? up to you
        

        this.Nation_Colour = Attached_Nations_Data.Nation_Colour;
        this.GDP = Attached_Nations_Data.GDP;
        this.Awareness = Attached_Nations_Data.Awareness;


        //Give territories its nation
        if(this.Nations_Territories != null)
        {
            for(int i = 0; i < Nations_Territories.Length; i++)
            {
                Nations_Territories[i].Occupiant_Nation = this;
            }
        }

        //Calculate Starting Woodland and Give it to Nation Data Holder and Nation
        Calculate_Starting_Woodland(); // -> Updates this and nation data


        //Calculate Population and Give it to Nation Data Holder
        Calculate_Starting_Population(); // -> Updates this and nation data

        //At the start of the game colour all territories according to the national colours.
        Colour_All_Teritories_According_to_the_Nation_Colour();

        //Calculate Starting Pops
        Calculate_Starting_Population();

        GameObject.Find("(!)Date & Time System").GetComponent<Date_and_Time_System>().Month_Pass_Event.AddListener(Calculate_On_Month_Pass);
    }

    #region On Month Pass Calculations

    public void Calculate_On_Month_Pass()
    {
        //Update 
        Calculate_Monthly_Population();
        Update_Buildings();

        //Nation Specific
        GDP_Growth_and_Shrink();
        Awareness_Growth_and_Shrink();

        //GDP Contribution
        Calculate_GDP_Contribution_and_Contribute_GDP();

        //Production
        Produce_Timber();
        //
    }

    public void Update_Buildings()
    {


        Lumbermill_Level = 0;
        Tera_Factory_Level = 0;
        Harbour_Level = 0;
        Railway_Level = 0;


        for (int i = 0; i < this.Nations_Territories.Length; i++)
        {
            Lumbermill_Level += Nations_Territories[i].Lumbermill_Level;
            Tera_Factory_Level += Nations_Territories[i].Tera_Factory_Level;
            Harbour_Level += Nations_Territories[i].Harbour_Level;
            Railway_Level += Nations_Territories[i].Railway_Level;
        }
    }

    public void GDP_Growth_and_Shrink()
    {
        //GDP Grows
        if(this.Cumilative_Population_From_Territories > Starting_Population)
        {
            this.GDP += (int)(this.GDP * 0.01f);
        }


        //GDP Shrinks

        //  Pop Effect
        if (this.Cumilative_Population_From_Territories < Starting_Population) //Population Shrinks, Society Starts to Collapse
        {
            this.GDP -= (int)(this.GDP * 0.01f);
        }
        if (this.Cumilative_Population_From_Territories < Starting_Population/2) //Famine and Disaster Society Collapses
        {
            this.GDP -= (int)(this.GDP * 0.025f);
        }
        if (this.Cumilative_Population_From_Territories < Starting_Population / 5) //Total Population Collapse
        {
            this.GDP -= (int)(this.GDP * 0.1f);
        }

        //Heat Level Effect
        //Calculate Avarage Heat Level on Territories
        float Avarage_Heat_Level_On_Teritories = 0;

        for(int i = 0; i < this.Nations_Territories.Length; i++)
        {
            Avarage_Heat_Level_On_Teritories += Nations_Territories[i].Average_Heat_Level;
        }
        Avarage_Heat_Level_On_Teritories = Avarage_Heat_Level_On_Teritories / Nations_Territories.Length;


        if (Avarage_Heat_Level_On_Teritories > 23)  //High
        {
            this.GDP -= (int)(this.GDP * 0.01f);
        }
        if (Avarage_Heat_Level_On_Teritories > 23)  //Very High
        {
            this.GDP -= (int)(this.GDP * 0.025f);
        }
            if (Avarage_Heat_Level_On_Teritories > 25)//Unhabitable
        {
            this.GDP -= (int)(this.GDP * 0.05f);
        }
    }

    public void Awareness_Growth_and_Shrink()
    {
        //  Pop Effect
        if (this.Cumilative_Population_From_Territories < Starting_Population) //Population Shrinks, Society Starts to Collapse
        {
            this.Awareness += 0.25f;
        }
        if (this.Cumilative_Population_From_Territories < Starting_Population / 2) //Famine and Disaster Society Collapses
        {
            this.Awareness += 0.5f;
        }
        if (this.Cumilative_Population_From_Territories < Starting_Population / 5) //Total Population Collapse
        {
            this.Awareness += 1f;
        }

        //Heat Level Effect
        //Calculate Avarage Heat Level on Territories
        float Avarage_Heat_Level_On_Teritories = 0;

        for (int i = 0; i < this.Nations_Territories.Length; i++)
        {
            Avarage_Heat_Level_On_Teritories += Nations_Territories[i].Average_Heat_Level;
        }
        Avarage_Heat_Level_On_Teritories = Avarage_Heat_Level_On_Teritories / Nations_Territories.Length;

        if (Avarage_Heat_Level_On_Teritories > 13)  //Low
        {
            this.Awareness += 0.005f;
        }
        if (Avarage_Heat_Level_On_Teritories > 16)  //Medium
        {
            this.Awareness += 0.01f;
        }
        if (Avarage_Heat_Level_On_Teritories > 20)  //High
        {
            this.Awareness += 0.05f;
        }
        if (Avarage_Heat_Level_On_Teritories > 23)  //Very High
        {
            this.Awareness += 0.25f;
        }
        if (Avarage_Heat_Level_On_Teritories > 25)//Unhabitable
        {
            this.Awareness -= 0.5f;
        }
    }
    public void Calculate_GDP_Contribution_and_Contribute_GDP()
    {
        GDP_Contribution = (int)(GDP * Awareness);
        Player_Stockpile.Money += GDP_Contribution;
    }


    public void Produce_Timber()
    {
        if (this.Lumbermill_Level != 0 && this.Woodland_Count > 0)
        {
            //How much wood is required per level
            int Woodland_Process_Capacity_per_Level = 1000;

            //Wood to Timber conversion rate (in tons)
            float Conversion_Rate = 0.5f;

            int Nations_Monthly_Woodland_Process_Capacity = Lumbermill_Level * Woodland_Process_Capacity_per_Level;

            if(this.Woodland_Count > Nations_Monthly_Woodland_Process_Capacity)
            {
                Woodland_Count = Woodland_Count - Nations_Monthly_Woodland_Process_Capacity;

                Player_Stockpile.Timber += (int)(Nations_Monthly_Woodland_Process_Capacity * Conversion_Rate);
            }
            else
            {
                Player_Stockpile.Timber += (int)(Woodland_Count * Conversion_Rate);
            }

        }

    }

    public void Calculate_Monthly_Population()
    {
        for(int i = 0; i < this.Nations_Territories.Length; i++)
        {
            this.Cumilative_Population_From_Territories = Nations_Territories[i].Population;
        }

    }

    public void Colour_All_Teritories_According_to_the_Nation_Colour()
    {
        //Colour all tiles according to the national colour
        for (int i = 0; i < Nations_Territories.Length; i++)
        {
            try
            {
                Nations_Territories[i].GetComponent<Renderer>().material.color = this.Nation_Colour;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public void Change_Awareness_According_to_the_Nation_Pop_Heat_Levels_And_GDP()
    {

    }
    #endregion

    #region Starting Calculations
    public void Calculate_Starting_Woodland()
    {
        if (this.Nations_Territories != null)
        {
            for (int i = 0; i < Nations_Territories.Length; i++)
            {
                this.Woodland_Count += Nations_Territories[i].Avaliable_Woodland;
                this.Attached_Nations_Data.Woodland_Count += Nations_Territories[i].Avaliable_Woodland;
            }
        }
    }

    public void Calculate_Starting_Population()
    {
        if (this.Nations_Territories != null)
        {
            for (int i = 0; i < Nations_Territories.Length; i++)
            {
                this.Starting_Population += Nations_Territories[i].Population;
                this.Attached_Nations_Data.Starting_Population += Nations_Territories[i].Population;
            }
        }
    }
    #endregion


    public void Save()
    {
        //Save Data
        this.Attached_Nations_Data.Woodland_Count = this.Woodland_Count;
        this.Attached_Nations_Data.GDP = this.GDP;
        this.Attached_Nations_Data.Awareness = this.Awareness;

        this.Attached_Nations_Data.Lumbermill_Level = this.Lumbermill_Level;
        this.Attached_Nations_Data.Lumbermill_Level = this.Tera_Factory_Level;
        this.Attached_Nations_Data.Lumbermill_Level = this.Harbour_Level;
        this.Attached_Nations_Data.Lumbermill_Level = this.Railway_Level;


        //Calculate Starting Woodland and Give it to Nation Data Holder and Nation
        Calculate_Starting_Woodland(); // -> Updates this and nation data


        //Calculate Population and Give it to Nation Data Holder
        Calculate_Starting_Population(); // -> Updates this and nation data

        //At the start of the game colour all territories according to the national colours.
        Colour_All_Teritories_According_to_the_Nation_Colour();

        //Calculate Starting Pops
        Calculate_Starting_Population();
    }

    public void Load()
    {
        this.Woodland_Count = this.Attached_Nations_Data.Woodland_Count;
        this.GDP = this.Attached_Nations_Data.GDP;
        this.Awareness = this.Attached_Nations_Data.Awareness;

        this.Lumbermill_Level = this.Attached_Nations_Data.Lumbermill_Level;
        this.Lumbermill_Level = this.Attached_Nations_Data.Tera_Factory_Level;
        this.Lumbermill_Level = this.Attached_Nations_Data.Harbour_Level;
        this.Lumbermill_Level = this.Attached_Nations_Data.Railway_Level;
    }
}
