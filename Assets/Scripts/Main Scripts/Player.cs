using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Player_Data Attached_Player_Data;

    [SerializeField] Nation[] All_Nations;

    #region Currencies, Stockpile and Logistics init
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space]
    [Header("DEBUG")]

    public int Money;       //in dollars //in millions
    public int Political_Power;

    public int Timber;        //in tons
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
        this.Timber = Attached_Player_Data.Timber;
        this.Pykerete = Attached_Player_Data.Pykerete;


        GameObject.Find("(!)Date & Time System").GetComponent<Date_and_Time_System>().Month_Pass_Event.AddListener(Calculate_On_Month_Pass);
    }

    #region On Month Pass Calculations
    //Calculation Values
    [Header("Generated Values")]
    private int Ship_Logistics_Coverage;
    private int Train_Logistics_Coverage;
    private int Total_Logistics_Coverage;
    private int Montly_Pykerete_Shipment;

    public void Calculate_On_Month_Pass()
    {
        //Update
        Update_Player_UI();

        //Money and Political Power
        Gain_Money_From_Country_GDP_Contribution();
        Gain_Political_Power();

        //Logistics
        Calculate_All_Logistics();
        Tranport_Pykerete_To_Antratica();

        //Check Win State
        Check_For_Win_State();
    }

    public void Update_Player_UI()
    {

    }

    public void Gain_Political_Power()
    {
        this.Political_Power += 1;
    }
    public void Gain_Money_From_Country_GDP_Contribution()
    {

    }
    public void Calculate_All_Logistics()
    {
        //Train Logistics Coverage
        //Ship Logistics Coverage
        //Total Logistics Coverage
    }
    public void Tranport_Pykerete_To_Antratica()
    {

    }

    public void Check_For_Win_State()
    {

    }
    #endregion


    public void Save()
    {
        this.Attached_Player_Data.Money = Money;
        this.Attached_Player_Data.Political_Power = Political_Power;
        this.Attached_Player_Data.Timber = Timber;
        this.Attached_Player_Data.Pykerete = Pykerete;
    }
    public void Load()
    {
        this.Money = Attached_Player_Data.Money;
        this.Political_Power = Attached_Player_Data.Political_Power;
        this.Timber = Attached_Player_Data.Timber;
        this.Pykerete = Attached_Player_Data.Pykerete;
    }

    private void Update()
    {
        Money_UI.text = this.Money.ToString();
        Political_Power_UI.text = this.Political_Power.ToString();

        Wood_UI.text = this.Timber.ToString();
        Pykerete__UI.text = this.Pykerete.ToString();



        Logistics_from_Train_UI.text = this.Train_Logistics_Coverage.ToString();
        Logistics_from_Ship_UI.text = this.Ship_Logistics_Coverage.ToString();
        //Logistics_Provided_and_Need.text = (Pykerete/ Total_Logistics_Coverage).ToString();
        Logistics_Total_UI.text = this.Total_Logistics_Coverage.ToString();


    }




}
