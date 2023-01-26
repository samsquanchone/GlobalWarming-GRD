using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    //Maybe create two lists and go through them, one for infrastructure and other for tree projects, #
    //could allow more flexibility with UI

    [SerializeField] private List<BuildingTypeSO> buildingTypeSOList;
    [SerializeField] private BuildingManager buildingManager;

    

    private Dictionary<BuildingTypeSO, Transform> objectBtnDictionary;

    
    private void Awake()
    {
        Transform objectButtonTemplate = transform.Find("ObjectButtonTemplate");
        objectButtonTemplate.gameObject.SetActive(false);

        objectBtnDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        foreach(BuildingTypeSO buildingTypeSO in buildingTypeSOList)
        {
            Transform objectButtonTransform = Instantiate(objectButtonTemplate, transform);
            objectButtonTransform.gameObject.SetActive(true);

            //Name the new object
            objectButtonTransform.gameObject.name = buildingTypeSO.name;

            //Provide SO to ToolTip script attached to button instantiated, so we can extrapolate data within the tooltip script 
            objectButtonTransform.GetComponent<HoverTip>().SetToolTipData(buildingTypeSO);

            objectButtonTransform.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, index * -30);

            objectButtonTransform.Find("BuildingImage").GetComponent<Image>().sprite = buildingTypeSO.sprite;
            objectButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                buildingManager.SetActiveBuildingType(buildingTypeSO);
                UpdateSelectedVisual();
            });
            objectBtnDictionary[buildingTypeSO] = objectButtonTransform;
            index++;
        }
    }

    private void Start()
    {
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {

        foreach (BuildingTypeSO buildingTypeSO in objectBtnDictionary.Keys)
        {
            objectBtnDictionary[buildingTypeSO].Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = buildingManager.GetActiveBuildingType();
        objectBtnDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
   

}
