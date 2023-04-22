using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualization : MonoBehaviour
{

    [SerializeField] GameObject Money_Model;
    [SerializeField] GameObject Population_Model;

    [SerializeField] Toggle Pop_Visualization_Toggle;
    [SerializeField] Toggle Money_Visualization_Toggle;

    [SerializeField] bool Pop_Viz_Enabled = false;
    [SerializeField] bool Money_Viz_Enabled = false;


    Nation[] All_Nations; 

    // Start is called before the first frame update
    void Start()
    {
        All_Nations = GameObject.FindObjectsOfType<Nation>();
        Debug.Log("Nation Count: " + All_Nations.Length);

        if (Pop_Visualization_Toggle.isOn)
        {
            Debug.Log("Toogle is On");
        }
        else
        {
            Debug.Log("Toogle is Off");
        }


    }
    private void Update()
    {
        //Pop Visualization
        if (Pop_Visualization_Toggle.isOn && Pop_Viz_Enabled == false)
        {
            Pop_Viz_Enabled = true;
            Visualize_Population();
            Debug.Log("Toogle is On");
        }
        else if (Pop_Visualization_Toggle.isOn == false && Pop_Viz_Enabled == true)
        {
            Pop_Viz_Enabled = false;
            Delete_Pop_Visualization();
            Debug.Log("Toogle is Off");
        }

        //Money Visualization
        if (Money_Visualization_Toggle.isOn && Money_Viz_Enabled == false)
        {
            Money_Viz_Enabled = true;
            Visualize_GDP();
        }
        else if (Money_Visualization_Toggle.isOn == false && Money_Viz_Enabled == true)
        {
            Money_Viz_Enabled = false;
            Delete_Money_Visualization();
        }
    }
    public void Visualize_Population()
    {
        Debug.Log("Visualize Pops");
        for(int i = 0; i < All_Nations.Length; i++)
        {
            
            Vector3 Mesh_Center = new Vector3(All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.x , All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.y + 0.5f , All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.z);



            int Pop_Count = (int)(All_Nations[i].Cumilative_Population_From_Territories / 2000000);
            if (Pop_Count < 5) { Pop_Count = 5; }
            Debug.Log("Pop count " + Pop_Count);
            if(Pop_Count > 50) { Pop_Count = 50; }

            for (int z = 0; z < Pop_Count; z++)
            {
                Instantiate(Population_Model, Mesh_Center, Quaternion.identity);
            }





        }
    }

    public void Visualize_GDP()
    {
        Debug.Log("Visualize GDP");
        for (int i = 0; i < All_Nations.Length; i++)
        {
            Vector3 Mesh_Center = new Vector3(All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.x, All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.y + 0.5f, All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.z + 0.5f);


            int Money_Count = All_Nations[i].GDP / 350;

            if(Money_Count > 30) { Money_Count = 30; }
            if(Money_Count < 1) { Money_Count = 1; }
            else if(Money_Count == 1) { Money_Count = 2; }
            else if (Money_Count == 2) { Money_Count = 3; }
            else if (Money_Count == 3) { Money_Count = 4; }
            else if (Money_Count == 5) { Money_Count = 6; }

            for (int z = 0; z < Money_Count; z++)
            {
                Instantiate(Money_Model, Mesh_Center, Quaternion.identity);
            }
    

 

        }
    }

    public void Delete_Pop_Visualization()
    {
        GameObject[] HumanModels = GameObject.FindGameObjectsWithTag("HumanModel");
        foreach (GameObject HumanModel in HumanModels)
            GameObject.Destroy(HumanModel);
    }

    public void Delete_Money_Visualization()
    {
        GameObject[] MoneyModels = GameObject.FindGameObjectsWithTag("MoneyModel");
        foreach (GameObject MoneyModel in MoneyModels)
            GameObject.Destroy(MoneyModel);
    }
}
