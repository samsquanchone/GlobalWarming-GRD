using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfrastructureType{Lumbermill, TrainStation, Dock}
public class InfrastructuerUpgrade : MonoBehaviour
{
    public InfrastructureType infrastuctureType;
    

    public void UpgradeInfrastructure()
    {
        //Check if enough money
        gameObject.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().UpgradeInfrastructure(infrastuctureType);
        AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.infrastructureUpgraded, null);
    }
    
}
