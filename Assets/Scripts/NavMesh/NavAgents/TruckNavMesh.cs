using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TruckNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      //  target = NavMeshManager.instance.GetDestination();

       // navMeshAgent.destination = target.position;

        

    }

    void OnCollisionEnter(Collision collision)
    {
       

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Dock(Clone)")
        {
            // Target reached
            Debug.Log("reached target");

            Destroy(this.gameObject);
        }

    }

}
