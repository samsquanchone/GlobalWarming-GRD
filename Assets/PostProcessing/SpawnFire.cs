using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Abstracted class to handle spawning fire vfxs 
public class SpawnFire : MonoBehaviour
{
 

    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Mesh mesh;
    private float y_amount = 5.4f;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    public void SpawnFireOnMesh()
    {
        
        Vector3 randomPoint = GetRandomPointFromMesh();
       
        Debug.Log(randomPoint);

        Instantiate(firePrefab, new Vector3(randomPoint.x, 5.4f, randomPoint.z), Quaternion.identity);

        Debug.Log("Fire Spawned");
    }

    private Vector3 GetRandomPointFromMesh()
    {
        float minx = mesh.bounds.min.x;
        float minz = mesh.bounds.min.z;
        float maxx = mesh.bounds.max.x;
        float maxz = 0.5f;
        float x = Random.Range(minx, maxx);
        float y = y_amount;
        float z = Random.Range(minz, maxz);
        var localPos = new Vector3(x, y, z);
        return transform.TransformPoint(localPos);
    }
}
