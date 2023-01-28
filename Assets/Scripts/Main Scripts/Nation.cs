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
    [SerializeField] public int LEVEL_1_Lumbermill_Factory_Count;
    [SerializeField] public int LEVEL_2_Lumbermill_Factory_Count;
    [SerializeField] public int LEVEL_3_Lumbermill_Factory_Count;
    [SerializeField] public int LEVEL_1_Tera_Factory_Count;
    [SerializeField] public int LEVEL_2_Tera_Factory_Count;
    [SerializeField] public int LEVEL_3_Tera_Factory_Count;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    private void Start()
    {
        //Pull Nation Data
        this.Nation_Name = Attached_Nations_Data.Nation_Name;
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
    }

    #region On Month Pass Calculations
    public void Produce_Pykerete()
    {
        //Produce pykerete from nation data properities

        //Update game state
    }

    public void Produce_Timber()
    {
        //Produce timber from nation data properities

        //Update game state
    }

    public void Calculate_Monthly_Population()
    {
        //Calculate pop from game state

        //Update this.

        //Update nation data statistics

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
}
