using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergNavAgent : MonoBehaviour
{
    [SerializeField] GameObject boat;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    [SerializeField] Transform location;
    public bool shouldMove = false;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        

        navMeshAgent.destination = location.position;

        if(navMeshAgent.remainingDistance == 0)
        {
            Destroy(boat);
        }
    }
}
