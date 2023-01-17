using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    [SerializeField] private Transform constructedBuildingPrefab;

    private float constructionTimer; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeToConstruct = 5f;

        constructionTimer += Time.deltaTime / timeToConstruct;

        if (constructionTimer >= 1f)
        {
            //buildingConstructed
            Instantiate(constructedBuildingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
