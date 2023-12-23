using UnityEngine;

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

    [SerializeField] GameObject[] mainElements;
    [SerializeField] GameObject[] Panels;

    private void Start()
    {
        CheckOpenedWindow(_winName);
    }
    void CheckOpenedWindow(WindowName winName)
    {
        CloseAllWindows();
        switch (winName)
        {
            case WindowName.None:
                foreach (GameObject item in mainElements)
                {
                    // информация о том что уровень пройден поступает после перезагрузки поэтому кнопка скрывается пока не перезагрузишь
                    if (!SGameState.GameComplite && item == mainElements[4])
                      continue;
                    item.SetActive(true);
                }
                break;

            case WindowName.Levels:
                Panels[0].SetActive(true);
                break;

            case WindowName.Store:
                Panels[1].SetActive(true);
                break;

            case WindowName.Info:
                Panels[2].SetActive(true);
                break;

            case WindowName.Reset:
                Panels[3].SetActive(true);
                break;
        }
    }
    public enum WindowName
    {
        None,
        Levels,
        Store,
        Settings,
        Info,
        Reset
    }
    private void CloseAllWindows()
    {
        foreach (GameObject button in Panels)
        {
            button.SetActive(false);
        }
        foreach (GameObject item in mainElements)
        {
            item.SetActive(false);
        }
    }
    public void ClosePanel()
    {
        winName = WindowName.None;
    }
}
