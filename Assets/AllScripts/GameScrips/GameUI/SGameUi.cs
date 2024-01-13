using System.Collections;
using TMPro;
using UnityEngine;

public class SGameUi : MonoBehaviour
{
    [SerializeField] private SRoad road;
    [SerializeField] private TextMeshProUGUI coinsNumTxt;
    [SerializeField] private TextMeshProUGUI materialsNumTxt;
    [SerializeField] private StateMonitor monitor;
    [SerializeField] private GameObject StageCompliteWindow;
    [SerializeField] private SBuildMaterialController buildMaterialController;
    [SerializeField] private SWinPanel winPanel;

    private void OnEnable()
    {
        road.onRoadComplited += () => StartCoroutine(RunTimer());
        monitor.OnLevelComplited += ShowLevelStatistic;
    }
    private void OnDisable()
    {
        road.onRoadComplited -= () => StartCoroutine(RunTimer());
        monitor.OnLevelComplited -= ShowLevelStatistic;
    }

    private void Update()
    {
        ShowCoinAndMaterialCounter();
    }
    private void ShowCoinAndMaterialCounter()
    {
        coinsNumTxt.text = monitor.coinsNum.ToString();
        materialsNumTxt.text = buildMaterialController.MaterialsNum.ToString();
    }

    private void ShowLevelStatistic() => winPanel.OpenPanel();

    internal protected int GetRunTimer { get; set; } = 2;

    public TextMeshProUGUI RunTimerTxt;
    private IEnumerator RunTimer()
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
