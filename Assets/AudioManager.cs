using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{   
    //Singleton
    public static AudioManager instance => m_instance;
    private static AudioManager m_instance;
    
    //Used to contain one shot references so can access them though singleton, so the amount of sfx references doesnt clock this audio manager for looping / ambience events
    public UIReferences uiRefs;
    public ObjectReferences objectRefs;

    [SerializeField] private EventReference gameMusicReference;
    EventInstance gameMusicInstance;

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        StartGameMusic();
    }

     void StartGameMusic()
   {
        gameMusicInstance =  FMODUnity.RuntimeManager.CreateInstance(gameMusicReference);
        
        gameMusicInstance.start();
      
   }

   public void StopMenuMusic()
   {
       gameMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       gameMusicInstance.release();
   }

    

}
