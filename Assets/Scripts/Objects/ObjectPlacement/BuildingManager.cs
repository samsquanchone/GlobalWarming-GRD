using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //Singleton for global access, public get, private set
    public static BuildingManager instance { get; private set; }

    [SerializeField] private BuildingTypeSO activeBuildingType;
    

    public void Start()
    {
        //Non-lazy instantiation of singleton 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    public void SpawnBuilding(Vector3 position)
    {
        //Instantiate constructoin prefab for respective object that will be built, based off Vector 3 of raycast hit passed from playerInteraction script

        if (CanSpawnBuilding(GetActiveBuildingType(), position) == true)
        {
           Instantiate(activeBuildingType.constructionPrefab, position, Quaternion.identity);
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSO)
    {
        //Set the active building type
        activeBuildingType = buildingTypeSO;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    //This function is used for placement validation, currently just to check if there are any other buildings on chose area, however down the liine it should include checks
    //for e.g. if country in UN, or if near water for dock/shipping yard 
    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position) 
    {
         
        BoxCollider buildingBoxCollider = buildingTypeSO.constructionPrefab.GetComponent<BoxCollider>();

        bool isAreaClear = Physics.OverlapBox(position, buildingBoxCollider.size, Quaternion.identity) != null; 


        if (!isAreaClear) return false;


        //Building of type within radius: this is used to check if there is a building of tag Building in a certain radius,
        // will be useful for putting lumber mills near trees for example

        float maxBuildingRadius = 0.5f;

        Collider[] colliderArray = Physics.OverlapSphere(position, maxBuildingRadius);

        foreach (Collider collider in colliderArray)
        {
            //Create bool variable of a tag you want to check for in collision arrray
            bool hasBuilding = collider.tag == "Infrastrcture";
            bool hasTree = collider.tag == "Tree";
            bool hasWater = collider.tag == "Water";
            bool hasCountry = collider.tag == "UNCountry";

            //Check for collision
            if (hasBuilding /*|| hasWater */ || hasTree && !hasCountry)
            {
                //Could trigger UI here that indicates you can't build 
                Debug.Log("Cant spawn");
                return false;
            }
        }

        return true;  
      
    }
}
