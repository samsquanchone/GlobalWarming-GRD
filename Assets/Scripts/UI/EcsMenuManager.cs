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
        StartCoroutine(TransitionScene(0));
        //SceneManager.LoadScene(0);
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
        StartCoroutine(TransitionScene(5));
        //SceneManager.LoadScene(5);
    }

    public void SoundMenuOnButtonClick()
    {
        soundMenu.gameObject.SetActive(!soundMenu.gameObject.activeSelf);
    }

    public void HowToPlayMenuOnButtonClick()
    {
        howToPlayMenu.gameObject.SetActive(!howToPlayMenu.gameObject.activeSelf);
    }

    //Sam: quick end addition: having a lot in scene was quite heavy when changing scene, just trying to force a bit of GC to make it less heavy 
    IEnumerator TransitionScene(int sceneNumber)
    {
        yield return new WaitForSeconds(0.3f); //Give a bit of time before GC, j incase save then quick load could issue 
        //Do GC before changing scene, if we try to change scene with loads of stuff in scene, can cause some pcs to crash
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    { 
        //Do a bit of gc beofore quit, incase a lot in scene could cause crash
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

        Application.Quit();
    }

}
