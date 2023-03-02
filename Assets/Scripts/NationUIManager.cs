using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NationUIManager : MonoBehaviour
{

    [SerializeField]public Canvas Nation_UI;

    [SerializeField] public TMP_Text Nation_Name_UI;

    [SerializeField] public TMP_Text Population_UI;
    [SerializeField] public TMP_Text Awareness_UI;
    [SerializeField] public TMP_Text Total_GDP_UI;
    [SerializeField] public TMP_Text GDP_Contribution_UI;

    [SerializeField] public TMP_Text Avaliable_Woodland_UI;

    [SerializeField] public TMP_Text Railway_Level_UI;
    [SerializeField] public TMP_Text Lumbermill_Level_UI;
    [SerializeField] public TMP_Text Harbour_Level_UI;
    [SerializeField] public TMP_Text Tera_Factory_Level_UI;

    private void Start()
    {
        Nation_UI.renderMode = RenderMode.WorldSpace;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Nation_UI.renderMode = RenderMode.WorldSpace;
        }

    }



    public void Show_Nation_UI(Nation nationData)
    {
        Nation_UI.renderMode = RenderMode.ScreenSpaceOverlay;
        //Pull Nation Data
        this.Nation_Name_UI.text = nationData.Nation_Name;

        this.Population_UI.text = nationData.Cumilative_Population_From_Territories.ToString();
        this.Awareness_UI.text = nationData.Awareness.ToString();
        this.Total_GDP_UI.text = nationData.GDP.ToString();
        this.GDP_Contribution_UI.text = nationData.GDP_Contribution.ToString();

        this.Avaliable_Woodland_UI.text = nationData.Woodland_Count.ToString();

        this.Railway_Level_UI.text = nationData.Lumbermill_Level.ToString();
        this.Lumbermill_Level_UI.text = nationData.Tera_Factory_Level.ToString();
        this.Harbour_Level_UI.text = nationData.Harbour_Level.ToString();
        this.Tera_Factory_Level_UI.text = nationData.Railway_Level.ToString();

    }
}
