using TMPro;
using UnityEngine;

public class SStageMonitor : MonoBehaviour
{
    public TextMeshPro currStagesTxt;
    void Update()
    {
        currStagesTxt.text = (StateMonitor.currentStageIndex + 1).ToString();
    }
}
