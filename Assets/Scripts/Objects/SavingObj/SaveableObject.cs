using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectType {Oak, Bamboo, Willow, Factory, Dock, Lumbermill, TrainStation} //Enum for object ypes


/// <summary>
/// This is the parent class for saveable objects,
/// it is abstract to avoid being able to add it to game objects. 
/// </summary>
public abstract class SaveableObject : MonoBehaviour
{
    protected string saveStats;
    [SerializeField] private ObjectType objectType;
    // Start is called before the first frame update
    private void Start()
    {
        PersistentManagerScript.instance.saveableObjects.Add(this);
      
    }

    //Any classes that inherit this class must override this function
    public virtual void Save(int id)
    {
       
        //save object scene index, properties(position, scale, rotation), as well as specific data in this object e.g. wood yield
        PlayerPrefs.SetString(Application.loadedLevel + "-" + id.ToString(), objectType + "_" + transform.position.ToString() + "_" + transform.localScale + "_" + transform.localRotation + "_" + saveStats);
    }

    //Any classes that inherit this class must override this function
    public virtual void Load(string[] values)
    {
        //Converting saved object properties from string into vector3 and quaternion, and setting the objects properties for re-instantiation
        transform.localPosition = PersistentManagerScript.instance.StringToVector(values[1]);
        transform.localScale = PersistentManagerScript.instance.StringToVector(values[2]);
        transform.localRotation = PersistentManagerScript.instance.StringToQuaternion(values[3]);
    }

    public void DestroySaveable()
    {
        PersistentManagerScript.instance.saveableObjects.Remove(this);
        Destroy(gameObject);
    }
}
