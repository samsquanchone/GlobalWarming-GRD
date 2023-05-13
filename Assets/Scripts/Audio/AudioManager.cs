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
    public EventInstance gameMusicInstance {get; private set;}

    [SerializeField] private EventReference oceanAmbienceReference;
    public EventInstance oceanAmbienceInstance {get; private set;}

    [SerializeField] private EventReference windAmbienceReference;
    public EventInstance windAmbienceInstance {get; private set;}


    [SerializeField] private EventReference cameraHighSnapShotReference;
    public EventInstance cameraHighSnapShotInstance {get; private set;}

    [SerializeField] private EventReference gameLostMusicReference;
    public EventInstance gameLostMusicInstance {get; private set;}

      [SerializeField] private EventReference gameLostFireReference;
    public EventInstance gameLostFireInstance {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        StartGameMusic();
        StartAmbience();
    }

    public void StartCameraSnapshot()
    {
        cameraHighSnapShotInstance = FMODUnity.RuntimeManager.CreateInstance(cameraHighSnapShotReference);
        cameraHighSnapShotInstance.start();
    }
    public void StopCameraSnapshot()
    {
        cameraHighSnapShotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    void StartGameMusic()
    {
        gameMusicInstance =  FMODUnity.RuntimeManager.CreateInstance(gameMusicReference);
        
        gameMusicInstance.start();
      
    }

    public void StopGameMusic()
    {
        gameMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameMusicInstance.release();
    }

    public void StartLostGameMusic()
    {
        StopGameMusic();
        gameLostMusicInstance = FMODUnity.RuntimeManager.CreateInstance(gameLostMusicReference);
        gameLostFireInstance = FMODUnity.RuntimeManager.CreateInstance(gameLostFireReference);
        gameLostMusicInstance.start();
        gameLostFireInstance.start();
    }
    
    public void StopLostGameMusic()
    {
        
        gameLostMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameLostFireInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        gameLostMusicInstance.release();
        gameLostFireInstance.release();
    }

    public void StopMenuMusic()
    {
       gameMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       gameMusicInstance.release();
       
    }

    void StartAmbience()
    {
       oceanAmbienceInstance =  FMODUnity.RuntimeManager.CreateInstance(oceanAmbienceReference);
       windAmbienceInstance =  FMODUnity.RuntimeManager.CreateInstance(windAmbienceReference);

       oceanAmbienceInstance.start();
       windAmbienceInstance.start();

       oceanAmbienceInstance.release();
       windAmbienceInstance.release();
    }

    void StopAmbience()
    {
       oceanAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       oceanAmbienceInstance.release();

       windAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       windAmbienceInstance.release();
    }

    void OnDestroy()
    {
        StopGameMusic();
        StopAmbience();
        StopLostGameMusic();
    }


    

}
