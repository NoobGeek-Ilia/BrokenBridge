using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SPushButt : MonoBehaviour
{
    GameObject bridge;
    public SBridgeSpawner bridgeSpawner;
    public Button button;
    void Start()
    {
        button = GetComponent<Button>();
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

        UnityAction actionPush = () => bridge.GetComponent<SBridge>().PushBridgeBody();
        button.onClick.AddListener(actionPush);
    }

    // Update is called once per frame
    void Update()
    {
        bridge = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");

    }
}
