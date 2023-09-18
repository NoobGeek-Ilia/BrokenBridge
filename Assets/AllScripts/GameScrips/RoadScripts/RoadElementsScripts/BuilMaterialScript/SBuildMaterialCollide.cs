using UnityEngine;

public class SBuildMaterialCollide : MonoBehaviour
{
    StateMonitor sm;
    private void Start()
    {
        sm = FindObjectOfType<StateMonitor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sm.materialsNum += 3;
            gameObject.SetActive(false);
        }
    }
}
