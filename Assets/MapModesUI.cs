using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModesUI : MonoBehaviour
{
    public void ActivatePanel()
    {
        this.gameObject.SetActive(true);
    }

    public void DeActivatePanel()
    {
        this.gameObject.SetActive(false);
    }
}
