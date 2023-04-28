using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;

    //Scene 0 Main Menu Scene Functions
    public void PlayNewGame()
    {
        MenuData.SetGameType(true); //Start new game
        SceneManager.LoadScene(5);
    }

    public void LoadGame()
    {
        MenuData.SetGameType(false); //Start new game
        SceneManager.LoadScene(5);
    }

    public void soundMenuOnButtonClick()
    {
        soundMenu.gameObject.SetActive(!soundMenu.gameObject.activeSelf);
    }

    public void HowToPlayGame()
    {
        MenuData.SetGameType(false); //Start new game
        SceneManager.LoadScene(1);
    }

    //Scene 1 How To Play Scene 1 Functions
    public void BackToMainMenu()
    {
        
        SceneManager.LoadScene(0);
    }

    public void LoadHowToPlayGameScene2()
    {
        
        SceneManager.LoadScene(2);
    }

    //Scene 2 How To Play Scene 2 Functions
    public void BackToHowToPlayGameScene1()
    {
        
        SceneManager.LoadScene(1);
    }

    public void LoadHowToPlayGameScene3()
    {
        
        SceneManager.LoadScene(3);
    }

    //Scene 3 How To Play Scene 3 Functions
    public void BackToHowToPlayGameScene2()
    {
        
        SceneManager.LoadScene(2);
    }

    public void LoadHowToPlayGameScene4()
    {
       
        SceneManager.LoadScene(4);
    }

    //Scene 4 How To Play Scene 4 Functions
    public void BackToHowToPlayGameScene3()
    {
        
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
