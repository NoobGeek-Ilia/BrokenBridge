using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SStageMonitor : MonoBehaviour
{
    public TextMeshPro currStagesTxt;
    public GameObject[] StageCircles;
    [SerializeField] StateMonitor stateMonitor;
    private void Start()
    {
        SetStageCircles();
        InitSatge();
    }
    internal protected void InitSatge()
    {
        //on first platform
        currStagesTxt.text = (stateMonitor.currentStageIndex + 1).ToString();

        //fill finished stage circles
        for (int i = 0; i < stateMonitor.currentStageIndex + 1; i++)
        {
            Image cirlePic = StageCircles[i].GetComponent<Image>();
            cirlePic.color = Color.green;
        }
    }
    private void SetStageCircles()
    {
        //stage cirles
        for (int i = 0; i < StateMonitor.stages[SBoxPanel.SelectedLevel]; i++)
        {
            StageCircles[i].SetActive(true);
        }
    }
}
