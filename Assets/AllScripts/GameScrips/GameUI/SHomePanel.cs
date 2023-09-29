using UnityEngine;
using UnityEngine.UI;

public class SHomePanel : MonoBehaviour
{
    [SerializeField] Button yesBut;
    [SerializeField] Button noBut;
    [SerializeField] Button messageBut;

    [SerializeField] GameObject messagePanel;
    [SerializeField] SWinPanel winPanel;

    public void OpenMessageWindow()
    {
        messagePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void YesBut() => winPanel.GoToMainScene();

    public void NoBut()
    {
        messagePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
