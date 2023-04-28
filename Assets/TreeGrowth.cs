using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    [SerializeField] public int optimumTemperature;
   // private const float heatPenalty = 1.4f;
    [SerializeField]public float monthsRemaining;
    [SerializeField] float initialGrowthTime;
    [SerializeField] private Material readyToHarvestMat;
    [SerializeField] private GameObject treeCutVFX;
    
    Vector3 growthIncrement;

    [SerializeField] ObjectNationInteraction nationInteraction;

    

    public bool isGrown {get; private set;} = false;
    private int treeYield;

    [SerializeField] private float harvestCost;
    // Start is called before the first frame update
    void Start()
    {
        //Get components and the trees initial growth time
        TimeManager.instance.activeTreeList.Add(this.GetComponent<TreeGrowth>());
        TimeManager.instance.treesPlanted += 1;
        monthsRemaining = GetComponent<TreeObject>().m_timeToGrow; //Maybe divide this by a heat factor on placed country
        treeYield = GetComponent<TreeObject>().m_yield;

        
        
        //Calculate harvest/replant cost: 10% of tree project cost
        harvestCost = (GetComponent<TreeObject>().m_cost * 10) / 100;
        
        if(monthsRemaining == GetComponent<TreeObject>().m_timeToGrow) //Stop from changing scale of objects on load game
        {
            //Calculate base scale for tree to then implement 
            growthIncrement = new Vector3(this.gameObject.transform.localScale.x / monthsRemaining, this.gameObject.transform.localScale.y / monthsRemaining, this.gameObject.transform.localScale.z / monthsRemaining);
            this.gameObject.transform.localScale = new Vector3(growthIncrement.x, growthIncrement.y, growthIncrement.z);
        }
        

        
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
    public float GetGrowthTimeRemaining()
    {
        float timeRemaining = monthsRemaining;

        return timeRemaining;
    }

    public void HarvestTree()
    {   
        //If player wealth is equal to or more than 10% the cost of the tree harves and replant tree project for automated planted, for more global progression
        if(Player.instance.GetPlayerWealth() >= harvestCost)
        {
            GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().AddToUnprocessedWoodStockPile(treeYield);
            Player.instance.RemoveAmountFromPlayerWealth((int) harvestCost);
            Instantiate(treeCutVFX, this.transform.position, treeCutVFX.transform.rotation);
            TreeReplantManager.instance.ReplantTree(this.gameObject.GetComponent<SaveableObject>().objectType, this.gameObject.transform);
            AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.treeHarvestedPurchased, null);
           
        }
    }

    private void OnDestroy()
    {
        //Remove tree from active growing trees list when game ended or the tree is harvested 
        TimeManager.instance.activeTreeList.Remove(this.GetComponent<TreeGrowth>());
        
        
    }
}
