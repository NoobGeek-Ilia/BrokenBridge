using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameOverPanel : MonoBehaviour
{
    [SerializeField] private SPlayerLifeController playerLifeController;
    [SerializeField] private SBuildMaterialController materialController;
    [SerializeField] private StateMonitor stateMonitor;
    [SerializeField] private GameObject HomeButtonReserve;
    [SerializeField] private Animator animator;

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
        HomeButtonReserve.SetActive(false);
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
