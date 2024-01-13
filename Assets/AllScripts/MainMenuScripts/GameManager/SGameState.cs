using System;
using UnityEngine;

public class SGameState : MonoBehaviour
{
    internal protected static bool GameComplite { get; private set; }

    [SerializeField] private GameObject[] CompliteItem;

    private Action allSetsUnlockedHandler;
    private static int gameState;

    private void Start()
    {
        GameComplite = PlayerPrefs.GetInt("GameComplite") == 1;
        if (!GameComplite)
        {
            allSetsUnlockedHandler = () =>
            {
                ActivateCompliteItems();
                PlayerPrefs.SetInt("GameComplite", 1);
                gameState = PlayerPrefs.GetInt("GameComplite");
                GameComplite = gameState == 1;
            };
            SLockController.AllSetsHaveBeenUnlocked += allSetsUnlockedHandler;
        }
    }

    private void ActivateCompliteItems()
    {
        for (int item = 0; item < CompliteItem.Length; item++)
        {
            CompliteItem[item].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        SLockController.AllSetsHaveBeenUnlocked -= allSetsUnlockedHandler;
    }
}
