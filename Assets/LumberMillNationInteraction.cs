using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberMillNationInteraction : MonoBehaviour
{
    private bool nationFound = false;

    void Start()
    {
        if (gameObject.name == "Dock(Clone)")
        {
            Debug.Log("dOCK ADDED TO NAV LIST");
            NavMeshManager.instance.SetDestination(gameObject.transform);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Placed on" + collision.gameObject.name);

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "UNCountry" &&  !nationFound)
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Placed on" + collision.gameObject.name);

            collision.gameObject.GetComponent<Tile>().nationPlacedObjectsList.Add(this.gameObject);

            nationFound = true;
        }
    }
    
}
