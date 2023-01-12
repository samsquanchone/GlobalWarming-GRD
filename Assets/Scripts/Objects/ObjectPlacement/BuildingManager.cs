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
         //This function is used for placement validation, currently just to check if there are any other buildings on chose area, however down the liine it should include checks
        //for e.g. if country in UN, or if near water for dock/shipping yard 
        
      
        BoxCollider buildingBoxCollider = buildingTypeSO.constructionPrefab.GetComponent<BoxCollider>();

        bool isAreaClear = Physics.OverlapBox(position, buildingBoxCollider.size, Quaternion.identity) != null; //Variable is equal to no collider overlap with other buildingss


        if (!isAreaClear) return false;


        //Building of type within radius: this is used to check if there is a building of tag Building in a certain radius,
        // will be useful for putting lumber mills near trees for example

        float maxBuildingRadius = 15f;

        Collider[] colliderArray = Physics.OverlapSphere(position, maxBuildingRadius);

        foreach (Collider collider in colliderArray)
        {
            bool hasBuilding = collider.tag == "Building";

            if (hasBuilding)
            {
                return true;
            }
        }

        return false;
        
        
      
    }
}
