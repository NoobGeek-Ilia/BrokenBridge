using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SLevelPanel : MonoBehaviour
{

    public TextMeshProUGUI starsCountToUnblock;
    public SBoxPanel sBoxPanel;

    private void Update()
    {
        starsCountToUnblock.text = $"left for next unlock: {sBoxPanel.StarsNumToUnblockNextSet}";
    }
}