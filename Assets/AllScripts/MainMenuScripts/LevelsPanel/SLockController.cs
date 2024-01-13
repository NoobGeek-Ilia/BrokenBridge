using System;
using UnityEngine;

public class SLockController
{
    internal protected static bool[] availableLevel = new bool[SBoxPanel.levelNum];
    internal protected int StarsNumNeedToUnblockNextSet
    {
        get => starsNumToUnblockNextSet;
        private set => starsNumToUnblockNextSet = value;

    }
    internal protected static Action AllSetsHaveBeenUnlocked;

    private const int defaultUnlocktedLevels = 3;
    private int unblocktedSet;
    private int starsNumToUnblockNextSet;
    private bool NewSetHaveBeenUnlocked;

    internal protected SLockController(out bool newSetUnlock)
    {
        unblocktedSet = PlayerPrefs.GetInt("UnblocktedSet");
        if (!SGameState.GameComplite)
            CheckAvailableLevel();
        UnlockDefaultLvlNum();
        newSetUnlock = NewSetHaveBeenUnlocked;
    }

    private void UnlockDefaultLvlNum()
    {
        for (int i = 0; i < defaultUnlocktedLevels; i++)
        {
            availableLevel[i] = true;
            PlayerPrefs.SetInt("AvailableLevel" + i, 1);
        }
    }
    private void CheckAvailableLevel()
    {
        const int minStarsNumToUnblockLevels = 6; //for one set
        int currentAvailableStars = ((minStarsNumToUnblockLevels * unblocktedSet) + minStarsNumToUnblockLevels);
        bool allSetsUnblocked = IsAllSetsUnblocked();

        if (!allSetsUnblocked)
        {
            if (SStarController.GetStarsSum() >= currentAvailableStars)
            {
                unblocktedSet++;
                SetNewValue();
                NewSetHaveBeenUnlocked = true;
            }
        }
        allSetsUnblocked = IsAllSetsUnblocked();
        if (allSetsUnblocked)
            AllSetsHaveBeenUnlocked?.Invoke();
        currentAvailableStars = ((minStarsNumToUnblockLevels * unblocktedSet) + minStarsNumToUnblockLevels); //обновляем значение здвезд для следующего сета
        StarsNumNeedToUnblockNextSet = currentAvailableStars - SStarController.GetStarsSum();
    }


    private bool IsAllSetsUnblocked() =>
        unblocktedSet > (SBoxPanel.levelNum / SBoxPanel.cellNumInOneSet - 2);
    internal protected int GetUnlockedLvlNum()
    {
        int res = SBoxPanel.cellNumInOneSet * unblocktedSet;
        return res;
    }

    private void SetNewValue()
    {
        PlayerPrefs.SetInt("UnblocktedSet", unblocktedSet);
        for (int i = GetUnlockedLvlNum(); i < (unblocktedSet * SBoxPanel.cellNumInOneSet) + SBoxPanel.cellNumInOneSet; i++)
        {
            availableLevel[i] = true;
            PlayerPrefs.SetInt("AvailableLevel" + i, 1);
        }
    }
}
