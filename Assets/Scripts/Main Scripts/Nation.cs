using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nation : MonoBehaviour
{
    [SerializeField] public string Nation_Name;

    //Nation's Tiles
    [SerializeField] public Tile[] Nations_Territories;

    //Nation Color
    [SerializeField] public Color32 Nation_Colour;

    //GDP
    [SerializeField] public int GDP; //Each nation has its starting GDP tied to its cumulative population from the nation’s tiles. If nothing happens, nation GDP’s increases because of the steady increase of the population of its tiles.

    //Population
    [SerializeField] public int Cumilative_Population_From_Territories; //Nations does not affect populations, but populations effect the nation by increasing/decreasing its GDP and Awareness.

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


    private void Start()
    {
        //At the start of the game colour all territories according to the national colours.
        Colour_All_Teritories_According_to_the_Nation_Colour();
    }

    public void Produce_Pykerete()
    {
        
    }


    public void Colour_All_Teritories_According_to_the_Nation_Colour()
    {
        int Randomness_For_Colour = Random.Range(-100, +100);
        //Colour all tiles according to the national colour
        for (int i = 0; i < Nations_Territories.Length; i++)
        {
            try
            {
                Nations_Territories[i].GetComponent<Renderer>().material.color = this.Nation_Colour;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public void Change_Awareness_According_to_the_Nation()
    {

    }
}
