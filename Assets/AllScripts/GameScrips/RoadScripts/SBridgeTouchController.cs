using UnityEngine;

public class SBridgeTouchController : MonoBehaviour
{
    internal protected bool isTouching;
    GameObject bridge;
    public SBridgeSpawner bridgeSpawner;
    public SCamera mainCamera;
    public SRoad road;

    private void Update()
    {
        if (mainCamera.cp == SCamera.CameraPosition.Build && !road.roadComplite)
            CheckTouchAndBuild();
    }
    void CheckTouchAndBuild()
    {
        bridge = bridgeSpawner.bridges[bridgeSpawner.currBridge - 1];
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                isTouching = true;
            else if (touch.phase == TouchPhase.Ended && bridge.GetComponent<SBridge>().copyBridgeParticle.Count > 0)
            {
                Debug.Log("есть части");
                bridge.GetComponent<SBridge>().PushBridgeBody();
                isTouching = false;
            }
        }
        if (isTouching && !bridge.GetComponent<SBridge>().bridgeIsFalling)
            bridge.GetComponent<SBridge>().BuildBringe();
    }
}
