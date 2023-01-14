using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance => m_Instance; //Singleton for global access, public get, private set
    private static BuildingManager m_Instance;

    [SerializeField] private BuildingTypeSO activeBuildingType;
    

    public void Awake()
    {
        m_Instance = this;
    }


    public void SpawnBuilding(Vector3 position)
    {
        //Instantiate constructoin prefab for respective object that will be build, based off Vector 3 of raycast hit passed from playerInteraction script
        Instantiate(activeBuildingType.constructionPrefab, position, Quaternion.identity);
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position) 
    {
         //This function is used for placement validation
        
      
        BoxCollider buildingBoxCollider = buildingTypeSO.constructionPrefab.GetComponent<BoxCollider>();

        bool isAreaClear = Physics.OverlapBox(position, buildingBoxCollider.size, Quaternion.identity) != null; //Variable is equal to no collider overlap with other buildingss


        if (!isAreaClear) return false;


        //Building of type within radius: this is used to check if there is a building of tag Building in a certain radius,
        // or for checking if there is water where trying to place object 

        float maxBuildingRadius = 15f;

        Collider[] colliderArray = Physics.OverlapSphere(position, maxBuildingRadius);

        foreach (Collider collider in colliderArray)
        
            //Create variables that are equal to any tags we don't want to place objects on, set from the colliderArray
            bool hasBuilding = collider.tag == "Building";
            bool hasWater = collider.tag == "Water";


            //Check if anyunwated tagged objects are present where trying to place an object
            if (hasBuilding || hasWater)
            {
                return true;
            }
        }

        return false;
        
        
      
    }
}
