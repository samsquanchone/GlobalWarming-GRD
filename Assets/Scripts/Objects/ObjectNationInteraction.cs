using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNationInteraction : MonoBehaviour
{
    bool nationFound = false;
    public string nationPlaced;
    public GameObject nation;
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UNCountry" && !nationFound)
        {
            nation = other.transform.gameObject;
            nationPlaced = other.transform.gameObject.name;
            
            Debug.Log("Placed on" + other.transform.gameObject.name);

           
            //Add this instance of object to tile script, providing Enum value for objectType for identification
            other.transform.gameObject.GetComponent<Tile>().AddObject(GetComponent<SaveableObject>().objectType);

            //Used to remove re-occuring iterrations of this loop once a nation is found
            nationFound = true;

            if (gameObject.name == "Dock(Clone)")
            {
                Debug.Log("dOCK ADDED TO NAV LIST");
                NavMeshManager.instance.SetDestination(gameObject.transform);
            }

        }
    }

    

    

   
       
   

   


    
}
