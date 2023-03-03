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

    //Images //
    [SerializeField] private Image objectImage;
 
    //Buttons//
    [SerializeField] private Button objectActionButton;

    //Button Text//
    [SerializeField] private TMP_Text buttonText;

    //Text//
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private TMP_Text objectData1;
    [SerializeField] private TMP_Text objectData2;
    [SerializeField] private TMP_Text objectData3;
    [SerializeField] private TMP_Text objectData4;
    [SerializeField] private TMP_Text objectData5;
    

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
    }

    public void SetObjectUI(GameObject objectToDisplay)
    {
        objectInspected = objectToDisplay;
        objectName.text = objectToDisplay.name;
        objectData1.text = "Country Placed: " + objectToDisplay.GetComponent<ObjectNationInteraction>().nationPlaced;
        objectImage.sprite = objectToDisplay.GetComponent<Image>().sprite;

        if (objectToDisplay.tag == "Tree")
        {
           
            objectData2.text = "Time until fully grown: " + objectToDisplay.GetComponent<TreeGrowth>().GetGrowthTimeRemaining();
           // objectData3.text = "Current growth conditions (Temperature): " + objectToDisplay.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().Average_Heat_Level;
            objectData4.text = "Expected yield: " + objectToDisplay.GetComponent<TreeObject>().m_yield;
            buttonText.text = "Harvest";

            objectPanel.SetActive(true);
        }
        if (objectToDisplay.name == "Lumbermill(Clone)")
        {
            
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Lumbemill";
          
            objectPanel.SetActive(true);
        }
        if (objectToDisplay.name == "Dock(Clone)" || objectToDisplay.name == "TrainStation(Clone)")
        {
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Transport";
            objectPanel.SetActive(true);
        }
        else if (objectToDisplay.name == "Factory(Clone)")
        {
            objectData2.text = "Processed wood stockpile: " + "Tons";
            objectData3.text = "Production rate: " + "Tons per year";
            objectData4.text = "Capacity: " + "Tons";
            buttonText.text = "Upgrade Factory";
            objectPanel.SetActive(true);
        }
        
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

        else if (objectInspected.tag == "TeraFactory")
        {
            //Spawn a pykrete berg at the selected tera factory
            objectInspected.GetComponent<TeraFactory>().ProducePykreteBerg();
        }
    }

    public void DisableObjectUI()
    {
        objectPanel.SetActive(false);
        objectInspected = null;
    }

   
   


}
