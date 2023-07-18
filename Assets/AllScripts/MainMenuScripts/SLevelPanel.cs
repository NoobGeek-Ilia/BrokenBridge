using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SLevelPanel : MonoBehaviour
{
    string GameScene = "Game";
    public SCheckWindow checkWindow;
    public Button closeButt;
    public Button[] levelButt;
    public static int levelNumber;
    int[] stages = { 3, 5, 7, 9, 10, 11, 12, 13, 13 };

    private void Awake()
    {
        
    }
    private void Start()
    {
        for (int i = 0; i < levelButt.Length; i++)
        {
            int buttonIndex = i; // Сохраняем индекс кнопки в локальной переменной
            levelButt[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        //Debug.Log("Нажата кнопка с индексом: " + buttonIndex);
        levelNumber = buttonIndex;
        SceneManager.LoadScene(GameScene);
    }

    public void ClosePanel()
    {
        checkWindow.winName = SCheckWindow.WindowName.None;
    }
}


