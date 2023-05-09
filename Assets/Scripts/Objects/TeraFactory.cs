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

    [SerializeField] Transform spawnPoint;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float offSet;
    int targetLocationIndex = 0;
    
    float spawnCooldown = 0.9f;
    bool canSpawn = false;



    
    public void ProducePykreteBerg(int bergSize)
    {
       if(canSpawn)
       {
            switch(bergSize)
            {
            
                case 0:
                //Spawn small berg. Reduce global warming by agreed small berg amoun
                if(CanBuildBerg(smallBergPykretePrice)){Instantiate(bergSmallPrefab, spawnPoint.position, bergSmallPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.000025); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
                break;

                case 1:
                if(CanBuildBerg(mediumBergPykretePrice)){Instantiate(bergMediumPrefab, spawnPoint.position, bergMediumPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.00005); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
                break;

                case 2:
                if(CanBuildBerg(largeBergPykretePrice)){Instantiate(bergLargePrefab, spawnPoint.position, bergLargePrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.0001); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
                break;
            }

            
            canSpawn = false;
       }
    }

    private void Update()
    {
        if(!canSpawn)
        {
            spawnCooldown -= Time.deltaTime;

            if(spawnCooldown <= 0)
            {
                canSpawn = true;
                spawnCooldown = 0.5f;
            }
        }
    }


     //Check to see if player has enough pkyrete to build the specific berg
    private bool CanBuildBerg(int bergCost)
    {
        int timberBergCost = Player.instance.GetTimberStockPile() / 5; //Calculate fee in timber (20%)

        if(bergCost <= Player.instance.GetPkyreteStockPile() && bergCost <= timberBergCost)
        {
            Player.instance.RemoveAmountFromPykereteStockPile(bergCost);
            Player.instance.RemoveAmountFromTimber(timberBergCost);

            return true;
        }

        else
        {
            UIHoverManager.instance.ShowTip("Insufficient Pykerete OR Timber in Stockpile!", Input.mousePosition);
            AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.cantPurchase, null);
            return false;
        }
    }

}
