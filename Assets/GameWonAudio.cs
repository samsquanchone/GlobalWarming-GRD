using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class GameWonAudio : MonoBehaviour
{
    public static GameWonAudio Instance => m_instance;
    private static GameWonAudio m_instance;

    [SerializeField] private EventReference gameWonReference;
    public EventInstance gameWonInstance {get; private set;}
    
    void Start()
    {
        m_instance = this;
        StartGameWonAudio();
    }

    void StartGameWonAudio()
    {
        gameWonInstance =  FMODUnity.RuntimeManager.CreateInstance(gameWonReference);
        
        gameWonInstance.start();
      
    }

    public void StopGameWonAudio()
    {
        gameWonInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameWonInstance.release();
    }

    void OnDestory()
    {
        StopGameWonAudio();
    }
}
