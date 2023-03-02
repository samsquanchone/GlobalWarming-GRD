using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is a singleton and will be handling nav mesh and nav agent based functionality, such as nav agent destinations
public class NavMeshManager : MonoBehaviour
{
    
    public static NavMeshManager instance { get; private set; }


    public Transform[] bergSpawnPoints;

    private Transform destination;
    // Start is called before the first frame update
    void Awake()
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

    public void SetDestination(Transform transform)
    {
        destination = transform;
    }

    public Transform GetDestination()
    {

        return destination;

    }


}
