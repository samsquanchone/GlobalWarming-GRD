using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance => m_instance;
    private static UIManager m_instance;

    private GameObject inspectedObject;

    //Canvas's////
    [SerializeField] private GameObject countryPanel;


    //PLayerUI////
    [SerializeField]
    private TMP_Text playerWealthText;


    //Country UI///

    //Text
    [SerializeField] private TMP_Text countryName;
    [SerializeField] private TMP_Text countryGDP;
    [SerializeField] private TMP_Text countryPopulation;
    [SerializeField] private TMP_Text countryCo2Production;
    [SerializeField] private TMP_Text countryPykreteProduction;

    [SerializeField] private TMP_Text countryGDPContribution;

    //Slider
    [SerializeField] private Slider contributionSlider;

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        countryPanel.SetActive(false);
    }

    void Update()
    {
        //Set total wealth UI text
        playerWealthText.text = "UN Wealth: $" + PersistentManagerScript.instance.GetWealth().ToString();
    }

    //Set country UI Canvas Element
    public void DisplayCountryData(string name, double gdp, double population, float tonsOfCo2Produced, float amountOfPykreteProduced, double gdpContribution, float percentGDPContributed)
    {
        //Set country UI panel to active, then set UI values based off of what was passed from CountryData from clicked mesh
        countryPanel.SetActive(true);
        countryName.text = name;
        countryGDP.text = "GDP: $" + gdp.ToString();
        countryPopulation.text = "Population: " + population.ToString();
        countryCo2Production.text = "Co2 Production (Tons): " + tonsOfCo2Produced.ToString();
        countryPykreteProduction.text = "Pykrete Production (KG): " + amountOfPykreteProduced.ToString();
        countryGDPContribution.text = "Current Contribution: $" + string.Format("{0:0.00}", gdpContribution);// gdpContribution.ToString();

        contributionSlider.value = percentGDPContributed;
    }

    public void UpdateCountryContributionUI()
    {
        if (inspectedObject != null)
        {
            //Update contribution value when slider is changed (Trigger from slider UI inspector value change )
            countryGDPContribution.text = "Current Contribution: $" + string.Format("{0:0.00}", inspectedObject.GetComponent<CountryData>().GetGDPContribution().ToString());

        }
    }

    public void DisableCountryUI()
    {
        //Called when X button is pressed on top right of country UI Panel 
        countryPanel.SetActive(false);
    }

    public void SetInspected(GameObject gameObject)
    {
        //Set's local gameObject to object that has been pressed (called in MouseClicked.cs)
        inspectedObject = gameObject;

    }

    public void SetInspectedContribution()
    {

        //On slider value change set the percentage variable of CountryData script attached to the country's gameObject that has been selected
        inspectedObject.GetComponent<CountryData>().percentageGDPContributed = contributionSlider.value;


    }


}
