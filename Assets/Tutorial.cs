using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private GameObject currentTutorialWindow;
    private GameObject nextTutorialWindow;

    [SerializeField] private List<GameObject> tutorialList;

    public void ShowNextTutorialWindow()
    {
        currentTutorialWindow = tutorialList[0];
        nextTutorialWindow = tutorialList[1];

        currentTutorialWindow.SetActive(false);
  
        if (tutorialList.Count != 0) {
            tutorialList.Remove(currentTutorialWindow);
            nextTutorialWindow.SetActive(true);
        }

    }

    public void DisableTutorial()
    {
        foreach (var tutorial in tutorialList)
        {
            tutorial.gameObject.SetActive(false);
        }
    }

}
