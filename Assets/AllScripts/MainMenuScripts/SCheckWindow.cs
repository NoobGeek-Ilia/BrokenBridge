using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SCheckWindow : MonoBehaviour
{
    private WindowName _winName;

    public WindowName winName
    {
        get { return _winName; }
        set
        {
            if (value != _winName)
            {
                _winName = value;
                CheckOpenedWindow(_winName);
            }
        }
    }

    public GameObject levelPanel;
    public GameObject[] mainElements;
    [SerializeField] GameObject StorePanel;
    [SerializeField] GameObject InfoPanel;


    void CheckOpenedWindow(WindowName winName)
    {
        CloseAllWindows();
        switch (winName)
        {
            case WindowName.None:
                foreach (GameObject button in mainElements)
                {
                    button.SetActive(true);
                }
                //SwitchMainButtons();
                break;

            case WindowName.Levels:
                levelPanel.SetActive(true);
                //SwitchMainButtons();
                break;

            case WindowName.Store:
                StorePanel.SetActive(true);
                break;

            case WindowName.Settings:

                break;

            case WindowName.Info:
                InfoPanel.SetActive(true);
                break;
        }
    }
    public enum WindowName
    {
        None,
        Levels,
        Store,
        Settings,
        Info
    }
    private void CloseAllWindows()
    {
        levelPanel.SetActive(false);
        StorePanel.SetActive(false);
        InfoPanel.SetActive(false);
        foreach (GameObject button in mainElements)
        {
            button.SetActive(false);
        }
    }
    public void ClosePanel()
    {
        winName = WindowName.None;
    }
}
