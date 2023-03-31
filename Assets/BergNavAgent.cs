using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergNavAgent : MonoBehaviour
{
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
        if(shouldMove)
        navMeshAgent.destination = location.position;
    }
}
