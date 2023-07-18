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
    public Button[] button;
    private void Update()
    {
        //CheckOpenedWindow(winName);
        //Debug.Log(_winName);
    }
    void CheckOpenedWindow(WindowName winName)
    {
        switch(winName)
        {
            case WindowName.None:
                levelPanel.SetActive(false);
                SwitchMainButtons();
                break;

            case WindowName.Levels:
                levelPanel.SetActive(true);
                SwitchMainButtons();
                break;

            case WindowName.Store:

                break;

            case WindowName.Settings:

                break;
        }
    }
    public enum WindowName
    {
        None,
        Levels,
        Store,
        Settings
    }
    public void SwitchMainButtons()
    {
        foreach (Button button in button)
        {
            if (button.interactable == true)
                button.interactable = false;
            else
                button.interactable = true;
        }
    }
}
