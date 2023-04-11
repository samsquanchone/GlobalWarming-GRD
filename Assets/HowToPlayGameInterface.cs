using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayGameInterface : MonoBehaviour
{
    [SerializeField] private GameObject playerStatisticsInfoPanel;
    [SerializeField] private GameObject treePlantationsInfoPanel;
    [SerializeField] private GameObject lumbermillAndTeraFactoryInfoPanel;
    [SerializeField] private GameObject LogisticsInfoPanel;
    [SerializeField] private GameObject graphInfoPanel;
    [SerializeField] private GameObject timeLapsedInfoPanel;
    [SerializeField] private GameObject mapModesInfoPanel;
    [SerializeField] private GameObject nationsInfoPanel;

    public void PlayerStatsOnMoreInfoClick()
    {
        playerStatisticsInfoPanel.gameObject.SetActive(!playerStatisticsInfoPanel.gameObject.activeSelf);
    }

    public void TreePlantationsOnMoreInfoClick()
    {
        treePlantationsInfoPanel.gameObject.SetActive(!treePlantationsInfoPanel.gameObject.activeSelf);
    }
    public void LumbermillAndTeraFactoryOnMoreInfoClick()
    {
        lumbermillAndTeraFactoryInfoPanel.gameObject.SetActive(!lumbermillAndTeraFactoryInfoPanel.gameObject.activeSelf);
    }

    public void LogisticsOnMoreInfoClick()
    {
        LogisticsInfoPanel.gameObject.SetActive(!LogisticsInfoPanel.gameObject.activeSelf);
    }

    public void GraphStatisticsOnMoreInfoClick()
    {
        graphInfoPanel.gameObject.SetActive(!graphInfoPanel.gameObject.activeSelf);
    }

    public void TimeLapsedOnMoreInfoClick()
    {
        timeLapsedInfoPanel.gameObject.SetActive(!timeLapsedInfoPanel.gameObject.activeSelf);
    }

    public void MapModesOnMoreInfoClick()
    {
        mapModesInfoPanel.gameObject.SetActive(!mapModesInfoPanel.gameObject.activeSelf);
    }

    public void NationsOnMoreInfoClick()
    {
        nationsInfoPanel.gameObject.SetActive(!nationsInfoPanel.gameObject.activeSelf);
    }
}
