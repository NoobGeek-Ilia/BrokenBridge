using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SStorePanel : MonoBehaviour
{
    private Image[] storeTabButtonImages;

    [SerializeField] private Button[] StoreTabBut;
    [SerializeField] private GameObject[] StoreTab;
    [SerializeField] private TextMeshProUGUI coinValueTxt;

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

    private void OpenActiveTab(int tab)
    {
        CloseAllTab();
        StoreTab[tab].SetActive(true);
    }

    private void CloseAllTab()
    {
        for (int i = 0; i < StoreTab.Length; i++)
        {
            StoreTab[i].SetActive(false);
        }
    }
}