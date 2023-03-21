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
        if(constructedBuildingPrefab.tag == "Infrastructure" || constructedBuildingPrefab.tag == "TeraFactory")
        {
            AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.infrastructureBuilding, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float timeToConstruct = 10.1f;

        constructionTimer += Time.deltaTime / timeToConstruct;
        



        if (constructionTimer >= 1f)
        {
            //buildingConstructed

            if(constructedBuildingPrefab.tag == "Tree")
            {
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);
            }

            Instantiate(constructedBuildingPrefab, transform.position, constructedBuildingPrefab.transform.rotation);

            Destroy(gameObject);
        }
    }
}
