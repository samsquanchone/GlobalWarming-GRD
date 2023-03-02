using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeraFactory : MonoBehaviour
{
    [SerializeField] private GameObject bergPrefab;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float offSet;
    int targetLocationIndex = 0;


    public void ProducePykreteBerg()
    {

       Instantiate(bergPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + offSet), Quaternion.identity);

       navMeshAgent = bergPrefab.GetComponent<NavMeshAgent>();
       
       //Get a random inedx from the array containing different targets for the bergs to travel to 
       targetLocationIndex = Random.Range(0, NavMeshManager.instance.bergSpawnPoints.Length);

       Debug.Log("SpawnIndex: " + targetLocationIndex);

       navMeshAgent.destination = NavMeshManager.instance.bergSpawnPoints[targetLocationIndex].position;

    }

}
