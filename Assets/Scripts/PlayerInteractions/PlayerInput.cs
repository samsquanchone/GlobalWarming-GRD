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


        //CountryData clickedCountry = hit.collider.gameObject.GetComponent<CountryData>(); //Get correct instance of CountryData
        // UIManager.instance.SetInspected(hit.collider.gameObject);
        //  UIManager.instance.DisplayCountryData(clickedCountry.name, clickedCountry.gdp, clickedCountry.population, clickedCountry.tonsOfCo2Produced, clickedCountry.amountOfPykreteProduced, clickedCountry.GetGDPContribution(), clickedCountry.percentageGDPContributed); //Provide clicked countries variable to SetCountry UI in UIManager

        //  BuildingManager.instance.MousePressed(); //Run clicked functionality for building trees and factories
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            var objHit = PlayerRayCast();

            if (objHit.transform.CompareTag("Tree"))
            {
                UIManager.instance.SetObjectUI(objHit.transform.gameObject);
            }


        }

        //  BuildingManager.instance.MousePressed(); //Run clicked functionality for building trees and factories
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            var objHit = PlayerRayCast();

            //If the object that is hit is of tag "UNCountry" get the dataScript for the selected country and send necessary parameters to the UIManager to set country pop up data
            if (objHit.transform.CompareTag("UNCountry"))
            {
                BuildingManager.instance.SpawnBuilding(objHit.point);

            }

        }
    }

    private RaycastHit PlayerRayCast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //If rayhit hits object, output hit variable
        if (Physics.Raycast(ray, out hit, 10000f))
        {
            return hit;
        }


        return hit;

    }

}







