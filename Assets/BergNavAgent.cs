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

    
    }

    // Update is called once per frame
    void Update()
    {
        

       navMeshAgent.destination = location.position;
       Debug.Log("Remaining distance" + navMeshAgent.remainingDistance);
       if(navMeshAgent.remainingDistance <= 0 + NavMeshManager.instance.GetBergOffset() && !navMeshAgent.pathPending && Date_and_Time_System.instance.timeMode != TimeModes.PAUSE)
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
        Instantiate(berg, this.transform.position, berg.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnDestory()
    {
        Date_and_Time_System.instance.FastforwardEvent.RemoveListener(NormalSpeedNavAgent);
        Date_and_Time_System.instance.PauseEvent.RemoveListener(StopNavAgent);
        Date_and_Time_System.instance.FastforwardEvent.RemoveListener(FastFowardNavAgent);

    }
}
