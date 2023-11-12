using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SMainButtons : MonoBehaviour
{
    public SCheckWindow checkWindow;
    
    public void StartButt()
    {
        checkWindow.winName = SCheckWindow.WindowName.Levels;
        //SceneManager.LoadScene(GameScene);
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
}
