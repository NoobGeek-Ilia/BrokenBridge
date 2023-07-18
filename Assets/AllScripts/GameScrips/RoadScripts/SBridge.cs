using System.Collections.Generic;
using UnityEngine;

public class SBridge : MonoBehaviour
{
    public List<GameObject> copyBridgeParticle = new List<GameObject>();
    public List<GameObject> brokenBridgePart = new List<GameObject>();
    public GameObject bridgeParticle;
    public GameObject bridgeBody;
    public SPlatform platform;
    public SBridgeSpawner bridgeSpawner;
    public Transform bridgeBodyTransform;
    private int newTopBridge = 0;
    public bool bridgeIsFalling;
    const int widthBridge = 5;
    const int heightBridge = 1;
    List<bool> particleOnPlatform = new List<bool>();
    bool bridgeCollided = false;
    float fadeStartTime;




    private void Start()
    {
        bridgeSpawner = FindObjectOfType<SBridgeSpawner>();
        bridgeParticle = Resources.Load<GameObject>("Prefabs/BridgeParticle");
        bridgeBody = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");
        platform = FindObjectOfType<SPlatform>();
        bridgeBodyTransform = bridgeBody.transform;
        newTopBridge = 0;

    }
    void Update()
    {
        //Debug.Log(platform.currentIndexPlatform + 1);
        LowerBridge();
        ApplyFadeEffect();
    }

    void LowerBridge()
    {
        const float speed = 100;
        if (bridgeIsFalling)
        {
            bridgeBody.transform.Rotate(Vector3.back, speed * Time.deltaTime);
        }
    }
    public void BuildBringe()
    {
        //button Bridge
        for (int i = newTopBridge; i < newTopBridge + heightBridge; i++)
        {
            for (int j = 0; j < widthBridge; j++)
            {
                float newPartPos_x = transform.position.x + (bridgeParticle.transform.localScale.x / 2);
                float newPartPos_y = (platform.GetRender_yPos + (bridgeParticle.transform.localScale.y / 2))
                    + (i * bridgeParticle.transform.localScale.y);
                float newPartPos_z = (platform.GetRender_zPos - (bridgeParticle.transform.localScale.z / 2)) - (j * bridgeParticle.transform.localScale.z);

                Vector3 newStartPosBridgeParticle = new Vector3(newPartPos_x, newPartPos_y, newPartPos_z);
                copyBridgeParticle.Add(Instantiate(bridgeParticle, newStartPosBridgeParticle, Quaternion.identity));
            }
        }
        for (int i = 0; i < copyBridgeParticle.Count; i++)
        {
            copyBridgeParticle[i].transform.SetParent(bridgeBodyTransform, true);
        }
        newTopBridge += heightBridge;
        particleOnPlatform.Add(false);
    }
    public void PushBridgeBody()
    {
        //button Push
        bridgeIsFalling = true;

        for (int i = widthBridge * 2; i < copyBridgeParticle.Count; ++i)
        {
            if (copyBridgeParticle[i].GetComponent<SParticle>() == null)
                copyBridgeParticle[i].AddComponent<SParticle>();
        }
    }

    public void CheckBridgeColl()
    {
        
        double lastParticleMaxPos_x = copyBridgeParticle[copyBridgeParticle.Count - 1].transform.position.x + copyBridgeParticle[copyBridgeParticle.Count - 1].transform.localScale.x + 1;
        double lastParticleMinPos_x = copyBridgeParticle[copyBridgeParticle.Count - 1].transform.position.x - copyBridgeParticle[copyBridgeParticle.Count - 1].transform.localScale.x - 1;
        double platformMaxPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x +
            (platform.transform.localScale.x / 2);
        double platformMinPos_x = platformMaxPos_x - platform.transform.localScale.x;
        bridgeCollided = true;
        if (lastParticleMinPos_x > platformMaxPos_x || lastParticleMaxPos_x < platformMinPos_x)
        {
            SplitBringe();
            ResetBridge();
        }
        else
            CutBridgeResetList();
    }
    void ResetBridge()
    {
        newTopBridge = 0;
        bridgeBody.transform.localRotation = Quaternion.identity;
    }
    void CutBridgeResetList()
    {
        //int[] brokenParts = { 3, 5, 7, 10 };
        double platformMaxPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x +
            platform.transform.localScale.x;
        double platformMinPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x - platform.transform.localScale.x;

        foreach (GameObject part in copyBridgeParticle)
        {
            double currParticleMaxPos = part.transform.position.x;
            if (currParticleMaxPos < platformMaxPos_x && currParticleMaxPos > platformMinPos_x)
            {
                brokenBridgePart.Add(part);
            }
            else
            {
                Destroy(part.GetComponent<Rigidbody>());
            }
        }

        foreach (GameObject part in brokenBridgePart)
        {
            copyBridgeParticle.Remove(part);
            Rigidbody rb = part.GetComponent<Rigidbody>();
            // Разрешить позицию по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;

            // Разрешить вращение по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;

            rb.useGravity = false;
            float forceAmount;

            if (part.transform.position.z > platform.copyPlatform[0].transform.position.z)
                forceAmount = 1000f;
            else
                forceAmount = -1000f;
            Vector3 force = new Vector3(0f, forceAmount, forceAmount);
            rb.AddForce(force);
        }
/*        for (int i = 0; i < brokenParts.Length; i++)
        {
            copyBridgeParticle[brokenParts[i]].AddComponent<Rigidbody>();
            Rigidbody rb = copyBridgeParticle[brokenParts[i]].GetComponent<Rigidbody>();
            // Разрешить позицию по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;

            // Разрешить вращение по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        }*/
        bridgeSpawner.brideComplite = true;
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -90f);
    }
    void SplitBringe()
    {
        for (int i = 0; i < copyBridgeParticle.Count; i++)
        {
            if (copyBridgeParticle[i].GetComponent<Rigidbody>() == null)
                copyBridgeParticle[i].transform.parent = null;
        }

        foreach (GameObject part in copyBridgeParticle)
        {
            float forceAmount;
            brokenBridgePart.Add(part);
            Rigidbody rb = part.GetComponent<Rigidbody>();
            // Разрешить позицию по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;

            // Разрешить вращение по всем осям
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
            part.transform.parent = null;
            if (part.transform.position.z > platform.copyPlatform[0].transform.position.z)
                forceAmount = 2000f;
            else
                forceAmount = -2000f;
            Vector3 force = new Vector3(0f, 0f, forceAmount);
            rb.AddForce(force);
        }
        copyBridgeParticle.Clear();
    }
    private void ApplyFadeEffect()
    {
        float fadeDuration = 0.3f; // Продолжительность затухания в секундах
        float targetAlpha = 0f; // Целевое значение прозрачности (0 - полностью прозрачный, 1 - полностью непрозрачный)

        if (bridgeCollided && fadeStartTime == 0f)
        {
            fadeStartTime = Time.time;
        }

        float fadeProgress = Mathf.Clamp01((Time.time - fadeStartTime) / fadeDuration);

        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in brokenBridgePart)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            Material objMaterial = renderer.material;

            Color originalColor = objMaterial.color;
            Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);

            Color newColor = Color.Lerp(originalColor, targetColor, fadeProgress);
            objMaterial.color = newColor;

            if (fadeProgress >= 1f)
            {
                // Завершение затухания
                objectsToRemove.Add(obj);
                fadeStartTime = 0;
                bridgeSpawner.brideComplite = false;
            }
        }

        // Удаление объектов
        foreach (GameObject obj in objectsToRemove)
        {
            brokenBridgePart.Remove(obj);
            Destroy(obj);
            
        }
        bridgeCollided = false;

    }


}
