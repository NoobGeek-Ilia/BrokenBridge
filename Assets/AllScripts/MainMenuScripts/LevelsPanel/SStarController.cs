using UnityEngine;

public class SStarController 
{
    internal protected const int maxStarsInOnLvl = 3;
    internal protected static bool[,] starExist = new bool[SBoxPanel.levelNum, maxStarsInOnLvl];

    internal protected SStarController()
    {
        //установить звезды, если получено больше
        if (SWinPanel.GetStarRecivedSum > GetStarsSumInSelectedLvl())
            SaveStarsData();
        SetSavedStarsData();
    }
    private void SetSavedStarsData()
    {
        for (int i = 0; i < SBoxPanel.levelNum; i++)
        {
            for (int j = 0; j < SStarController.maxStarsInOnLvl; j++)
            {
                int exist = PlayerPrefs.GetInt("StarExist" + i + "_" + j);
                starExist[i, j] = exist == 1;
            }
        }
    }
    private void SaveStarsData()
    {
        for (int i = 0; i < maxStarsInOnLvl; i++)
        {
            if (SWinPanel.starRecived[i])
            {
                starExist[SBoxPanel.SelectedLevel, i] = true;
                SWinPanel.starRecived[i] = false; // вывести в начало скрипта игры, чтобы значение обновлялось
                PlayerPrefs.SetInt("StarExist" + SBoxPanel.SelectedLevel + "_" + i, 1);
            }
            else
            {
                starExist[SBoxPanel.SelectedLevel, i] = false;
                PlayerPrefs.SetInt("StarExist" + SBoxPanel.SelectedLevel + "_" + i, 0);
            }
        }
        PlayerPrefs.Save();
    }

    internal protected static int GetStarsSum()
    {
        int sum = 0;
        for (int i = 0; i < SBoxPanel.levelNum; i++)
        {
            for (int j = 0; j < maxStarsInOnLvl; j++)
            {
                if (starExist[i, j])
                    sum++;
            }
        }
        return sum;
    }

    private int GetStarsSumInSelectedLvl()
    {
        int sum = 0;
        for (int i = 0; i < maxStarsInOnLvl; i++)
        {
            if (starExist[SBoxPanel.SelectedLevel, i])
                sum++;
        }
        return sum;
    }
}
