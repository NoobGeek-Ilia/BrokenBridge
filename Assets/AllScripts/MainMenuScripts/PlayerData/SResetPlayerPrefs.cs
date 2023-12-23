using UnityEngine;

public class SResetPlayerPrefs : MonoBehaviour
{
    [ContextMenu("Reset PlayerPrefs")]
    private void ResetPlayerPrefses()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs סבנמרוםû!");
    }
}
