using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is used to Set win state or loss state

public class GameManager : MonoBehaviour
{
   public static GameManager instance => m_instance;
   private static GameManager m_instance;

   void Start()
   {
      m_instance = this;
   }
   public void GameLost()
   {
       
      Debug.Log("Game Lost");
      
         
   }

   public void GameWon()
   {
      Debug.Log("Game won");
      SceneManager.LoadScene(7);
      
   }
}
