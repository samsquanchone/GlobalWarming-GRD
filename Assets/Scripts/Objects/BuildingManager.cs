using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance => m_Instance;
    private static BuildingManager m_Instance;
    [SerializeField] private BuildingTypeSO activeBuildingType;
    

    public void Awake()
    {
        m_Instance = this;
    }


    public void SpawnBuilding(Vector3 position)
    {
        
        Instantiate(activeBuildingType.constructionPrefab, position, Quaternion.identity);
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSO)
    {
        activeBuildingType = buildingTypeSO;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position) 
    {
        /* This function is used for placement validation, currently just to check if there are any other buildings on chose area, however down the liine it should include checks
        for e.g. if country in UN, or if near water for dock/shipping yard 
        
         NOTE: THIS MAY NEED CHANGING TO WORK WITH THE RAYCAST STUFF, SO LOCATION IS DONE OFF RAYCAST, AND ON COLLISION WORKS HERE*/
        BoxCollider buildingBoxCollider = buildingTypeSO.constructionPrefab.GetComponent<BoxCollider>();

        bool isAreaClear = Physics.OverlapBox(position, buildingBoxCollider.size, Quaternion.identity) != null; //Variable is equal to no collider overlap with other buildingss


        if (!isAreaClear) return false;


        /*Building of type within radius: this is used to check if there is a building of tag Building in a certain radius,
          will be useful for putting lumber mills near trees for example */

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
