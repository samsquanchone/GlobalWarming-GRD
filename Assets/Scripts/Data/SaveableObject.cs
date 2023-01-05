using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectType {Tree, Factory} //Enum for object ypes


/// <summary>
/// This is the parent class for saveable objects,
/// it is abstract to avoid being able to add it to game objects. 
/// </summary>
public abstract class SaveableObject : MonoBehaviour
{
    protected string save;
    [SerializeField] private ObjectType objectType;
    // Start is called before the first frame update
    private void Start()
    {
        PersistentManagerScript.instance.saveableObjects.Add(this);
      //  PlayerPrefs.setInt
    }

    //Any classes that inherit this class must override this function
    public virtual void Save(int id)
    {
       
        //Convert to string for vector3 for object position
        PlayerPrefs.SetString(id.ToString(), objectType + "_" + transform.position.ToString());
    }

    //Any classes that inherit this class must override this function
    public virtual void Load(string[] values)
    {
        transform.localPosition = PersistentManagerScript.instance.StringToVector(values[1]);
    }

    public void DestroySaveable()
    {
        
    }
}
