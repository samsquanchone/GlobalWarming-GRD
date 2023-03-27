using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNewGame()
    {
        MenuData.SetGameType(true); //Start new game
        SceneManager.LoadScene(1);
        
    }

    public void LoadGame()
    {
        MenuData.SetGameType(false); //Start new game
        SceneManager.LoadScene(1);
    }
}
