using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Currencies, Stockpile and Logistics init
    //Currencies
    public int Money;       //in dollars //in millions
    public int Political_Power;

    //Stockpile
    public int Wood;        //in tons
    public int Pykerete;    //in tons

    //Logistics
    public int Logistics;   //in tons
    public int Logistics_Coverage; //Logistics coverage of current need (between 0% to 100%)

    #endregion


    #region UI Links
    //UI links


    #endregion

    private void Start()
    {
        //Starting Resources of the Player
        //Starting Currencies
        this.Money = 100;
        this.Political_Power = 10;

        //Starting Stockpile
        this.Wood = 100;
        this.Pykerete = 0;

        //Starting Logistics Capacity
        this.Logistics = 0;
    }


    private void Update()
    {

    }
}
