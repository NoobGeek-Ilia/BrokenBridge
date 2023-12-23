using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SWeaponTab : SWallet
{
    private int currIndex;
    internal protected static int _weaponNum = 7;
    internal protected bool[] selectInfo = new bool[_weaponNum];
    private bool[] boughtInfo = new bool[_weaponNum];
    [SerializeField] GameObject[] Weapons = new GameObject[_weaponNum];
    [SerializeField] GameObject selectBut;
    [SerializeField] GameObject buyBut;
    [SerializeField] GameObject priceUI;

    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI accuracyTxt;
    [SerializeField] TextMeshProUGUI distanceTxt;
    [SerializeField] TextMeshProUGUI priceTxt;

    [SerializeField] TextMeshProUGUI selectTxt;
    SWeaponInfo[] weaponInfo =
    {
        new SWeaponInfo("Wood Sword", 2, 5),
        new SWeaponInfo("Metall Sword", 3, 5, 250),
        new SWeaponInfo("Dimond Axe", 4, 3, 900),
        new SWeaponInfo("Elven Sword", 5, 6, 1200),
        new SWeaponInfo("Butcher's Mace", 8, 3, 1850),
        new SWeaponInfo("Scythe of death", 5, 10, 2900),
        new SWeaponInfo("Mega Sword", 10, 12, 5000),

    };

    private void Start()
    {
        boughtInfo[0] = true;
        for (int i = 1; i < _weaponNum; i++)
        {
            int boughtValue = PlayerPrefs.GetInt("BoughtWeaponInfo_" + i, 0);
            boughtInfo[i] = boughtValue == 1;
        }
        if (CheckSumBoughtWeapon() == 1)
        {
            selectInfo[0] = true;
            boughtInfo[0] = true;
        }
            SetActiveWeapon();
    }
    private void Update()
    {
        CheckSelectedWeapon();
    }
    int CheckSumBoughtWeapon()
    {
        int sum = 0;
        for (int i = 0; i < _weaponNum; i++)
        {
            if (boughtInfo[i])
                sum++;
        }
        return sum;
    }
    void SetWeaponInfo(SWeaponInfo weapon)
    {
        nameTxt.text = weapon.name;
        accuracyTxt.text = weapon.accuracy.ToString();
        distanceTxt.text = weapon.distance.ToString();
        priceTxt.text = weapon.price.ToString();
    }
    public void RightBut()
    {
        if (currIndex < Weapons.Length - 1)
            currIndex++;
        SetActiveWeapon();
    }

    public void LeftBut()
    {
        if (currIndex > 0)
            currIndex--;
        SetActiveWeapon();
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

    void CheckSelectedWeapon()
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

    void SetActiveWeapon()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }
        Weapons[currIndex].SetActive(true);
        SetWeaponInfo(weaponInfo[currIndex]);
        ShowActiveElements();
    }

    public void SelectWeapon()
    {
        for (int i = 0; i < selectInfo.Length; i++)
        {
            selectInfo[i] = false;
            PlayerPrefs.SetInt("SelectedWeaponInfo_" + i, 0);
        }
        selectInfo[currIndex] = true;
        PlayerPrefs.SetInt("SelectedWeaponInfo_" + currIndex, 1);
        SGlobalGameInfo.selectedWeapon = currIndex;
    }

    public void BuyWeapon()
    {
        DoTransaction(weaponInfo[currIndex].price);
    }

    private new void DoTransaction(int itemPrice)
    {
        if (itemPrice <= CoinValue)
        {
            boughtInfo[currIndex] = true;
            PlayerPrefs.SetInt("BoughtWeaponInfo_" + currIndex, 1);
            SelectWeapon();
            ShowActiveElements();
        }
        base.DoTransaction(itemPrice);
    }
}
