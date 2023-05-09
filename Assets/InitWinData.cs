using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class InitWinData : MonoBehaviour
{
   int treesPlanted;
   int bergsProduced;
   int shipsPurchased;
   int trainsPurchased;

   [SerializeField] private TMP_Text treeText;
   [SerializeField] private TMP_Text bergText;
   [SerializeField] private TMP_Text shipText;
   [SerializeField] private TMP_Text trainText;

   void Start()
   {
      InitData();
   }

   void InitData()
   {
      SaveGraphData treeData = JSONManager.LoadGraphData("TreesPlantedValues");
      SaveGraphData bergData = JSONManager.LoadGraphData("PykreteValues");

      SavePlayerData playerData = JSONManager.LoadPlayerData("PlayerData");

      //Get values from json deseralized object
      shipsPurchased = playerData.ships;
      trainsPurchased = playerData.trains;
      treesPlanted = (int)SortList(treeData.values); //Add all elements in list and get result
      bergsProduced = (int)SortList(bergData.values); //Add all elements in list and get result

      treeText.text = "Trees Planted: " + treesPlanted.ToString() + " Hectares";
      bergText.text = "Estimated Pykrete Produced: " + bergsProduced.ToString() + " Tons";
      shipText.text = "Ships Pruchased: " + shipsPurchased.ToString();
      trainText.text = "Trains Purchased : " + trainsPurchased.ToString();
      
      
   }
    
    //Little list sorter for adding all values in the data set
   float SortList(List<float> valueSet)
   {
      float calc = 0;
      foreach(float value in valueSet)
      {
        calc += value; 
      }

      return calc;
      
   }


}

