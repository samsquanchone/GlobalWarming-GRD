using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameStateManager : MonoBehaviour
{
    public void PlayNewGame()
    {
        MenuData.SetGameType(true); //Start new game
        SceneManager.LoadScene(5);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        GameWonAudio.Instance.StopGameWonAudio();
    }

    public void QuitGame()
    {
        GameWonAudio.Instance.StopGameWonAudio();
        Application.Quit();
        
    }
}