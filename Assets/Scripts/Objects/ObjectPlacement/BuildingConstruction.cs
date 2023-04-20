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
        

       //USE CO ROUTINE FOR THIS, MORE EFFICENT
        
        if (constructionTimer >= 1f)
        {
           

            
            //SpawnObjectFromPool(); //Spawn object from pool
           Instantiate(constructedBuildingPrefab, transform.position, constructedBuildingPrefab.transform.rotation);
           Destroy(gameObject); //Destroy
            
        }
    }
    
    //This function is used to identify the constructed objec and set its enum type from its attached saveableobject script
    //Then setting the objectpools enum type to the respective objects type, then passing that to the coroutine to spawn object from pool
    void SpawnObjectFromPool()
    {
         //buildingConstructed
            ObjectType obj = constructedBuildingPrefab.GetComponent<SaveableObject>().objectType;
            
            switch(obj)
            {
                case ObjectType.Oak:
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Oak));
                break;

                case ObjectType.Willow:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Willow));
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);
                break;

                case ObjectType.Bamboo:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Bamboo));
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);

                break;

                case ObjectType.Pine:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Pine));
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);
                break;


                case ObjectType.Redwood:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Redwood));
                AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treePlanted, null);
                break;

                case ObjectType.Lumbermill:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Lumbermill));
                break;

                case ObjectType.TrainStation:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.TrainStation));
                break;

                case ObjectType.Dock:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Dock));
                break;

                case ObjectType.Factory:
                StartCoroutine(SpawnCoroutine(PoolingObjectType.Factory));
                break;
            }

           

    }

    private IEnumerator SpawnCoroutine(PoolingObjectType type)
    {
        GameObject _obj = PoolManager.instance.GetPoolObject(type);
        _obj.transform.position = transform.position; //Set transform based on position building has been constructed at
        _obj.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        PoolManager.instance.CoolObject(_obj, type);

        Destroy(gameObject); //Destroy VFX
    }
}
