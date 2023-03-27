using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapModeManager : MonoBehaviour
{
    [SerializeField] Button Normal_Map_Button;
    [SerializeField] Button Heat_Map_Button;
    [SerializeField] Button GDP_Map_Button;
    [SerializeField] Button GDP_Contribution_Map_Button;
    [SerializeField] Button Awareness_Map_Button;
    [SerializeField] Button Woodland_Map_Button;
    [SerializeField] Button Building_Level_Map_Button;

    bool Normal_Map_Pressed;
    bool Heat_Map_Pressed;
    bool GDP_Map_Pressed;
    bool GDP_Contribution_Map_Pressed;
    bool Awareness_Map_Pressed;
    bool Woodland_Map_Pressed;
    bool Building_Level_Map_Pressed;

    Nation[] All_Nations;
    Tile[] All_Tiles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("(!)Date & Time System").GetComponent<Date_and_Time_System>().Month_Pass_Event.AddListener(Update_According_to_the_Selected_Map_Mode);
        All_Nations = GameObject.FindObjectsOfType<Nation>();
        All_Tiles = GameObject.FindObjectsOfType<Tile>();

        Normal_Map_Button.onClick.AddListener(Normal_Map_Colouring);
        Heat_Map_Button.onClick.AddListener(Heat_Map_Colouring);
        GDP_Map_Button.onClick.AddListener(GDP_Map_Colouring);
        GDP_Contribution_Map_Button.onClick.AddListener(GDP_Contribution_Map_Colouring);
        Awareness_Map_Button.onClick.AddListener(Awareness_Map_Colouring);
        Woodland_Map_Button.onClick.AddListener(Woodland_Map_Colouring);
        Building_Level_Map_Button.onClick.AddListener(Building_Map_Colouring);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_According_to_the_Selected_Map_Mode()
    {
        if (Normal_Map_Pressed)
        {

        }

        if (Heat_Map_Pressed)
        {

        }

        if (GDP_Map_Pressed)
        {

        }

        if (GDP_Contribution_Map_Pressed)
        {

        }

        if (Awareness_Map_Pressed)
        {

        }

        if (Woodland_Map_Pressed)
        {

        }

        if (Building_Level_Map_Pressed)
        {

        }
    }


    public void Normal_Map_Colouring()
    {
        Normal_Map_Pressed = true;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = false;

        for (int i = 0; i < All_Nations.Length; i++)
        {
            if (All_Nations[i] != null)
            {
                All_Nations[i].Colour_All_Teritories_According_to_the_Nation_Colour();
            }
        }
    }

    public void Heat_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = true;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = false;
        //min 10
        //max 25

        for (int i = 0; i < All_Tiles.Length; i++)
        {
            if (All_Tiles[i] != null)
            {
                if (All_Tiles[i].Average_Heat_Level < 10)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 78, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 12) {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 227, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 13)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 233, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 15)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(218, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 16)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 226, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 18)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 181, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 19)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 131, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 20)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 76, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 21)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(218, 12, 131, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 22)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(72, 10, 90, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Average_Heat_Level < 23)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(40, 5, 50, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else //if (All_Tiles[i].Average_Heat_Level > 22)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(12, 1, 20, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }
    }


    public void GDP_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = true;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = false;

        for (int i = 0; i < All_Tiles.Length; i++)
        {
            if (All_Tiles[i] != null)
            {
                if (All_Tiles[i].Occupiant_Nation.GDP < 100)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 43, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 200)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 137, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 500)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 220, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 750)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(202, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 1000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(108, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 2000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 155, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 3000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 233, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 4000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 128, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP < 5000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(74, 43, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else //if (All_Tiles[i].Occupiant_Nation.GDP < 20000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(157, 43, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }
    }

    public void GDP_Contribution_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = true;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = false;

        for (int i = 0; i < All_Tiles.Length; i++)
        {
            if (All_Tiles[i] != null)
            {
                if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 5)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 10)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 49, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 20)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 74, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 50)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 178, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 100)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(240, 255, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 200)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 255, 154, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 300)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 221, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 400)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 135, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 500)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 68, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else //if (All_Tiles[i].Occupiant_Nation.GDP_Contribution < 2000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }
    }

    public void Awareness_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = true;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = false;

        for (int i = 0; i < All_Tiles.Length; i++)
        {
            if (All_Tiles[i] != null)
            {
                if (All_Tiles[i].Occupiant_Nation.Awareness < 2)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 5)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 49, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 10)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 227, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 20)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(190, 255, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 30)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(49, 255, 0, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 40)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 255, 141, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 50)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 227, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 60)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 129, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 70)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(0, 135, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Awareness < 80)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(12, 0, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else  //if (All_Tiles[i].Occupiant_Nation.Awareness < 90)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(98, 0, 255, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }
    }
    
    public void Woodland_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = true;
        Building_Level_Map_Pressed = false;


        for (int i = 0; i < All_Tiles.Length; i++)
        {
            if (All_Tiles[i] != null)
            {
                if (All_Tiles[i].Occupiant_Nation.Woodland_Count == 0)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(171, 171, 171, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Woodland_Count < 3000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(181, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Woodland_Count < 5000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(130, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Woodland_Count < 7500)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(74, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (All_Tiles[i].Occupiant_Nation.Woodland_Count < 10000)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 58, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else  //if (All_Tiles[i].Occupiant_Nation.Woodland_Count < 12500)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 115, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
              
            }
        }
    }

    public void Building_Map_Colouring()
    {
        Normal_Map_Pressed = false;
        Heat_Map_Pressed = false;
        GDP_Map_Pressed = false;
        GDP_Contribution_Map_Pressed = false;
        Awareness_Map_Pressed = false;
        Woodland_Map_Pressed = false;
        Building_Level_Map_Pressed = true;

 

        for (int i = 0; i < All_Tiles.Length; i++)
        {
            int Total_Building = 0;
            if (All_Tiles[i] != null)
            {
                Total_Building += All_Tiles[i].Occupiant_Nation.Lumbermill_Level;
                Total_Building += All_Tiles[i].Occupiant_Nation.Tera_Factory_Level;
                Total_Building += All_Tiles[i].Occupiant_Nation.Railway_Level;
                Total_Building += All_Tiles[i].Occupiant_Nation.Harbour_Level;

                if (Total_Building < 2)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 43, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (Total_Building < 4)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 120, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (Total_Building < 6)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(255, 186, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (Total_Building < 8)
                {
                    try
                    {
                        All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(212, 255, 43, 255);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                else if (Total_Building < 10)
                    {
                        try
                        {
                            All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(120, 255, 43, 255);
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                else if (Total_Building < 15)
                    {
                        try
                        {
                            All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 79, 255);
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                else //if (Total_Building > 14)
                    {
                        try
                        {
                            All_Tiles[i].GetComponent<Renderer>().material.color = new Color32(43, 255, 196, 255);
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                
            }
        }
    }

}
