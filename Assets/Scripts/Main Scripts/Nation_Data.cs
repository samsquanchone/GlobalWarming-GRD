using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class Nation_Data : ScriptableObject
{
    [SerializeField] public string Nation_Name;

    [Header("Tiles")]
    //Nation's Tiles
    [SerializeField] public Tile[] Nations_Territories;

    [Header("Colour")]
    //Nation Color
    [SerializeField] public Color32 Nation_Colour;

    [Header("Nation Properties")]
    //GDP
    [SerializeField] public int GDP; //Each nation has its starting GDP tied to its cumulative population from the nation�s tiles. If nothing happens, nation GDP�s increases because of the steady increase of the population of its tiles.

    //Population
    [SerializeField] public int Cumilative_Population_From_Territories; //Nations does not affect populations, but populations effect the nation by increasing/decreasing its GDP and Awareness.
    [SerializeField] public int Starting_Population;

    //Awereness
    [SerializeField] public float Awareness; //From 0.0 to 1.0
    //At 1 awareness nations contribute all of their resources.

    //GDP Contribution
    [SerializeField] public int GDP_Contribution; //Current GDP * Awareness

    //Pykerete production
    [SerializeField] public int National_Pykerete_Production;

    /*Nations produce pykrete according to their GDP and Awareness. 
     * (GDP represents the industrial capacity of the nation.) 
     * Nations pull wood from wood stockpile of the player and produce pykrete.*/

    //Production Building Counts
    [Header("Production Building Counts")]
    [SerializeField] public int LEVEL_1_Lumbermill_Factory_Count;
    [SerializeField] public int LEVEL_2_Lumbermill_Factory_Count;
    [SerializeField] public int LEVEL_3_Lumbermill_Factory_Count;

    [SerializeField] public int LEVEL_1_Tera_Factory_Count;
    [SerializeField] public int LEVEL_2_Tera_Factory_Count;
    [SerializeField] public int LEVEL_3_Tera_Factory_Count;

    [Header("Country's Total Avaliable Woodland ")]
    [SerializeField] public int Woodland_Count;

}