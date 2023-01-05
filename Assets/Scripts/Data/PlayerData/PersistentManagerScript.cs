using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{
    //This script will be the main script where data is handled, it will contain all variables that will be serialized for a save system,
    //any objects built will be stored in a list with Vector3 for position, on reload these objects will then be reinstantiated 
    public static PersistentManagerScript instance { get; private set; }
    private double playerWealth = 0;
    [SerializeField] private List<GameObject> playerObjectsList = new List<GameObject>(); //Will be used to store any objects that player builds for re-load of a save file

    public List<SaveableObject> saveableObjects { get; private set; }


    private void Awake()
    {
        saveableObjects = new List<SaveableObject>(); //Insantiate list

    }

    private void Start()
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

    public void Save()
    {
        //Keep track of amount of objects save, so we can load the correct amount 
        PlayerPrefs.SetInt("ObjectCount", saveableObjects.Count);

        for (int i = 0; i < saveableObjects.Count; i++)
        {
            //Save all objects in the saveableObjects list (Added to list when a player placed object spawns)
            saveableObjects[i].Save(i);
        }
    }

    public void Load()
    {
        //Used to destroy objects in world when loading, to avoid duplicates
        foreach (SaveableObject obj in saveableObjects)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        //Clear saveableObjectsList
        saveableObjects.Clear();


        int objectCount = PlayerPrefs.GetInt("ObjectCount");

        for (int i = 0; i < objectCount; i++)
        {
            string[] values = PlayerPrefs.GetString(i.ToString()).Split('_');
            GameObject tmp = null;

            switch (values[0])
            {
                case "Tree":
                    tmp = Instantiate(Resources.Load("Tree") as GameObject);
                    break;
                case "Factory":
                    tmp = Instantiate(Resources.Load("Factory") as GameObject);
                    break;
            }

            if (tmp != null)
            {
                tmp.GetComponent<SaveableObject>().Load(values);
            }
           
            
        }
    }

    public Vector3 StringToVector(string value)
    {
        //Trim brackets from Vector3 to string conversion
        value = value.Trim(new char[] { '(', ')' });

        //Remove whitespace from Vector3 to string conversion
        value = value.Replace(" ", " ");

        //Convert string value into an array of values for X,Y,Z positions
        string[] pos = value.Split(',');

        //Parse string array of position values into a Vector3, then return the new Vector3
        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

    public Quaternion StringToQuaternion(string value)
    {
        return Quaternion.identity;
    }

    public void AddObjectToPersistentDataList(GameObject gameObject)
    {
        playerObjectsList.Add(gameObject);
    }

    public void AddWealth(double wealth)
    {

        playerWealth += wealth;
    }

    public double GetWealth()
    {
        return playerWealth;
    }

}
