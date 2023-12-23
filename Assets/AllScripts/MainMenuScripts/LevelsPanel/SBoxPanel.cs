using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SBoxPanel : MonoBehaviour
{
    const string GameScene = "Game";
    public Transform[] panel;
    public GameObject ButtonTemplate;
    List<GameObject> allLevelButtons = new List<GameObject>();
    Color[] colorArray = new Color[]
    {
            new Color(0.90f, 0.70f, 1f), // Светло коричнивый
            new Color(0.5f, 0.7f, 1f),     // Светло голубой
            new Color(0.5f, 0.5f, 0.5f),   // Серый
            new Color(0f, 1f, 0.5f),
    };

    internal protected static int SelectedLevel 
    { get; private set; }

    internal protected static int SelectedSet
    { get; private set; }

    internal protected const int levelNum = 36;
    internal protected const int cellNumInOneSet = 3;
    private const int cellsPerSet = 9;

    [SerializeField] private GameObject unblockMessageWindow;
    public TextMeshProUGUI starsCountToUnblock;
    private SLockController lockController;
    private bool newSetUnlock;

    private void Start()
    {
        FillLevelPanels();
        ShowStars();
        ShowAvalableLevels();
        CheckSelectedLevel();
        ShowMessage();
        UnlockMessageDisplay();
    }

    private void FillLevelPanels()
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
    private void CheckSelectedLevel()
    {
        foreach (GameObject button in allLevelButtons)
        {
            Button buttonComponent = button.GetComponent<Button>();
            int buttonIndex = allLevelButtons.IndexOf(button);
            if(SLockController.availableLevel[allLevelButtons.IndexOf(button)])
                buttonComponent.onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    private void ShowAvalableLevels()
    {
        lockController = new SLockController(out newSetUnlock);
        for (int i = 0; i < levelNum; i++)
        {
            int levelValue = PlayerPrefs.GetInt("AvailableLevel" + i);
            SLockController.availableLevel[i] = levelValue == 1;
            if (SLockController.availableLevel[i])
                allLevelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void ShowStars()
    {
        new SStarController();
        for (int i = 0; i < levelNum; i++)
        {
            for (int j = 0; j < SStarController.maxStarsInOnLvl; j++)
            {
                if (SStarController.starExist[i, j])
                    allLevelButtons[i].transform.GetChild(1).transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        SelectedLevel = buttonIndex;
        SelectedSet = buttonIndex / cellsPerSet;
        SceneManager.LoadScene(GameScene);
    }

    private void ShowMessage()
    {
        string message = null;
        if (!SGameState.GameComplite)
        {
            starsCountToUnblock.color = Color.white;
            message = $"left for next unlock: {lockController.StarsNumNeedToUnblockNextSet}";
        }
        else
        {
            starsCountToUnblock.color = Color.green;
            message = $"are not necessary";
        }

        starsCountToUnblock.text = message;
    }

    private void UnlockMessageDisplay()
    {
        if (newSetUnlock)
        {
            unblockMessageWindow.SetActive(true);
            newSetUnlock = false;
        }
        else
            unblockMessageWindow.SetActive(false);
    }
}