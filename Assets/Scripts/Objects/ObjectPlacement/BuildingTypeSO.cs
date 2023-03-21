using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject
{
    public Transform buildingPrefab;
    public Transform constructionPrefab;
    public Sprite sprite;
    public int cost;
    
}
