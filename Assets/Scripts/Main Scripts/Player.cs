using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion


    #region UI Links
    //UI links
    [Header("UI Connections")]
    [SerializeField] TMP_Text Money_UI;
    [SerializeField] TMP_Text Political_Power_UI;

    [SerializeField] TMP_Text Wood_UI;
    [SerializeField] TMP_Text Pykerete__UI;

    [SerializeField] TMP_Text Logistics_from_Train_UI;
    [SerializeField] TMP_Text Logistics_from_Ship_UI;
    [SerializeField] TMP_Text Logistics_Provided_and_Need;
    [SerializeField] TMP_Text Logistics_Total_UI;

    #endregion

    private void Start()
    {
        //Pull Starting Data from Player Data
        this.Money = Attached_Player_Data.Money;
        this.Political_Power = Attached_Player_Data.Political_Power;
        this.Wood = Attached_Player_Data.Wood;
        this.Pykerete = Attached_Player_Data.Pykerete;
    }

    private void Update()
    {
        Money_UI.text = this.Money.ToString();
        Political_Power_UI.text = this.Political_Power.ToString();

        Wood_UI.text = this.Wood.ToString();
        Pykerete__UI.text = this.Pykerete.ToString();



        Logistics_from_Train_UI.text = this.Train_Logistics_Coverage.ToString();
        Logistics_from_Ship_UI.text = this.Ship_Logistics_Coverage.ToString();
        Logistics_Provided_and_Need.text = (Pykerete/ Total_Logistics_Coverage).ToString();
        Logistics_Total_UI.text = this.Total_Logistics_Coverage.ToString();


    }
    #region Generated Values
    //Calculation Values
    [Header("Generated Values")]
    private int Ship_Logistics_Coverage;
    private int Train_Logistics_Coverage;

    private int Total_Logistics_Coverage;



    private int Montly_Pykerete_Shipment;
    #endregion




}
