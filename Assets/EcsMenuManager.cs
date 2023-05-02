using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EcsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject howToPlayMenu;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenu.gameObject.SetActive(true);
        }
    }

    public void DisableEscapeMenuButton()
    {
        escapeMenu.SetActive(false);
    }
    
    //Sam add// going to reload the scene on a load button press in game
    public void LoadGame()
    {
        MenuData.SetGameType(false);
        SceneManager.LoadScene(5);
    }

    public void SoundMenuOnButtonClick()
    {
        soundMenu.gameObject.SetActive(!soundMenu.gameObject.activeSelf);
    }

    public void HowToPlayMenuOnButtonClick()
    {
        howToPlayMenu.gameObject.SetActive(!howToPlayMenu.gameObject.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
