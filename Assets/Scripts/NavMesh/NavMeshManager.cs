using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is a singleton and will be handling nav mesh and nav agent based functionality, such as nav agent destinations
public class NavMeshManager : MonoBehaviour
{
    
    public static NavMeshManager Instance { get; private set; }

    public float bergOffset = 0; //Used to increment berg stopping distance

    float increment = 0.7f;
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

    void Start()
    {
        if(!MenuData.GetGameType()) //Sam edit: IS A LOAD GAME: instantiate local variables from player data json save file
        {
            bergOffset = LoadNavData();
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

    public void SaveNavData()
    {
        SaveableNavData navData = new SaveableNavData();
        navData.offSet = bergOffset;

        JSONManager.SaveNavJSON(navData, "NavAgentData");


    }

    public float LoadNavData()
    {
        SaveableNavData navData = JSONManager.LoadNavData("NavAgentData");
        float _bergOffset = navData.offSet;

        return _bergOffset;

    }
    
}

[System.Serializable]
public class SaveableNavData
{
    public float offSet;
}

