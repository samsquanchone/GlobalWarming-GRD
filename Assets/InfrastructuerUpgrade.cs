using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfrastructureType{Lumbermill, TrainStation, Dock}
public class InfrastructuerUpgrade : MonoBehaviour
{
    public InfrastructureType infrastuctureType;
    [SerializeField] private int upgradeCost;
    [SerializeField] private GameObject upgadeVFX;
    

    public void UpgradeInfrastructure()
    {
        //Check if enough money
        if(CanUpgrade(upgradeCost))
        {
           Player.instance.RemoveAmountFromPlayerWealth(upgradeCost);
           gameObject.GetComponent<ObjectNationInteraction>().nation.GetComponent<Tile>().UpgradeInfrastructure(infrastuctureType);
           Instantiate(upgadeVFX, gameObject.transform.position, upgadeVFX.transform.rotation);
           AudioPlayback.PlayOneShot(AudioManager.instance.objectRefs.infrastructureUpgraded, null);
           StartCoroutine(WaitVFXDuration());
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
            UIHoverManager.instance.ShowTip("Insufficient Funds", Input.mousePosition);
            return false;
        }
    }

    private IEnumerator WaitVFXDuration()
    {
        yield return new WaitForSeconds(3.5f);
        Debug.Log("Upgrade");
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 0.1f, this.gameObject.transform.localScale.y + 0.1f, this.gameObject.transform.localScale.z + 0.2f);
    }
    
}
