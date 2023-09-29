using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SWinPanel : MonoBehaviour
{
    [SerializeField] SPlayerMovement playerMovement;
    [SerializeField] InitRoadFilling initRoad;
    [SerializeField] StateMonitor stateMonitor;
    [SerializeField] GameObject HomeButton;
    [SerializeField] GameObject HomeButtonReserve;
    

    [SerializeField] TextMeshProUGUI[] statisticTxt;

    const int _maxStars = 3;
    internal protected static bool[] starRecived = new bool[_maxStars];
    [SerializeField] GameObject[] Star;

    const int maxValuePlayerFell = 3;
    const int minValueKilledEnemiesPercent = 50;
    const int maxValueBridgeBroke = 3;
    int[] thresholdValue = { maxValuePlayerFell , minValueKilledEnemiesPercent, maxValueBridgeBroke };

    internal protected void OpenPanel()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        HomeButtonReserve.SetActive(false);

        SWinStastistic statistic = new SWinStastistic(playerMovement.PlayerFellNum, initRoad.allEnemiesOnLvlNum,
stateMonitor.KilledEnemyesNum, stateMonitor.BrokeBridgeNum);
        int[] statisticInfo = { statistic.playerFellSum, statistic.percentEnemiesSum, statistic.bridgeBrokeSum };

        StartCoroutine(ShowStaticticAnim(statisticInfo));
        
        
    }
    public void GoToMainScene()
    {
        //send money to wallet
        SWallet.CoinValue += stateMonitor.coinsNum;
        PlayerPrefs.SetInt("CoinValue", SWallet.CoinValue);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log(PlayerPrefs.GetInt("CoinValue", SWallet.CoinValue));
    }

    private IEnumerator ShowStaticticAnim(int[] statistic)
    {
        for (int i = 0; i < statistic.Length; i++)
        {
            int currNum = 0;
            while (currNum < statistic[i])
            {
                currNum++;
                statisticTxt[i].text = currNum.ToString();
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        StartCoroutine(ShowStars(statistic));
    }

    private IEnumerator ShowStars(int[] statistic)
    {
        // can not use for loop, different comparison sign
        if (statistic[0] < thresholdValue[0])
        {
            Star[0].SetActive(true);
            starRecived[0] = true;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
        if (statistic[1] >= thresholdValue[1])
        {
            Star[1].SetActive(true);
            starRecived[1] = true;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
        if (statistic[2] < thresholdValue[2])
        {
            Star[2].SetActive(true);
            starRecived[2] = true;
            yield return new WaitForSecondsRealtime(1f);
        }
        HomeButton.SetActive(true);
    }
}
