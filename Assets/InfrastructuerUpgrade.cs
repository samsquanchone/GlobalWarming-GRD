using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfrastructureType{Lumbermill, TrainStation, Dock}
public class InfrastructuerUpgrade : MonoBehaviour
{
    public InfrastructureType infrastuctureType;
    [SerializeField] private int upgradeCost;
    

    public void UpgradeInfrastructure()
    {
        //Check if enough money
        if(CanUpgrade(upgradeCost))
        {
           Player.instance.RemoveAmountFromPlayerWealth(upgradeCost);
           gameObject.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().UpgradeInfrastructure(infrastuctureType);
           AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.infrastructureUpgraded, null);
        }
    }

    private bool CanUpgrade(int cost)
    {
        if(Player.instance.GetPlayerWealth() >= cost)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    
}
