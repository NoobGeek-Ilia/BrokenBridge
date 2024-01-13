using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SBridgeButt : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public Button button;

    private GameObject bridge;
    private void Start()
    {
        button = GetComponent<Button>();
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

        UnityAction action = () => bridge.GetComponent<SBridge>().BuildBridge();
        button.onClick.AddListener(action);
    }

    private void Update()
    {
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");
    }
}
