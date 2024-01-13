using UnityEngine;
using UnityEngine.UI;

public class SHomePanel : MonoBehaviour
{
    [SerializeField] private Button yesBut;
    [SerializeField] private Button noBut;
    [SerializeField] private Button messageBut;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private SWinPanel winPanel;

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
