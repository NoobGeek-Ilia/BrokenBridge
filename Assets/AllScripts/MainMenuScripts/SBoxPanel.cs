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
    int unblocktedSet;
    int[] starsNum = new int[levelNum];
    int defaultUnlocktedLevels = 3;
    bool[] availableLevel = new bool[levelNum];
    int cellNumInOneSet = 3;
    internal protected int StarsNumToUnblockNextSet { get; private set; }

    void Start()
    {
        FillLevelPanels();
        for (int i = 0; i < defaultUnlocktedLevels; i++)
        {
            availableLevel[i] = true;
        }
        CheckAvailableLevel();
        ShowStars();
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
        for (int i = 0; i < (unblocktedSet * cellNumInOneSet) + cellNumInOneSet; i++)
        {
            sum += starsNum[i];
        }
        return sum;
    }
    void CheckAvailableLevel()
    {
        int minStarsNumToUnblockLevels = 6; //for one set
        int currentAvailableStars = (minStarsNumToUnblockLevels * unblocktedSet) + minStarsNumToUnblockLevels;
        if (currentAvailableStars >= GetStarsSumInAvailableSets())
        {
            for (int i = 0; i < (unblocktedSet * cellNumInOneSet) + cellNumInOneSet; i++)
            {
                allLevelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                availableLevel[i] = true;
            }
        }
        StarsNumToUnblockNextSet = currentAvailableStars - GetStarsSumInAvailableSets();
    }
    void ShowStars()
    {
        for (int i = 0; i < levelNum; i++)
        {
            for (int j = 0; j < starsNum[i]; j++)
                allLevelButtons[i].transform.GetChild(1).transform.GetChild(j).gameObject.SetActive(true);
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        SelectedLevel = buttonIndex;
        SceneManager.LoadScene(GameScene);
    }
}