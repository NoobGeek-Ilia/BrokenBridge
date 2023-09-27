using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameOverPanel : MonoBehaviour
{
    [SerializeField] SPlayerLifeController playerLifeController;
    [SerializeField] SBuildMaterialController materialController;
    [SerializeField] StateMonitor stateMonitor;
    private void Awake()
    {
        playerLifeController.OnPlayerDied += OpenPanel;
        materialController.OnMaterialRunOut += OpenPanel;
    }
    private void OnDisable()
    {
        playerLifeController.OnPlayerDied -= OpenPanel;
        materialController.OnMaterialRunOut -= OpenPanel;
    }
    internal protected void OpenPanel()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        stateMonitor.Reload(true);
        
    }
    public void GoToMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
