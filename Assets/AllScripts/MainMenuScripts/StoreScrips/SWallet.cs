using System;
using UnityEngine;

public class SWallet : MonoBehaviour
{
    private static int coinValue = 10000;
    internal protected static int CoinValue { get { return coinValue; } set { coinValue = value; }}
    public virtual void DoTransaction(int itemPrice)
    {
        if (itemPrice <= CoinValue)
        {
            CoinValue -= itemPrice;
        }
    }
}
