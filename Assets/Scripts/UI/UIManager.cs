using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;






public class UIManager : MonoBehaviour
{   //Singleton
    public static UIManager instance { get; private set; }

    
    //Panels///
    [SerializeField] private GameObject objectPanel;
    [SerializeField] private GameObject pykreteFactoryPanel;

    //Images //
    [SerializeField] private Image objectImage;
 
    //Buttons//
    [SerializeField] private Button objectActionButton;

    [SerializeField] private Button factoryActionButton1;
    [SerializeField] private Button factoryActionButton2;
    [SerializeField] private Button factoryActionButton3;

    //Button Text//
    [SerializeField] private TMP_Text buttonText;

    //Text For infrastucture and tree panels NOT PYKRETE FACTORY//
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private TMP_Text objectData1;
    [SerializeField] private TMP_Text objectData2;
    [SerializeField] private TMP_Text objectData3;
    [SerializeField] private TMP_Text objectData4;
    [SerializeField] private TMP_Text objectData5;

    //Panel for pykreteFactory
   
    [SerializeField] private TMP_Text pykreteFactoryData1;
    [SerializeField] private TMP_Text pykreteFactoryData2;
    [SerializeField] private TMP_Text pykreteFactoryData3;
    [SerializeField] private TMP_Text pykreteFactoryData4;
    

    int iterator = 0;

    //Used to contain pressed object, to execute button actions
    private GameObject objectInspected = null;

    int upgradeValueLumber;
    int upgradeValueTrainStation;
    int upgradeValueDock;

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
        
        //Button on click delegates, with argument for swich case for determining which button was pressed
        factoryActionButton1.onClick.AddListener(delegate {PykreteFactoryActionButtonpressed(0); });
        factoryActionButton2.onClick.AddListener(delegate {PykreteFactoryActionButtonpressed(1); });
        factoryActionButton3.onClick.AddListener(delegate {PykreteFactoryActionButtonpressed(2); });

        objectPanel.SetActive(false);
        pykreteFactoryPanel.SetActive(false);
    }

    //This is used to determine which panel to open, has been refactored instead of lodas of if statements
    public void SetObjectUI(GameObject objectToDisplay)
    {
        /*
        objectInspected = objectToDisplay.gameObject;
        objectName.text = objectToDisplay.name;
        objectData1.text = "Country Placed: " + objectToDisplay.GetComponent<ObjectNationInteraction>().nationPlaced;
        objectImage.sprite = objectToDisplay.GetComponent<Image>().sprite;
        */

        

        ObjectType objectType = objectToDisplay.GetComponent<SaveableObject>().objectType;

        objectData1.text = "Country Placed: " + objectToDisplay.GetComponent<ObjectNationInteraction>().nationPlaced;
        objectInspected = objectToDisplay;
        
        switch(objectType)
        {
            case ObjectType.Lumbermill:
            objectPanel.name = "Infrastructure";
            OpenInfrastructurePanel(objectToDisplay, 1);
            break;

            case ObjectType.Factory:
            objectPanel.name = "Infrastructure";
            OpenPykretePanelFactoryPanel(objectToDisplay);
            break;

            case ObjectType.Dock:
            objectPanel.name = "Infrastructure";
            OpenInfrastructurePanel(objectToDisplay, 2);
            break;

            case ObjectType.TrainStation:
            objectPanel.name = "Infrastructure";
            OpenInfrastructurePanel(objectToDisplay, 3);
            break;
            
            //Is a tree
            default:
            objectPanel.name = "Tree";
            OpenTreePanel(objectToDisplay);
            break;

        }
      
        
    }

    public void OpenTreePanel(GameObject gameObject)
    { 
        //Converting enum value to name for naming the object name text
        objectName.text = gameObject.GetComponent<SaveableObject>().objectType.ToString() + " Project";
        objectImage.sprite = gameObject.GetComponent<Image>().sprite;
        if(gameObject.GetComponent<TreeGrowth>().IsGrown())
        {
            objectData2.text = "Time until fully grown: READY TO HARVEST!";
            objectActionButton.gameObject.SetActive(true);
        }
            
        else
        {
            objectData2.text = "Time until fully grown: " + gameObject.GetComponent<TreeGrowth>().GetGrowthTimeRemaining() + " Months";
            objectActionButton.gameObject.SetActive(false);
        }

           
        
        objectData3.text = "Expected yield: " + gameObject.GetComponent<TreeObject>().m_yield + " Tons";

        objectData4.text = "Current growth Temperature: " + gameObject.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().Average_Heat_Level;
        buttonText.text = "Harvest";

        

        objectPanel.SetActive(true);

            
    }

    public void OpenInfrastructurePanel(GameObject gameObject, int index)
    {
        objectImage.sprite = gameObject.GetComponent<Image>().sprite;

        Tile tile = gameObject.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>(); //Get reference to tile script, get ref off a script on object that nation placed is assigned
        int level = gameObject.GetComponent<InfrastructuerUpgrade>().level; //Get upgrade level of infrastructure

        switch(index)
        {
            case 1:
            objectName.text = "Lumber Mill";
            objectData2.text = "Processed wood stockpile: " + tile.GetAvailableWoodland() + " Tons";
            objectData3.text = "Upgrade level: Level " + level;
            objectData4.text = "";
            buttonText.text = "Upgrade Lumbemill";
            break;

            case 2:
            objectName.text = "Dock";
            objectData2.text = "Processed wood stockpile: " + tile.GetAvailableWoodland() + "Tons";
            objectData3.text = "Upgrade level: Level " + level;
            objectData4.text = "Capacity: " + Player.instance.Logistics_Capacity_in_tons_from_Ships + " Tons";
            buttonText.text = "Upgrade Transport";

            break;

            case 3:
            objectName.text = "Train Station";
            objectData2.text = "Processed wood stockpile: " + tile.GetAvailableWoodland() + " Tons";
            objectData3.text = "Upgrade level: Level " + level;
            objectData4.text = "Capacity: " + Player.instance.Logistics_Capacity_in_tons_from_Trains + "Tons";
            buttonText.text = "Upgrade Transport";

            break;
        }

            objectPanel.SetActive(true);
            objectActionButton.gameObject.SetActive(true);
       
    }

    public void ObjectActionButtonPressed()
    {
        if (objectInspected.tag == "Tree")
        {
            if(objectInspected.GetComponent<TreeGrowth>().IsGrown())
            {
                objectInspected.GetComponent<TreeObject>().RemoveObject();
                DisableObjectUI();
            }

            else
            {
                Debug.Log("Tree Not Grown");
            }
        }

        else if(objectInspected.tag == "Infrastructure")
        {
            
            objectInspected.GetComponent<InfrastructuerUpgrade>().UpgradeInfrastructure();
            DisableObjectUI();
        }

       
    }

    public string InspectedObjectTag()
    {
       string tag = objectInspected.tag;

       return tag;
    }

    public void PykreteFactoryActionButtonpressed(int buttonNum)
    {
        
        
        switch(buttonNum)
        {
        
           case 0: 
           objectInspected.GetComponent<TeraFactory>().ProducePykreteBerg(0);
           break;
        
           case 1:
           objectInspected.GetComponent<TeraFactory>().ProducePykreteBerg(1);
           break;

           case 2:
           objectInspected.GetComponent<TeraFactory>().ProducePykreteBerg(2);
           break;
        }
       
        
       
    }

    public void OpenPykretePanelFactoryPanel(GameObject gameObject)
    {
        pykreteFactoryData1.text = "Placed in: Antarctica";
        pykreteFactoryData2.text = "Processed wood stockpile: \n" + Player.instance.GetPkyreteStockPile() + " Giga Tons";
        pykreteFactoryData3.text = "";
        pykreteFactoryData4.text = "";
        
        pykreteFactoryPanel.SetActive(true);
    }

    public void DisableObjectUI()
    {
        objectPanel.SetActive(false);
        pykreteFactoryPanel.SetActive(false);
        objectInspected = null;
    }


}
