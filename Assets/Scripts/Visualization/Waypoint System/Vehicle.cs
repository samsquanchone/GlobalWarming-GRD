using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Vehicle : MonoBehaviour
{
    public Node TargetNode;

    public float speed = 1.0f; //
    //Sam add: vars for time state
    float speedNormal = 1.0f;
    float speedFast = 4.0f;

    private void Start()
    {
         //Sam Add: Add functions as listeners to time sytem, so we can pause nav agent, start nav agent and change its speed, based on the state of the time system
        Date_and_Time_System.instance.PlayEvent.AddListener(NormalSpeedVehical);
        Date_and_Time_System.instance.PauseEvent.AddListener(StopVehical);
        Date_and_Time_System.instance.FastforwardEvent.AddListener(FastFowardVehical);
        
        //Sam Add: Checks current time mode when spanws, as event system wont kick in until after user presses a time UI button for the first time
        GetInitialTimeMode();
    }

    private void Update()
    {
        if(TargetNode != null)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, TargetNode.transform.position, step);


            //If target and this are in the same location
            if (Vector3.Distance(transform.position, TargetNode.transform.position) < 0.001f)
            {
                int NewTargetNodeNumber = Random.Range(0, TargetNode.Neighbooring_Nodes.Length); //-1
                try {
                    this.TargetNode = TargetNode.Neighbooring_Nodes[NewTargetNodeNumber];
                }
                catch(Exception e)
                {

                }


                transform.LookAt(TargetNode.transform.position);
            }
        }

        
    }
    
    //Sam addition: functions to set vehicle speed based off time state
    void StopVehical()
    {
       speed = 0;
    }

    void NormalSpeedVehical()
    {
       speed = speedNormal;
    }

    void FastFowardVehical()
    {
       speed = speedFast;
    }

    
    //Sam Addition: uses enum in time system to detect time state when a vehicle spawns
    void GetInitialTimeMode()
    {
        switch(Date_and_Time_System.instance.GetTimeMode())
        {
            case TimeModes.PAUSE:
            StopVehical();
            break;

            case TimeModes.NORMAL:
            NormalSpeedVehical();
            break;

            case TimeModes.FASTFORWARD:
            FastFowardVehical();
            break;
        }
    }
    
    //Sam addition: remove listeners from memory when vehicle destory / game ended
    void OnDestory()
    {
        Date_and_Time_System.instance.PlayEvent.RemoveListener(NormalSpeedVehical);
        Date_and_Time_System.instance.PauseEvent.RemoveListener(StopVehical);
        Date_and_Time_System.instance.FastforwardEvent.RemoveListener(FastFowardVehical);
    }

}
