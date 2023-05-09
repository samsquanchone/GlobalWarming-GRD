using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is a singleton and will be handling nav mesh and nav agent based functionality, such as nav agent destinations
public class NavMeshManager : MonoBehaviour
{
    
    public static NavMeshManager Instance { get; private set; }

    public float bergOffset = 0; //Used to increment berg stopping distance
    float increment = 0.5f;
    public Transform[] bergSpawnPoints;


    private Transform destination;
    // Start is called before the first frame update
    void Awake()
    {
        //Non-lazy instantiation of singleton 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseBergOffset()
    {
        bergOffset += increment;
    }

    public float GetBergOffset()
    {
        float _offset = bergOffset;

        return _offset;
    }
    
}
