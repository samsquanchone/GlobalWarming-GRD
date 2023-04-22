using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameStateManager : MonoBehaviour
{
    public VisualEffect fireworksVfx;

    private void Start()
    {
        fireworksVfx.Play();
    }

    public void PlayNewGame()
    {
        MenuData.SetGameType(true); //Start new game
        SceneManager.LoadScene(5);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
