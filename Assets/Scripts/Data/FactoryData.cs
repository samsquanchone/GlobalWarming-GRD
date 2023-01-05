using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentManagerScript.instance.AddObjectToPersistentDataList(this.gameObject); //Add instantiated object to persistent data list for serialization
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
