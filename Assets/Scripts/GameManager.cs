using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is used to Set win state or loss state

public class GameManager : MonoBehaviour
{
   public static GameManager instance => m_instance;
   private static GameManager m_instance;
   [SerializeField] private GameObject gameLostPanel;

   [SerializeField] private GameObject fireVFX; //Wanted the fire as a game mechanic but we had issues with country mesh not being generated in blender, so will have to just be used as a game over visual :(
    Nation[] All_Nations; 

   void Start()
   {
      m_instance = this;
      All_Nations = GameObject.FindObjectsOfType<Nation>(); //Get all nations
   }
   public void GameLost()
   { 
      Debug.Log("Lost");
      Debug.Log("Game Lost");
      Date_and_Time_System.instance.Stop_Speed();
      GameLostVisualization();
      gameLostPanel.gameObject.SetActive(true);
      AudioManager.instance.StartLostGameMusic();
   }

   public void GameWon()
   {
      Debug.Log("Game won");
      SceneManager.LoadScene(7);
   }

   private void GameLostVisualization()
   {
      for (int i = 0; i < 20; i++)
      {  
         //Get center of mesh for all nations to spawn fire
         Vector3 Mesh_Center = new Vector3(All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.x, All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.y + 0.5f, All_Nations[i].Nations_Territories[0].GetComponent<Collider>().bounds.center.z + 0.5f);
         Instantiate(fireVFX, Mesh_Center, fireVFX.transform.rotation); //Spawn fire vfx on every nation
      }
   }
}
