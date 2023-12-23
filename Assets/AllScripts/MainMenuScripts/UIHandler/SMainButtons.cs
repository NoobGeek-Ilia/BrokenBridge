using UnityEngine;

public class SMainButtons : MonoBehaviour
{
    public SCheckWindow checkWindow;
    public void StartButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Levels;
    }
    public void StoreButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Store;
    }
    public void SettingsButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Settings;
    }

    public void InfoButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Info;
    }
    public void ResetButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Reset;
    }
}
