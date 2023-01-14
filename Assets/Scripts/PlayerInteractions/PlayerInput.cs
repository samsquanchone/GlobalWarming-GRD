using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
      //  BuildingManager.instance.MousePressed(); //Run clicked functionality for building trees and factories
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
          
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //If rayhit hits object, output hit variable
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                
                //If the object that is hit is of tag "UNCountry" get the dataScript for the selected country and send necessary parameters to the UIManager to set country pop up data
                if (hit.transform.CompareTag("UNCountry")) 
                {
                    BuildingManager.instance.SpawnBuilding(hit.point);

                    CountryData clickedCountry = hit.collider.gameObject.GetComponent<CountryData>(); //Get correct instance of CountryData
                    UIManager.instance.SetInspected(hit.collider.gameObject);
                    UIManager.instance.DisplayCountryData(clickedCountry.name, clickedCountry.gdp, clickedCountry.population, clickedCountry.tonsOfCo2Produced, clickedCountry.amountOfPykreteProduced, clickedCountry.GetGDPContribution(), clickedCountry.percentageGDPContributed); //Provide clicked countries variable to SetCountry UI in UIManager


                }

            }

        }
       
    }


}






