using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask selectionMask;

    public HexGrid hexGrid;

    List<Vector3Int> neighbours = new List<Vector3Int>();

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;

        if (FindTarget(mousePosition, out result))
        {
            HexManager selectedHex = result.GetComponent<HexManager>();
            
            selectedHex.DisableHighlight();
            selectedHex.EnableHighlight();

            //To reset highlight use a stack, when this function is called Disable the selected hex that was on stack then pop it off the stack , then add the new selected tile to the stack then enable the highlight



            //THIS IS FOR NEIGHBOURS SELECT, WITH CURRENT IMPLEMENTATION WOULD NEED TO BE REFINED
            /*  foreach (Vector3Int neighbour in neighbours) 
              {
                  hexGrid.GetTileAt(neighbour).DisableHighlight();
              }
              neighbours = hexGrid.GetNeightboursFor(selectedHex.HexCoords);

              foreach (Vector3Int neighbour in neighbours)
              {
                  hexGrid.GetTileAt(neighbour).EnableHighlight();
              }

              Debug.Log($"Neightbours for {selectedHex.HexCoords} are: ");
              foreach (Vector3Int neighbourPos in neighbours)
              {
                  Debug.Log(neighbourPos);
              }
            */


        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }
}
