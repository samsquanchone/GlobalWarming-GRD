using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Vehicle : MonoBehaviour
{
    public Node TargetNode;

    public float speed = 1.0f;

    private void Start()
    {
        
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
                int NewTargetNodeNumber = Random.Range(0, TargetNode.Neighbooring_Nodes.Length - 1);
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
}
