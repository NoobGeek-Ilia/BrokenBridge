using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SBuildMaterialController : MonoBehaviour
{
    [SerializeField] StateMonitor stateMonitor;
    [SerializeField] SGameOverPanel gameOverPanel;
    [SerializeField] SPlatform platform;
    [SerializeField] TextMeshProUGUI MaterialCounter;
    [SerializeField] SBuildMaterialSoundController materialSoundController;

    internal protected Action OnMaterialRunOut;
    const int _materialBonusNum = 3;
    int extraMaterial = 5;
    private int currMaterialsNum;
    private bool materialRunOut;

    internal protected int MaterialsNum 
    { 
        get => currMaterialsNum;
        set
        {
            if (currMaterialsNum < 1)
                currMaterialsNum = 0;
            currMaterialsNum = value;
        }
    }
    private void Start()
    {
        extraMaterial -= SBoxPanel.SelectedSet + 1;
        MaterialsNum = (platform.GetPlatformNum * stateMonitor.GetStageNum) + extraMaterial;

    }

    private void Update()
    {
        //גûחûגאועסÿ גטהטלמ קאסעמ, ג ןנמפאיכונו 
        if (MaterialsNum < 1 && !materialRunOut)
        {
            bool isLastPlatform = platform.GetPlatformNum - 2 == platform.currentIndexPlatform;
            OnMaterialRunOut?.Invoke();
            materialRunOut = true;
            gameOverPanel.OpenPanel();
        }
    }
    internal protected void PickUpMaterial()
    {
        MaterialsNum += _materialBonusNum;
        materialSoundController.PlaySound();
    }
    internal protected void ResetMaterial()
    {
        materialRunOut = false;
    }
    internal protected void LoseMaterial()
    {
        StartCoroutine(LoseEffect());
        MaterialsNum--;
    }
    IEnumerator LoseEffect()
    {
        MaterialCounter.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        MaterialCounter.color = Color.white;
    }
}