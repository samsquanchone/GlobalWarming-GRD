using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualization : MonoBehaviour
{

    [SerializeField] GameObject Money_Model;
    [SerializeField] GameObject Population_Model;

    Nation[] All_Nations; 

    // Start is called before the first frame update
    void Start()
    {
        All_Nations = GameObject.FindObjectsOfType<Nation>();
        Debug.Log("Nation Count: " + All_Nations.Length);
    }

    public void Visualize_Population()
    {
        for(int i = 0; i < All_Nations.Length; i++)
        {
            Instantiate(Population_Model, All_Nations[i].Nations_Territories[0].transform.position, Quaternion.identity);
        }
    }

    public void Visualize_GDP()
    {
        for (int i = 0; i < All_Nations.Length; i++)
        {
            Instantiate(Money_Model, All_Nations[i].Nations_Territories[0].transform.position, Quaternion.identity);
        }
    }
}
