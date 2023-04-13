using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Player_Data : ScriptableObject
{

    //Currencies
    public int Money            = 100;  //in dollars //in millions
    public int Political_Power  = 10;

    //Stockpile
    public int Timber = 0;        //in tons
    public int Pykerete     = 0;        //in tons
    public int Transported_Timber_Waiting_To_Be_Processed = 0;

    //Logistics
    public int Logistics    = 0;        //in tons
    public int Logistics_Coverage = 0;  //Logistics coverage of current need (between 0% to 100%)

    //Vehicles
    public int Trains = 0;    
    public int Ships = 0;


    //Heat Level Increase
    public float Monthly_Heat_Level_Increase = 0.0012f;

  
}
