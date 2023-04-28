using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSnapShotTrigger : MonoBehaviour
{
    bool shouldTriggerSnapshot = true;
   

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y > 17 && shouldTriggerSnapshot)
        {
            StartSnapShot();
        }
        else if(this.gameObject.transform.position.y < 17 && !shouldTriggerSnapshot)
        {
           StopSnapShot();
        }
        
    }

    void StartSnapShot()
    {
        AudioManager.instance.StartCameraSnapshot();
        shouldTriggerSnapshot = false;
    }

    void StopSnapShot()
    {
       
        AudioManager.instance.StopCameraSnapshot();
        shouldTriggerSnapshot = true;
    }
}
