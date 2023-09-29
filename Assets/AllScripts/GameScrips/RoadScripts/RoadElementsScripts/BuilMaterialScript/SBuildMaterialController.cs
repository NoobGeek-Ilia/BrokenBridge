using System;
using UnityEngine;

public class SBuildMaterialController : MonoBehaviour
{
    [SerializeField] StateMonitor stateMonitor;
    [SerializeField] SGameOverPanel gameOverPanel;
    internal protected Action OnMaterialRunOut;
    const int _materialBonusNum = 3;
    const int _startMaterialNum = 100;
    private int currMaterialsNum = _startMaterialNum;
    private bool materialRunOut;

    internal protected int MaterialsNum 
    { 
        get => currMaterialsNum; 
        set => currMaterialsNum = value;
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