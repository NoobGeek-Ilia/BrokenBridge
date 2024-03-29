using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SCharacterTab : SWallet
{
    public static SCharacterInfo[] characterInfo = {
        new SCharacterInfo("Ninja boy", 30),
        new SCharacterInfo("Ninja girl", 40, 2200),
        new SCharacterInfo("Heir boy", 50, 3700)
    };

    internal protected static int maxCurrCharacterHp { get; private set; } = 30;
    internal protected static int _characterNum = 3;
    internal protected bool[] selectInfo = new bool[_characterNum];

    private bool[] boughtInfo = new bool[_characterNum];
    private int currIndex;

    [SerializeField] private GameObject[] Characters = new GameObject[_characterNum];
    [SerializeField] private GameObject selectBut;
    [SerializeField] private GameObject buyBut;
    [SerializeField] private GameObject priceUI;
    [SerializeField] private TextMeshProUGUI nameTxt;    
    [SerializeField] private TextMeshProUGUI hpTxt;    
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private TextMeshProUGUI selectTxt;

    private void Start()
    {
        boughtInfo[0] = true;
        for (int i = 1; i < _characterNum; i++)
        {
            int boughtValue = PlayerPrefs.GetInt("BoughtCharacterInfo_" + i);
            boughtInfo[i] = boughtValue == 1;
        }
        if (CheckSumBoughtChar() == 1)
        {
            selectInfo[0] = true;
            boughtInfo[0] = true;
        }
        SetActiveCharacter();
    }
    private void Update()
    {
        CheckSelectedCharacter();
    }
    private int CheckSumBoughtChar()
    {
        int sum = 0;
        for (int i = 0; i < _characterNum; i++)
        {
            if (boughtInfo[i])
                sum++;
        }
        return sum;
    }
    private void SetCharacterInfo(SCharacterInfo character)
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

    private void ShowActiveElements()
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

    private void CheckSelectedCharacter()
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

    private void SetActiveCharacter()
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
            PlayerPrefs.SetInt("SelectedCharacterInfo_" + i, 0);
        }
        selectInfo[currIndex] = true;
        PlayerPrefs.SetInt("SelectedCharacterInfo_" + currIndex, 1);
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
            SelectCharacter();
            ShowActiveElements();
        }
        base.DoTransaction(itemPrice);
    }
}
