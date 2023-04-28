using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    //Sam Add: making this a singleton:
    public static NodeManager instance => m_instance;
    private static NodeManager m_instance;



    [SerializeField] Node[] Sea_Nodes;
    [SerializeField] Node[] Land_Nodes;

    [SerializeField] GameObject Ship;
    [SerializeField] GameObject Train;

    /*    [SerializeField] int Number_of_Ships = 0;
        [SerializeField] int Number_of_Trains = 0;*/
    private void Update()
    {

    }
    void Start()
    {
        m_instance = this;
    }

    public void Add_Ship()
    {
        int NewSpawnNodeNumber = Random.Range(0, Sea_Nodes.Length - 1);
        GameObject SpawnedShipObject = Instantiate(Ship, Sea_Nodes[NewSpawnNodeNumber].transform.position , Quaternion.identity);
        SpawnedShipObject.GetComponent<Vehicle>().TargetNode = Sea_Nodes[NewSpawnNodeNumber];
        SpawnedShipObject.transform.LookAt(Sea_Nodes[NewSpawnNodeNumber].transform.position);
    }

    public void Add_Train()
    {
        int NewSpawnNodeNumber = Random.Range(0, Land_Nodes.Length - 1);
        GameObject SpawnedShipObject = Instantiate(Train, Land_Nodes[NewSpawnNodeNumber].transform.position, Quaternion.identity);
        SpawnedShipObject.GetComponent<Vehicle>().TargetNode = Land_Nodes[NewSpawnNodeNumber];
        SpawnedShipObject.transform.LookAt(Land_Nodes[NewSpawnNodeNumber].transform.position);
    }
}
