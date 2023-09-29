using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SBoxPanel : MonoBehaviour
{
    string GameScene = "Game";
    public Transform[] panel;
    public GameObject ButtonTemplate;
    List<GameObject> allLevelButtons = new List<GameObject>();
    Color[] colorArray = new Color[]
    {
        new Color(0.5f, 0.7f, 1f), // Голубой цвет через RGB
        new Color(0.5f, 0.6f, 0.4f), // Оранжевый цвет через RGB
        new Color(0f, 1f, 0.5f),  // Зеленый цвет через RGB
        new Color(1f, 0.7f, 0.8f)  // Зеленый цвет через RGB
    };
    internal protected static int SelectedLevel 
    { get; private set; }

    const int levelNum = 36;
    const int defaultUnlocktedLevels = 3;
    const int _maxStarsInOnLvl = 3;
    const int _cellNumInOneSet = 3;

    static int unblocktedSet;
    static bool[,] starExist = new bool[levelNum, _maxStarsInOnLvl];
    static bool[] availableLevel = new bool[levelNum];
    
    internal protected int StarsNumToUnblockNextSet { get; private set; }

    void Start()
    {
        FillLevelPanels();
        UpdateStarsNumInComplitedLevel();
        ShowStars();
        for (int i = 0; i < defaultUnlocktedLevels; i++)
        {
            allLevelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            availableLevel[i] = true;
        }
        CheckAvailableLevel();
        CheckSelectedLevel();
    }

    void FillLevelPanels()
    {
        int currentCell = 0;
        int cellNum = 9;
        for (int i = 0; i < panel.Length; i++)
        {
            Color color = colorArray[i];
            for (int j = currentCell; j < (i * cellNum) + levelNum / panel.Length; j++)
            {
                int levelNumber = j + 1;
                GameObject buttonObject = Instantiate(ButtonTemplate);
                buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = levelNumber.ToString();
                buttonObject.GetComponent<Image>().color = color;
                buttonObject.transform.SetParent(panel[i], false);
                allLevelButtons.Add(buttonObject);
                currentCell++;
            }
        }
    }
    void CheckSelectedLevel()
    {
        foreach (GameObject button in allLevelButtons)
        {
            Button buttonComponent = button.GetComponent<Button>();
            int buttonIndex = allLevelButtons.IndexOf(button);
            if (availableLevel[allLevelButtons.IndexOf(button)])
                buttonComponent.onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }
    int GetStarsSumInAvailableSets()
    {
        int sum = 0;
        for (int i = 0; i < levelNum; i++)
        {
            for (int j = 0; j < _maxStarsInOnLvl; j++)
            {
                if (starExist[i, j])
                    sum++;
            }
        }
        return sum;
    }
    void CheckAvailableLevel()
    {
        int minStarsNumToUnblockLevels = 6; //for one set
        int currentAvailableStars = ((minStarsNumToUnblockLevels * unblocktedSet) + minStarsNumToUnblockLevels);
        Debug.Log($"Sum: {GetStarsSumInAvailableSets()}, Need: {currentAvailableStars}, {unblocktedSet}");
        if (GetStarsSumInAvailableSets() >= currentAvailableStars)
        {
            Debug.Log("sum > value");
            unblocktedSet++;
            int unblocedLevelNum = _cellNumInOneSet * unblocktedSet;
            for (int i = unblocedLevelNum; i < (unblocktedSet * _cellNumInOneSet) + _cellNumInOneSet; i++)
                availableLevel[i] = true;
        }

        //set locker
        for (int i = 0; i < levelNum; i++)
        {
            if (availableLevel[i])
                allLevelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
        }

        StarsNumToUnblockNextSet = ((minStarsNumToUnblockLevels * unblocktedSet) + minStarsNumToUnblockLevels) - GetStarsSumInAvailableSets();
    }

    void UpdateStarsNumInComplitedLevel()
    {
        if (SWinPanel.starRecived != null)
        {

            for (int i = 0; i < _maxStarsInOnLvl; i++)
            {
                if (SWinPanel.starRecived[i])
                {
                    starExist[SelectedLevel, i] = true;
                    SWinPanel.starRecived[i] = false;
                }
                else
                    starExist[SelectedLevel, i] = false;
            }
        }

    }

    void ShowStars()
    {
        for (int i = 0; i < levelNum; i++)
        {
            for (int j = 0; j < _maxStarsInOnLvl; j++)
            {
                if (starExist[i, j])
                    allLevelButtons[i].transform.GetChild(1).transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        SelectedLevel = buttonIndex;
        SceneManager.LoadScene(GameScene);
    }
}