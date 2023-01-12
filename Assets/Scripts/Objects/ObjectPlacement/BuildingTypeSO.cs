using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject
{
    public Transform buildingPrefab;
    public Transform constructionPrefab;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        //Potentially start building timer/ animations here 
    }

    // Update is called once per frame
    void Update()
    {
        //Any neccessary calculations can be added here, or in individual functions. For example, cut tree could trigger animation and add pykrete amount to country
    }
}
