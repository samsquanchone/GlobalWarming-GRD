using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simplfy2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var originalMesh = GetComponent<MeshFilter>().sharedMesh;
        float quality = 50;
        var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
        meshSimplifier.Initialize(originalMesh);
        meshSimplifier.SimplifyMesh(quality);
        var destMesh = meshSimplifier.ToMesh();
        GetComponent<MeshFilter>().sharedMesh = destMesh;

    }
}
