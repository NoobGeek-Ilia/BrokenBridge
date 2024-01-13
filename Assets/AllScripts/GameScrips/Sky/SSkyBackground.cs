using UnityEngine;

public class SSkyBackground : MonoBehaviour
{
    private void Start()
    {
        SetBackground();
    }
    private void SetBackground()
    {
        string[] material = { "Sunset5", "Night1", "Night4", "Sunset4" };
        RenderSettings.skybox = Resources.Load<Material>($"SkyMaterials/{material[SBoxPanel.SelectedSet]}");
    }
}
