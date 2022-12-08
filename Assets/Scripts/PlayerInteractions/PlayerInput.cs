using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //If rayhit hits object, output hit variable
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                //Debug.Log("hello");
                //If the object that is hit is of tag "City" get the values for the houseByPriceRegion dictionary (from DataManager instance) for the current slider UI year value
                if (hit.transform.CompareTag("UNCountry") && !UIManager.instance.countryPanel.activeSelf) 
                {

                    CountryData clickedCountry = hit.collider.gameObject.GetComponent<CountryData>(); //Get correct instance of CountryData
                    UIManager.instance.SetInspected(hit.collider.gameObject);
                    UIManager.instance.DisplayCountryData(clickedCountry.name, clickedCountry.gdp, clickedCountry.population, clickedCountry.tonsOfCo2Produced, clickedCountry.amountOfPykreteProduced, clickedCountry.GetGDPContribution(), clickedCountry.percentageGDPContributed); //Provide clicked countries variable to SetCountry UI in UIManager


                }

            }

        }
    }
}







