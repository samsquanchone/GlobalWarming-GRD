using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDataManager : MonoBehaviour
{
    public static GraphDataManager instance => m_instance;
    private static GraphDataManager m_instance;
    
    [SerializeField] private Button moneyDataSetButton;
    [SerializeField] private Button treesPlantedDataSetButton;
    [SerializeField] private Button pykreteProducedDataSetButton;
    [SerializeField] private Button co2ProducedDataSetButton;


    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        
    }

   
}
