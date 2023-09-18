using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SGameUi : MonoBehaviour
{
    [SerializeField] SRoad road;
    [SerializeField] TextMeshProUGUI coinsNumTxt;
    [SerializeField] TextMeshProUGUI materialsNumTxt;
    [SerializeField] StateMonitor monitor;
    [SerializeField] GameObject StageCompliteWindow;
    private void OnEnable()
    {
        road.onRoadComplited += () => StartCoroutine(RunTimer());
    }
    private void OnDisable()
    {
        road.onRoadComplited -= () => StartCoroutine(RunTimer());
    }

    void Update()
    {
        ShowCoinAndMaterialCounter();
        if (monitor.levelComplite)
            ShowLevelStatistic();
    }
    void ShowCoinAndMaterialCounter()
    {
        coinsNumTxt.text = monitor.coinsNum.ToString();
        materialsNumTxt.text = monitor.materialsNum.ToString();
    }

    void ShowLevelStatistic()
    {
        StageCompliteWindow.SetActive(true);
    }

    internal protected int GetRunTimer { get; set; } = 2;

    public TextMeshProUGUI RunTimerTxt;
    IEnumerator RunTimer()
    {
        int timeMax = 3;
        RunTimerTxt.gameObject.SetActive(true);
        for (int i = timeMax; i > 0; i--)
        {
            RunTimerTxt.text = i.ToString();
            GetRunTimer = i;
            yield return new WaitForSeconds(1);
        }
        GetRunTimer = 0;
        RunTimerTxt.gameObject.SetActive(false);
        RunTimerTxt.text = timeMax.ToString();

    }
}
