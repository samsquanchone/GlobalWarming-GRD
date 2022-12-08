using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneReached : MonoBehaviour
{
    [SerializeField]private GameObject[] countryArray;
    private double amountToIncrement = 0;

    private void Start()
    {
        countryArray = GameObject.FindGameObjectsWithTag("UNCountry");
    }

    public void IncrementWealth()
    {
        //Reset variable
        amountToIncrement = 0;
        //Iterate over countries, adding their contribution 
        for (int i = 0; i < countryArray.Length; i++)
        {
           
                amountToIncrement += countryArray[i].GetComponent<CountryData>().GetGDPContribution();
            
        }

        //Set data manager playerWealth variable
        PersistentManagerScript.instance.AddWealth(amountToIncrement);
    }
}
   
