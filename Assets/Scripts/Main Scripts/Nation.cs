using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nation : MonoBehaviour
{
    [Header("Nation Data")]
    [SerializeField] Nation_Data Attached_Nations_Data;
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

        //Give Nation Data its territories
        Attached_Nations_Data.Nations_Territories = this.Nations_Territories;

        //Give territories its nation
        if(this.Attached_Nations_Data.Nations_Territories != null)
        {
            for(int i = 0; i < Attached_Nations_Data.Nations_Territories.Length; i++)
            {
                Attached_Nations_Data.Nations_Territories[i].Occupiant_Nation = this;
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
        //Nation Specific
        GDP_Growth_and_Shrink();
        Calculate_Monthly_Population();
        Awareness_Growth_and_Shrink();

        //Production
        Produce_Pykerete();
        Produce_Timber();
        //
    }



    public void GDP_Growth_and_Shrink()
    {

    }

    public void Awareness_Growth_and_Shrink()
    {

    }


    public void Produce_Pykerete()
    {

    }

    public void Produce_Timber()
    {

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
