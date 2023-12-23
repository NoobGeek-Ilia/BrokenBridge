using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SStorePanel : MonoBehaviour
{
    [SerializeField] Button[] StoreTabBut;
    [SerializeField] GameObject[] StoreTab;
    [SerializeField] TextMeshProUGUI coinValueTxt;

    private Image[] storeTabButtonImages;

    private void Start()
    {
        storeTabButtonImages = new Image[StoreTabBut.Length];
        storeTabButtonImages[0] = StoreTabBut[0].GetComponent<Image>();
        OpenActiveTab(0);
        for (int i = 0; i < StoreTabBut.Length; i++)
        {
            int index = i;
            storeTabButtonImages[i] = StoreTabBut[i].GetComponent<Image>();

            StoreTabBut[i].onClick.AddListener(() => OpenActiveTab(index));
        }
    }

    private void Update()
    {
        coinValueTxt.text = SWallet.CoinValue.ToString();
    }

    void OpenActiveTab(int tab)
    {
        CloseAllTab();
        StoreTab[tab].SetActive(true);
    }

    void CloseAllTab()
    {
        for (int i = 0; i < StoreTab.Length; i++)
        {
            StoreTab[i].SetActive(false);
        }
    }

}
