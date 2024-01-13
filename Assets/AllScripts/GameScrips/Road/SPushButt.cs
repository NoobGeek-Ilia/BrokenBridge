using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SPushButt : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public Button button;

    private GameObject bridge;

    private void Start()
    {
        button = GetComponent<Button>();
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

        UnityAction actionPush = () => bridge.GetComponent<SBridge>().PushBridgeBody();
        button.onClick.AddListener(actionPush);
    }

    private void Update()
    {
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

    }
}
