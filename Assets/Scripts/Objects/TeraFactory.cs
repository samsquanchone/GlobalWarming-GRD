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
                if(CanBuildBerg(smallBergPykretePrice)){Instantiate(bergSmallPrefab, spawnPoint.position, bergSmallPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.000025); Player.instance.RemoveAmountFromTimber(smallBergPykretePrice); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
                break;

                case 1:
                if(CanBuildBerg(mediumBergPykretePrice)){Instantiate(bergMediumPrefab, spawnPoint.position, bergMediumPrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.00005); Player.instance.RemoveAmountFromTimber(mediumBergPykretePrice); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
                break;

                case 2:
                if(CanBuildBerg(largeBergPykretePrice)){Instantiate(bergLargePrefab, spawnPoint.position, bergLargePrefab.transform.rotation); Player.instance.MinusFromMonthlyHeatLevel(0.0001); Player.instance.RemoveAmountFromTimber(largeBergPykretePrice); AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.pykreteProduced, null);}
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
