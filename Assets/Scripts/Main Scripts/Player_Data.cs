using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Player_Data : ScriptableObject
{
    #region Currencies, Stockpile and Logistics init
    //Currencies
    public int Money            = 100;  //in dollars //in millions
    public int Political_Power  = 10;

    //Stockpile
    public int Wood         = 0;        //in tons
    public int Pykerete     = 0;        //in tons

    //Logistics
    public int Logistics    = 0;        //in tons
    public int Logistics_Coverage = 0;  //Logistics coverage of current need (between 0% to 100%)

    #endregion
}
