using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SStageMonitor : MonoBehaviour
{
    public TextMeshPro currStagesTxt;
    public GameObject[] StageCircles;
    private void Start()
    {
        SetStageCircles();
        InitSatge();
    }
    private void OnEnable()
    {
        SLastElevator.onSwichedToNextStage += InitSatge;
    }
    private void OnDisable()
    {
        SLastElevator.onSwichedToNextStage -= InitSatge;
    }
    private void InitSatge()
    {
        //on first platform
        currStagesTxt.text = (StateMonitor.currentStageIndex + 1).ToString();

        //fill finished stage circles
        for (int i = 0; i < StateMonitor.currentStageIndex + 1; i++)
        {
            Image cirlePic = StageCircles[i].GetComponent<Image>();
            cirlePic.color = Color.yellow;
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
