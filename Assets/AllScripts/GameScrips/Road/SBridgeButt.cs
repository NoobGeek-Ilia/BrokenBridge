using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SBridgeButt : MonoBehaviour
{
    GameObject bridge;
    public SBridgeSpawner bridgeSpawner;
    public Button button;
    void Start()
    {
        button = GetComponent<Button>();
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

        UnityAction action = () => bridge.GetComponent<SBridge>().BuildBridge();
        button.onClick.AddListener(action);
    }

    // Update is called once per frame
    void Update()
    {
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");
    }
}
