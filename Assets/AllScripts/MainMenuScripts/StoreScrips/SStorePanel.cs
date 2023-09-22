using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SStorePanel : MonoBehaviour
{
    [SerializeField] Button[] StoreTabBut;
    [SerializeField] GameObject[] StoreTab;
    [SerializeField] GameObject WindowBackground;
    [SerializeField] TextMeshProUGUI coinValueTxt;

    private Image windowBackgroundImage;
    private Image[] storeTabButtonImages;

    private void Start()
    {
        storeTabButtonImages = new Image[StoreTabBut.Length];
        windowBackgroundImage = WindowBackground.GetComponent<Image>();
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
        SetColorTab(tab);
        StoreTab[tab].SetActive(true);
    }

    void CloseAllTab()
    {
        for (int i = 0; i < StoreTab.Length; i++)
        {
            StoreTab[i].SetActive(false);
        }
    }

    void SetColorTab(int i)
    {
        Color buttonColor = storeTabButtonImages[i].color;
        windowBackgroundImage.color = buttonColor;

        Color darkerColor = new Color(buttonColor.r * 0.5f, buttonColor.g * 0.5f, buttonColor.b * 0.5f, buttonColor.a);
        WindowBackground.transform.GetChild(0).GetComponent<Image>().color = darkerColor;
    }
}
