using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MenuAudioManager : MonoBehaviour
{
    //Singleton deceleration
    public static MenuAudioManager instance => m_instance;
    private static MenuAudioManager m_instance;

    [SerializeField] private EventReference menuMusic;
    EventInstance menuMusicInstance;

    //As audio is not a primary development focus, just a flourish for polish. References for menu will be declared hear. As will only be a handful of sounds for main menu

    // Start is called before the first frame update
    void Start()
    {
        //Singleton initlialization
        m_instance = this;
        StartMenuMusic();
        
    }

   void StartMenuMusic()
   {
        menuMusicInstance =  FMODUnity.RuntimeManager.CreateInstance(menuMusic);
        
        menuMusicInstance.start();
      
   }

   public void StopMenuMusic()
   {
       menuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       menuMusicInstance.release();
   }
   
}
