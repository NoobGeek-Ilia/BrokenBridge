using System;
using UnityEngine;

public class SWallet : MonoBehaviour
{
    private static int coinValue = 10000;
    private void Awake()
    {
        coinValue = PlayerPrefs.GetInt("CoinValue", coinValue);
    }
    internal protected static int CoinValue { get { return coinValue; } set { coinValue = value; }}
    public virtual void DoTransaction(int itemPrice)
    {
        if (itemPrice <= CoinValue)
        {
            CoinValue -= itemPrice;
            PlayerPrefs.SetInt("CoinValue", coinValue);
        }
    }
}
