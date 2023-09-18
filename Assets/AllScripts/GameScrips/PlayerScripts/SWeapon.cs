using UnityEngine;

public class SWeapon : MonoBehaviour
{
    private const int weaponNum = 7;
    [SerializeField] GameObject[] allWeapons = new GameObject[weaponNum];
    internal protected int[] missHitProb = new int[weaponNum] { 2, 2, 3, 4, 5, 6, 7};

}
