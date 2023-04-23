using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{ 
    [SerializeField] public Node[] Neighbooring_Nodes;
    [SerializeField] public GameObject lineRenderer;

    [SerializeField]  List<GameObject> Created_Renderers = new List<GameObject>();


    private void Start()
    {
        Show_Connections();
    }
    public void Show_Connections()
    {

        
        if(Neighbooring_Nodes != null)
        {
            for (int i = 0; i < Neighbooring_Nodes.Length; i++)
            {
                GameObject InstanciatedLinerenderer = Instantiate(lineRenderer, this.transform.position, Quaternion.identity);

                LineRenderer rend = InstanciatedLinerenderer.GetComponent<LineRenderer>();

                rend.positionCount = 2;

                rend.SetPosition(0, this.transform.position);
                Vector3 HalvedPosition = Vector3.Lerp(this.transform.position, Neighbooring_Nodes[i].transform.position, 0.5f);
                rend.SetPosition(1, HalvedPosition);

                Created_Renderers.Add(InstanciatedLinerenderer);
            }
        }
    }

    public void Disable_Connections()
    {
        if(Created_Renderers != null)
        {
            for (int i = 0; i < Created_Renderers.Count; i++)
            {
                Destroy(Created_Renderers[i]);
            }
        }
    }
}
