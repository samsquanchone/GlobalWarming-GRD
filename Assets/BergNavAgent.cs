using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BergSize {Small, Medium, Large};

public class BergNavAgent : MonoBehaviour
{
    public BergSize bergSize;
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
        
        offSet = NavMeshManager.Instance.GetBergOffset(); //Increase berg offset
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = offSet; //Get saved stopping distance

          
        //Add functions as listeners to time sytem, so we can pause nav agent, start nav agent and change its speed, based on the state of the time system
        Date_and_Time_System.instance.PlayEvent.AddListener(NormalSpeedNavAgent);
        Date_and_Time_System.instance.PauseEvent.AddListener(StopNavAgent);
        Date_and_Time_System.instance.FastforwardEvent.AddListener(FastFowardNavAgent);

        
        //Increment music param
        FmodParameters.IncrementBergsFmodParam(AudioManager.instance.gameMusicInstance);
        //Checks current time mode when spanws, as event system wont kick in until after user presses a time UI button for the first time
        GetInitialTimeMode();

    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
     
        navMeshAgent.destination = location.position;
       


       if(navMeshAgent.remainingDistance <= 0 + NavMeshManager.Instance.GetBergOffset() && !navMeshAgent.pathPending)
       {
           
           NavMeshManager.Instance.IncreaseBergOffset(); //Increase berg offset
           SpawnBerg(this.bergSize);
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
       navMeshAgent.speed = speedFast;
    }

    void SpawnBerg(BergSize size)
    {
        Vector3 spawnPos = new Vector3 (this.transform.position.x, this.transform.position.y - 0.7f, this.transform.position.z);
        Instantiate(berg, spawnPos, berg.transform.rotation);

        switch(size)
        {
            case (BergSize.Small):
            PoolManager.instance.CoolObject(this.gameObject, PoolingObjectType.BergSmall);
            break;

            case (BergSize.Medium):
            PoolManager.instance.CoolObject(this.gameObject, PoolingObjectType.BergMed);
            break;

            case (BergSize.Large):
            PoolManager.instance.CoolObject(this.gameObject, PoolingObjectType.BergLarge);
            break;
        }
       // Destroy(this.gameObject);
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
