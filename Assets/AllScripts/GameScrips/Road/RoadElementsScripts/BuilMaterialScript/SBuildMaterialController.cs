using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SBuildMaterialController : MonoBehaviour
{
    [SerializeField] private StateMonitor stateMonitor;
    [SerializeField] private SGameOverPanel gameOverPanel;
    [SerializeField] private SPlatform platform;
    [SerializeField] private TextMeshProUGUI MaterialCounter;
    [SerializeField] private SBuildMaterialSoundController materialSoundController;

    internal protected Action OnMaterialRunOut;
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

    private const int _materialBonusNum = 3;
    private int extraMaterial = 5;
    private int currMaterialsNum;
    private bool materialRunOut;

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
    private IEnumerator LoseEffect()
    {
        MaterialCounter.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        MaterialCounter.color = Color.white;
    }
}