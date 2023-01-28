using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    private GameObject inspectedObject;

   
    //Canvas's////
    [SerializeField] public GameObject countryPanel;

    [SerializeField] private GameObject objectPanel;


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

    [SerializeField] private Image objectImage;

    [SerializeField] private Button objectActionButton;



    [SerializeField] private TMP_Text objectName;
    [SerializeField] private TMP_Text objectData1;
    [SerializeField] private TMP_Text objectData2;
    [SerializeField] private TMP_Text objectData3;
    [SerializeField] private TMP_Text objectData4;
    [SerializeField] private TMP_Text objectData5;
    


    private GameObject objectInspected = null;

    // Start is called before the first frame update
    void Start()
    {
        //Non-lazy instantiation of singleton 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        countryPanel.SetActive(false);
        objectPanel.SetActive(false);
    }

    public void SetObjectUI(GameObject objectToDisplay)
    {
        objectInspected = objectToDisplay;
        objectName.text = objectToDisplay.name;
        objectData1.text = "Country Placed: " + objectToDisplay.GetComponent<ObjectNationInteraction>().nationPlaced;


        if (objectToDisplay.tag == "Tree")
        {
            objectImage.sprite = objectToDisplay.GetComponent<Image>().sprite;
            objectData2.text = "Time until fully grown: " + objectToDisplay.GetComponent<TreeObject>().m_timeToGrow;
            objectData3.text = "Current growth conditions (Temperature): " + objectToDisplay.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().Average_Heat_Level;
            objectData4.text = "Expected yield: " + objectToDisplay.GetComponent<TreeObject>().m_yield;
            objectPanel.SetActive(true);
        }
        else
        {
            objectImage.sprite = objectToDisplay.GetComponent<Image>().sprite;
            objectPanel.SetActive(true);
        }
        
    }

    public void ObjectActionButtonPressed()
    {
        if (objectInspected.tag == "Tree")
        {
            objectInspected.GetComponent<TreeObject>().RemoveObject();
            DisableObjectUI();
        }
    }

    public void DisableObjectUI()
    {
        objectPanel.SetActive(false);
    }

    void Update()
    {
        //Set total wealth UI text
       // playerWealthText.text = "UN Wealth: $" + PersistentManagerScript.instance.GetWealth().ToString();
    }

    //Set country UI Canvas Element
    public void DisplayCountryData(string name, double gdp, double population, float tonsOfCo2Produced, float amountOfPykreteProduced, double gdpContribution, float percentGDPContributed)
    {
        //Set country UI panel to active, then set UI values based off of what was passed from CountryData from clicked mesh

        if (!countryPanel.activeSelf)
        {
            countryPanel.SetActive(true);
            countryName.text = name;
            countryGDP.text = "GDP: $" + gdp.ToString();
            countryPopulation.text = "Population: " + population.ToString();
            countryCo2Production.text = "Co2 Production (Tons): " + tonsOfCo2Produced.ToString();
            countryPykreteProduction.text = "Pykrete Production (KG): " + amountOfPykreteProduced.ToString();
            countryGDPContribution.text = "Current Contribution: $" + string.Format("{0:0.00}", gdpContribution);// gdpContribution.ToString();

            contributionSlider.value = percentGDPContributed;

        }
    }

    public void UpdateCountryContributionUI()
    {
        if (inspectedObject != null)
        {
            //Update contribution value when slider is changed (Trigger from slider UI inspector value change )
            countryGDPContribution.text = "Current Contribution: $" + inspectedObject.GetComponent<CountryData>().GetGDPContribution().ToString();

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

    public void SetInspectedContribution(float value)
    {

        //On slider value change set the percentage variable of CountryData script attached to the country's gameObject that has been selected
        inspectedObject.GetComponent<CountryData>().percentageGDPContributed = value;
        Debug.Log(inspectedObject.GetComponent<CountryData>().percentageGDPContributed);
        UpdateCountryContributionUI();
    }

   


}
