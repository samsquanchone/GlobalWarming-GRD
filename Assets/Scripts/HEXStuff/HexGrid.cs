using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, HexManager> hexTileDict = new Dictionary<Vector3Int, HexManager>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach (HexManager hex in FindObjectsOfType<HexManager>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }

        
    }

    public HexManager GetTileAt(Vector3Int hexCoordinates)
    {
        HexManager result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeightboursFor(Vector3Int hexCoordinates)
    {
        if (hexTileDict.ContainsKey(hexCoordinates) == false)
            return new List<Vector3Int>(); //Return empty list if does not contan key

        if (hexTileNeighboursDict.ContainsKey(hexCoordinates)) //return cached values if key is found
            return hexTileNeighboursDict[hexCoordinates];

        hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>()); //Add hex coordinates to neighbours dictionary

        foreach (var direction in Direction.GetDirectionList(hexCoordinates.z)) //Get direction from list of directions and pass row of coordinates
        {
            if (hexTileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + direction);
            }
        }
        return hexTileNeighboursDict[hexCoordinates];


    }

}

public static class Direction
{
    public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1, 0, 1),  //N1
        new Vector3Int(0, 0, 1),   //N2
        new Vector3Int(1, 0, 0),   //E
        new Vector3Int(0, 0, -1),  //S2
        new Vector3Int(-1, 0, -1), //S1
        new Vector3Int(-1, 0, 0),  //W

    };

    public static List<Vector3Int> directionOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1),   //N1
        new Vector3Int(1, 0, 1),   //N2
        new Vector3Int(1, 0, 0),   //E
        new Vector3Int(1, 0, -1),  //S2
        new Vector3Int(0, 0, -1),  //S1
        new Vector3Int(-1, 0, 0),  //W
    };

    public static List<Vector3Int> GetDirectionList(int z)
        => z % 2 == 0 ? directionOffsetEven : directionsOffsetOdd;
    
}