using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Player_Data Attached_Player_Data;

    #region Currencies, Stockpile and Logistics init
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space]
    [Header("DEBUG")]

    public int Money;       //in dollars //in millions
    public int Political_Power;

    public int Wood;        //in tons
    public int Pykerete;    //in tons

    public int Logistics;   //in tons
    public int Logistics_Coverage; //Logistics coverage of current need (between 0% to 100%)

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion


    #region UI Links
    //UI links
    #endregion

    private void Start()
    {
        //Pull Starting Data from Player Data
        this.Money = Attached_Player_Data.Money;
        this.Political_Power = Attached_Player_Data.Political_Power;
        this.Wood = Attached_Player_Data.Wood;
        this.Pykerete = Attached_Player_Data.Pykerete;
        this.Logistics = Attached_Player_Data.Logistics;
        this.Logistics_Coverage = Attached_Player_Data.Logistics_Coverage;
    }


}
