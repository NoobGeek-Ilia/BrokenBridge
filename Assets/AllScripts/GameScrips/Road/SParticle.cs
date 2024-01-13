using UnityEngine;

public class SParticle : MonoBehaviour
{
    public SPlatform platform;
    public SBridge bridge;
    private void Start()
    {
        bridge = FindObjectOfType<SBridge>();
        platform = FindObjectOfType<SPlatform>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (bridge.bridgeIsFalling && collision.gameObject.CompareTag("PlatformTag"))
        {
            if (collision.gameObject == platform.copyPlatform[platform.currentIndexPlatform + 1] ||
                collision.gameObject == platform.copyPlatform[platform.currentIndexPlatform])
            {
                bridge.CheckBridgeColl();
                bridge.bridgeIsFalling = false;
                
            }
        }
    }
}