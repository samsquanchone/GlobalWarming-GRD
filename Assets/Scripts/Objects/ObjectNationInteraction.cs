using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNationInteraction : MonoBehaviour
{
    bool nationFound = false;
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UNCountry" && !nationFound)
        {
            Debug.Log("Placed on" + other.transform.gameObject.name);
            other.transform.gameObject.GetComponent<Tile>().nationPlacedObjectsList.Add(this.gameObject);
            nationFound = true;

            if (gameObject.name == "Dock(Clone)")
            {
                Debug.Log("dOCK ADDED TO NAV LIST");
                NavMeshManager.instance.SetDestination(gameObject.transform);
            }

        }


    }

   
       
   

   


    
}
