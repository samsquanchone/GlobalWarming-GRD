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
    

    

    //Used to contain pressed object, to execute button actions
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
            OpenInfrastructurePanel(objectToDisplay, 1);
            break;

            case ObjectType.Factory:
            OpenPykretePanelFactoryPanel(objectToDisplay);
            break;

            case ObjectType.Dock:
            OpenInfrastructurePanel(objectToDisplay, 2);
            break;

            case ObjectType.TrainStation:
            OpenInfrastructurePanel(objectToDisplay, 3);
            break;
            
            //Is a tree
            default:
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
            objectData2.text = "Time until fully grown: " + gameObject.GetComponent<TreeGrowth>().GetGrowthTimeRemaining();
            objectActionButton.gameObject.SetActive(false);
        }

           
        // objectData3.text = "Current growth conditions (Temperature): " + objectToDisplay.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().Average_Heat_Level;
        objectData4.text = "Expected yield: " + gameObject.GetComponent<TreeObject>().m_yield;
        buttonText.text = "Harvest";

        objectPanel.SetActive(true);

            
    }

    public void OpenInfrastructurePanel(GameObject gameObject, int index)
    {
        objectImage.sprite = gameObject.GetComponent<Image>().sprite;

        switch(index)
        {
            case 1:
            objectName.text = "Lumber Mill";
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Lumbemill";
            break;

            case 2:
            objectName.text = "Dock";
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Transport";

            break;

            case 3:
            objectName.text = "Train Station";
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Transport";

            break;
        }

            objectPanel.SetActive(true);
       
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

        if(objectInspected.tag == "Infrastructure")
        {
            
            objectInspected.GetComponent<InfrastructuerUpgrade>().UpgradeInfrastructure();
        }

        else if (objectInspected.tag == "TeraFactory")
        {
            //Spawn a pykrete berg at the selected tera factory
            objectInspected.GetComponent<TeraFactory>().ProducePykreteBerg();
        }
    }

    public void OpenPykretePanelFactoryPanel(GameObject gameObject)
    {
        pykreteFactoryData2.text = "Processed wood stockpile: " + "Tons";
        pykreteFactoryData3.text = "Production rate: " + "Tons per year";
        pykreteFactoryData4.text = "Capacity: " + "Tons";
        
        pykreteFactoryPanel.SetActive(true);
    }

    public void DisableObjectUI()
    {
        objectPanel.SetActive(false);
        pykreteFactoryPanel.SetActive(false);
        objectInspected = null;
    }

   
   


}
