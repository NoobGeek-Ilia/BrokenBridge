using UnityEngine;

public class SPlayerPrefs : MonoBehaviour
{
    [SerializeField] private SCharacterTab characterTab;
    [SerializeField] private SWeaponTab sWeapon;

    private void Start()
    {
        for (int i = 0; i < SCharacterTab._characterNum; i++)
        {
            int selectValue = PlayerPrefs.GetInt("SelectedCharacterInfo_" + i);
            characterTab.selectInfo[i] = selectValue == 1;
            if (characterTab.selectInfo[i])
                SGlobalGameInfo.selectedCharacter = i;
        }

        for (int i = 0; i < SWeaponTab._weaponNum; i++)
        {
            int selectValue = PlayerPrefs.GetInt("SelectedWeaponInfo_" + i);
            sWeapon.selectInfo[i] = selectValue == 1;
            if (sWeapon.selectInfo[i])
                SGlobalGameInfo.selectedWeapon = i;
        }
    }
}