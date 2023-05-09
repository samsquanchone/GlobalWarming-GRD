using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameLoaderWinState : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private float delayTime = 5f;
    AsyncOperation loadingOperation;
    // Start is called before the first frame update
    void Start()
    {
        
       StartCoroutine("loadScene");
        
    }

    IEnumerator loadScene()
    {
      loadingOperation = SceneManager.LoadSceneAsync(8, LoadSceneMode.Single);
      loadingOperation.allowSceneActivation = false;

      while(!loadingOperation.isDone)
      {
            loadingText.text = "Loading: " + Mathf.Round(loadingOperation.progress * 100) + "%";

            yield return new WaitForSeconds(3f);
               
            StartCoroutine("DelayScene");
      } 
    } 

    IEnumerator DelayScene()
    {

      yield return new WaitForSeconds(delayTime);
      
      loadingOperation.allowSceneActivation = true;
    }

    void OnDestroy()
    {
       Debug.Log("Stop menu music");
       MenuAudioManager.instance.StopMenuMusic();
    }

    

}


