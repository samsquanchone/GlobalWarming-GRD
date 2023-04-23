using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergNavAgent : MonoBehaviour
{
    [SerializeField] GameObject boat;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    [SerializeField] Transform location;
    public bool shouldMove = false;
    float offSet;

    [SerializeField] private float speedNormal;
    [SerializeField] private float speedFast;


    [SerializeField] private GameObject berg;

    // Start is called before the first frame update
    void Start()
    {
        
        offSet = NavMeshManager.instance.GetBergOffset(); //Increase berg offset
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = offSet; //Get saved stopping distance

          
        //Add functions as listeners to time sytem, so we can pause nav agent, start nav agent and change its speed, based on the state of the time system
        Date_and_Time_System.instance.PlayEvent.AddListener(NormalSpeedNavAgent);
        Date_and_Time_System.instance.PauseEvent.AddListener(StopNavAgent);
        Date_and_Time_System.instance.FastforwardEvent.AddListener(FastFowardNavAgent);
        
        //Checks current time mode when spanws, as event system wont kick in until after user presses a time UI button for the first time
        GetInitialTimeMode();

    
    }

    // Update is called once per frame
    void Update()
    {
        
     
        navMeshAgent.destination = location.position;
        Debug.Log("Remaining distance" + navMeshAgent.remainingDistance);

       if(navMeshAgent.remainingDistance <= 0 + NavMeshManager.instance.GetBergOffset() && !navMeshAgent.pathPending)
       {
           Debug.Log(NavMeshManager.instance.GetBergOffset());  
           NavMeshManager.instance.IncreaseBergOffset(); //Increase berg offset
           SpawnBerg();
       }
    }

    void StopNavAgent()
    {
       navMeshAgent.speed = 0;
    }

    void NormalSpeedNavAgent()
    {
       navMeshAgent.speed = speedNormal;
    }

    void FastFowardNavAgent()
    {
       navMeshAgent.speed = speedNormal;
    }

    void SpawnBerg()
    {
        Vector3 spawnPos = new Vector3 (this.transform.position.x, this.transform.position.y - 0.7f, this.transform.position.z);
        Instantiate(berg, spawnPos, berg.transform.rotation);
        Destroy(this.gameObject);
    }

    void GetInitialTimeMode()
    {
        switch(Date_and_Time_System.instance.GetTimeMode())
        {
            case TimeModes.PAUSE:
            StopNavAgent();
            break;

            case TimeModes.NORMAL:
            NormalSpeedNavAgent();
            break;

            case TimeModes.FASTFORWARD:
            FastFowardNavAgent();
            break;
        }
    }

    void OnDestory()
    {
        Date_and_Time_System.instance.FastforwardEvent.RemoveListener(NormalSpeedNavAgent);
        Date_and_Time_System.instance.PauseEvent.RemoveListener(StopNavAgent);
        Date_and_Time_System.instance.FastforwardEvent.RemoveListener(FastFowardNavAgent);

    }
}
