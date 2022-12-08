using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryData : MonoBehaviour
{
    //This class contains variables for a country, and will be attached to each country, with variables assigned independantly for each country

    public string name;
    public double gdp;
    public double population;
    public float tonsOfCo2Produced;
    public float amountOfPykreteProduced;
    public float percentageGDPContributed;


    public double GetGDPContribution()
    {
        double currentContribution = gdp / percentageGDPContributed; //Get percentage alocated from GDP

        Debug.Log("Current Contribution: " + name + ": " + currentContribution);
        return currentContribution;
    }

}
