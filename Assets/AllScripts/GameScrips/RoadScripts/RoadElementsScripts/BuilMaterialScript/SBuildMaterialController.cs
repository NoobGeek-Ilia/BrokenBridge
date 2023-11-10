using System;
using UnityEngine;

public class SBuildMaterialController : MonoBehaviour
{
    [SerializeField] StateMonitor stateMonitor;
    [SerializeField] SGameOverPanel gameOverPanel;
    [SerializeField] SPlatform platform;

    internal protected Action OnMaterialRunOut;
    const int _materialBonusNum = 3;
    int extraMaterial = 5;
    private int currMaterialsNum;
    private bool materialRunOut;

    internal protected int MaterialsNum 
    { 
        get => currMaterialsNum; 
        set => currMaterialsNum = value;
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
            OnMaterialRunOut?.Invoke();
            materialRunOut = true;
            gameOverPanel.OpenPanel();
        }
    }
    internal protected void PickUpMaterial()
    {
        MaterialsNum += _materialBonusNum;
    }
    internal protected void ResetMaterial()
    {
        materialRunOut = false;
    }
}