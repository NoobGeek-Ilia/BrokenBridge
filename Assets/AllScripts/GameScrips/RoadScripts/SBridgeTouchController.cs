using UnityEngine;

public class SBridgeTouchController : MonoBehaviour
{
    internal protected bool isTouching;
    GameObject bridge;
    SBridge currBridge;
    public SBridgeSpawner bridgeSpawner;
    public SCamera mainCamera;
    public SRoad road;

    private void OnEnable()
    {
        bridgeSpawner.onBridgeSet += BridgeInit;
    }
    private void OnDisable()
    {
        bridgeSpawner.onBridgeSet -= BridgeInit;
    }
    private void Start()
    {
        BridgeInit();
    }
    private void Update()
    {
        if (mainCamera.cp == SCamera.CameraPosition.Build && !road.roadComplite)
            CheckTouchAndBuild();
    }
    public void BridgeInit()
    {
        bridge = bridgeSpawner.bridges[bridgeSpawner.currBridge - 1];
        currBridge = bridge.GetComponent<SBridge>();
    }

    void CheckTouchAndBuild()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isTouching = true;

        if (Input.GetKeyUp(KeyCode.Space) && currBridge.copyBridgeParticle.Count > 0)
        {
            currBridge.PushBridgeBody();
            isTouching = false;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                isTouching = true;
            else if (touch.phase == TouchPhase.Ended && currBridge.copyBridgeParticle.Count > 0)
            {
                currBridge.PushBridgeBody();
                isTouching = false;
            }
        }
        if (isTouching && !currBridge.bridgeIsFalling)
            currBridge.BuildBringe();
    }
}
