using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class TeraFactory : MonoBehaviour
{
     
    [SerializeField] private GameObject bergSmallPrefab;
    [SerializeField] private GameObject bergMediumPrefab;
    [SerializeField] private GameObject bergLargePrefab;

    [SerializeField] private int smallBergPykretePrice;
    [SerializeField] private int mediumBergPykretePrice;
    [SerializeField] private int largeBergPykretePrice;


    private NavMeshAgent navMeshAgent;
    [SerializeField] private float offSet;
    int targetLocationIndex = 0;


    
    public void ProducePykreteBerg(int bergSize)
    {

       switch(bergSize)
       {

       case 0:
       //Spawn small berg. Reduce global warming by agreed small berg amoun
       if(CanBuildBerg(smallBergPykretePrice)){Instantiate(bergSmallPrefab, new Vector3(this.gameObject.transform.position.x + offSet, this.gameObject.transform.position.y, this.gameObject.transform.position.z), bergSmallPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.000025);}
       
       break;

       case 1:
       if(CanBuildBerg(mediumBergPykretePrice)){Instantiate(bergMediumPrefab, new Vector3(this.gameObject.transform.position.x + offSet, this.gameObject.transform.position.y, this.gameObject.transform.position.z), bergMediumPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.00005);}
       break;

       case 3:
       if(CanBuildBerg(largeBergPykretePrice)){Instantiate(bergLargePrefab, new Vector3(this.gameObject.transform.position.x + offSet, this.gameObject.transform.position.y, this.gameObject.transform.position.z), bergLargePrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.0001);}
       break;
       
      /*
       navMeshAgent = bergPrefab.GetComponent<NavMeshAgent>();
       
       //Get a random inedx from the array containing different targets for the bergs to travel to 
       targetLocationIndex = Random.Range(0, NavMeshManager.instance.bergSpawnPoints.Length);

       Debug.Log("SpawnIndex: " + targetLocationIndex);

       navMeshAgent.destination = NavMeshManager.instance.bergSpawnPoints[targetLocationIndex].position;
       */
       }

    }


     //Check to see if player has enough pkyrete to build the specific berg
    private bool CanBuildBerg(int bergCost)
    {
        if(bergCost <= Player.instance.GetPkyreteStockPile())
        {
            Player.instance.RemoveAmountFromPykereteStockPile(bergCost);
            return true;
        }

        else
        {
            UIHoverManager.instance.ShowTip("Insufficient Pykerete in Stockpile!", Input.mousePosition);
            return false;
        }
    }

}
