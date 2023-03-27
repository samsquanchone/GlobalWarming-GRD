using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject howToPlayMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenu.gameObject.SetActive(true);
        }
    }

    public void disableEscapeMenuButton()
    {
        escapeMenu.SetActive(false);
    }

    public void soundMenuOnButtonClick()
    {
        soundMenu.gameObject.SetActive(!soundMenu.gameObject.activeSelf);
    }

    public void howToPlayMenuOnButtonClick()
    {
        howToPlayMenu.gameObject.SetActive(!howToPlayMenu.gameObject.activeSelf);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
