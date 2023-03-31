using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeReplantManager : MonoBehaviour
{
    public static TreeReplantManager instance => m_instance;
    private static TreeReplantManager m_instance;

    [SerializeField] GameObject[] treePrefabs;


    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        
    }

    //When tree harvested from treegrowth script, pass its obj to here to replant it, wanted to just pass object but wrong materials assigned and cant swap them back with OG as not working, so used this method instead
    public void ReplantTree(ObjectType objType, Transform positionToPlace)
    {
        switch(objType)
        {
            case ObjectType.Oak:
            Instantiate(treePrefabs[0], positionToPlace.transform.position, positionToPlace.transform.rotation);
            
            break;

            case ObjectType.Bamboo:
            Instantiate(treePrefabs[1], positionToPlace.transform.position, positionToPlace.transform.rotation);

            break;

            case ObjectType.Willow:
            Instantiate(treePrefabs[2], positionToPlace.transform.position, positionToPlace.transform.rotation);

            break;

            case ObjectType.Pine:
            Instantiate(treePrefabs[3], positionToPlace.transform.position, positionToPlace.transform.rotation);

            break;

            case ObjectType.Redwood:
            Instantiate(treePrefabs[4], positionToPlace.transform.position, positionToPlace.transform.rotation);

            break;

        }
    }
}
