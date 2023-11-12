using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SCharacterTab : SWallet
{
    private int currIndex;
    internal protected static int maxCurrCharacterHp { get; private set; } = 30;
    private const int _characterNum = 3;
    private bool[] selectInfo = new bool[_characterNum];
    private bool[] boughtInfo = new bool[_characterNum];
    [SerializeField] GameObject[] Characters = new GameObject[_characterNum];
    [SerializeField] GameObject selectBut;
    [SerializeField] GameObject buyBut;
    [SerializeField] GameObject priceUI;

    [SerializeField] TextMeshProUGUI nameTxt;    
    [SerializeField] TextMeshProUGUI hpTxt;    
    [SerializeField] TextMeshProUGUI priceTxt;

    [SerializeField] TextMeshProUGUI selectTxt;
    SCharacterInfo[] characterInfo =
    {
        new SCharacterInfo("Ninja boy", 30),
        new SCharacterInfo("Ninja girl", 40, 500),
        new SCharacterInfo("Heir boy", 50, 950)
    };



    private void Start()
    {
        for (int i = 0; i < _characterNum; i++)
        {
            int boughtValue = PlayerPrefs.GetInt("BoughtCharacterInfo_" + i, 0);
            boughtInfo[i] = boughtValue == 1;
        }
        boughtInfo[0] = true;
        selectInfo[0] = true;
        SetActiveCharacter();
    }
    private void Update()
    {
        CheckSelectedCharacter();
    }
    void SetCharacterInfo(SCharacterInfo character)
    {
        nameTxt.text = character.name;
        hpTxt.text = character.hp.ToString();
        priceTxt.text = character.price.ToString();
    }
    public void RightBut()
    {
        if (currIndex < Characters.Length - 1)
            currIndex++;
        SetActiveCharacter();
    }

    public void LeftBut()
    {
        if(currIndex > 0)
         currIndex--;
        SetActiveCharacter();
    }

    void ShowActiveElements()
    {
        if (boughtInfo[currIndex])
        {
            buyBut.SetActive(false);
            selectBut.SetActive(true);
            priceUI.SetActive(false);
        }
        else
        {
            buyBut.SetActive(true);
            selectBut.SetActive(false);
            priceUI.SetActive(true);
        }
    }

    void CheckSelectedCharacter()
    {
        if (selectInfo[currIndex])
        {
            selectBut.GetComponent<Image>().color = Color.green;
            selectTxt.text = "selected";
        }
        else
        {
            selectBut.GetComponent<Image>().color = Color.red;
            selectTxt.text = "select";
        }

    }

    void SetActiveCharacter()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }
        Characters[currIndex].SetActive(true);
        SetCharacterInfo(characterInfo[currIndex]);
        ShowActiveElements();
    }

    public void SelectCharacter()
    {
        for (int i = 0; i < selectInfo.Length; i++)
        {
            selectInfo[i] = false;
        }
        selectInfo[currIndex] = true;
        SGlobalGameInfo.selectedCharacter = currIndex;
        maxCurrCharacterHp = characterInfo[currIndex].hp;
    }

    public void BuyCharacter()
    {
        DoTransaction(characterInfo[currIndex].price);
    }
    
    private new void DoTransaction(int itemPrice)
    {
        if (itemPrice <= CoinValue)
        {
            boughtInfo[currIndex] = true;
            PlayerPrefs.SetInt("BoughtCharacterInfo_" + currIndex, 1);
            ShowActiveElements();
        }
        base.DoTransaction(itemPrice);
    }
}
