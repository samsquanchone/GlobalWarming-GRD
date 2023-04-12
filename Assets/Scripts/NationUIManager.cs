using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NationUIManager : MonoBehaviour
{
    
    public static NationUIManager instance => m_instance;
    private static NationUIManager m_instance;

    //Sam edit: why are these all public? Remember set things to private unless you have to! The serializeField keyword shows private members in inspector, so why use on public memebers?
    [SerializeField] public Canvas Nation_UI;

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

    [SerializeField] private Button closePopUpButton;

    public bool Tile_Pressed = true;
    Nation Last_Nation_Pressed;
    private void Start()
    {
        m_instance = this;
        Nation_UI.renderMode = RenderMode.WorldSpace;

        Date_and_Time_System.instance.Month_Pass_Event.AddListener(() => Show_Nation_UI(Last_Nation_Pressed));
        closePopUpButton.onClick.AddListener(CloseNationUI);
    }

    private void CloseNationUI()
    {
        Nation_UI.renderMode = RenderMode.WorldSpace;
        Tile_Pressed = false;
    }

    
    public void Show_Nation_UI(Nation nationData)
    {
        if (Tile_Pressed)
        {
            Nation_UI.renderMode = RenderMode.ScreenSpaceOverlay;
            Last_Nation_Pressed = nationData;
            Tile_Pressed = true;
            //Pull Nation Data
            this.Nation_Name_UI.text = nationData.Nation_Name;

            this.Population_UI.text = (nationData.Cumilative_Population_From_Territories / 1000000).ToString() + " M";
            this.Awareness_UI.text = (nationData.Awareness).ToString("F2") + " %";
            this.Total_GDP_UI.text = (nationData.GDP).ToString() + " M";
            this.GDP_Contribution_UI.text = (nationData.GDP_Contribution).ToString() + " M";

            this.Avaliable_Woodland_UI.text = nationData.Woodland_Count.ToString();

            this.Railway_Level_UI.text = nationData.Railway_Level.ToString(); 
            this.Lumbermill_Level_UI.text = nationData.Lumbermill_Level.ToString();
            this.Harbour_Level_UI.text = nationData.Harbour_Level.ToString();
            this.Tera_Factory_Level_UI.text = nationData.Tera_Factory_Level.ToString(); 
        }

    }
}
