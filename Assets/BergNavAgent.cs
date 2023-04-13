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
    // Start is called before the first frame update
    void Start()
    {
        
        offSet = NavMeshManager.instance.GetBergOffset(); //Increase berg offset
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = offSet; //Get saved stopping distance
    }

    // Update is called once per frame
    void Update()
    {
        

       navMeshAgent.destination = location.position;

       if(navMeshAgent.remainingDistance == 0)
       {
           Debug.Log(NavMeshManager.instance.GetBergOffset());  
           NavMeshManager.instance.IncreaseBergOffset(); //Increase berg offset
       }
    }
}
