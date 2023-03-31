using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    int monthsRemaining;

    [SerializeField] private Material readyToHarvestMat;
    [SerializeField] private GameObject treeCutVFX;
    [SerializeField] private GameObject treeHarvestedVFX;
    Vector3 growthIncrement;

    

    public bool isGrown {get; private set;} = false;
    private int treeYield;
    // Start is called before the first frame update
    void Start()
    {
        //Get components and the trees initial growth time
        TimeManager.instance.activeTreeList.Add(this.GetComponent<TreeGrowth>());
        TimeManager.instance.treesPlanted += 1;
        monthsRemaining = GetComponent<TreeObject>().m_timeToGrow; //Maybe divide this by a heat factor on placed country
        treeYield = GetComponent<TreeObject>().m_yield;

        //Calculate base scale for tree to then implement 
        growthIncrement = new Vector3(this.gameObject.transform.localScale.x / monthsRemaining, this.gameObject.transform.localScale.y / monthsRemaining, this.gameObject.transform.localScale.z / monthsRemaining);
        this.gameObject.transform.localScale = new Vector3(growthIncrement.x, growthIncrement.y, growthIncrement.z);
    }

    public void UpdateGrowthTimer()
    {
        monthsRemaining -= 1;

        if(monthsRemaining > 0)
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + growthIncrement.x, this.gameObject.transform.localScale.y + growthIncrement.y, this.gameObject.transform.localScale.z + growthIncrement.z);
        }
    }

    private void Update()
    {
        if(monthsRemaining <= 0)
        {
            isGrown = true;

            //Get array of all materials, then iterate through size of array and set all mats to HDR tree grown material
            Material[] mats = this.gameObject.GetComponent<Renderer>().materials;
            
            for(int i = 0; i < mats.Length; i++)
            {
                mats[i] = readyToHarvestMat;
                GetComponent<Renderer>().materials = mats;
            }
        }
    }

    public bool IsGrown()
    {
        bool ready = isGrown;

        return ready;
    }

    //Used to get this non persistent value when save is pressed to update the persistent value
    public int GetGrowthTimeRemaining()
    {
        int timeRemaining = monthsRemaining;

        return timeRemaining;
    }

    public void HarvestTree()
    {
        GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().AddToUnprocessedWoodStockPile(treeYield);
        Instantiate(treeCutVFX, this.transform.position, treeCutVFX.transform.rotation);
        TreeReplantManager.instance.ReplantTree(this.gameObject.GetComponent<SaveableObject>().objectType, this.gameObject.transform);
    }

    private void OnDestroy()
    {
        //Remove tree from active growing trees list when game ended or the tree is harvested 
        TimeManager.instance.activeTreeList.Remove(this.GetComponent<TreeGrowth>());
        Instantiate(treeHarvestedVFX, this.transform.position, treeHarvestedVFX.transform.rotation);
        
    }
}
