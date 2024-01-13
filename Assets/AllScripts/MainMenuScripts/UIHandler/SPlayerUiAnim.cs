using UnityEngine;

public class SPlayerUiAnim : MonoBehaviour
{
    private float spinSpeed = 60;
    private void Update()
    {
        transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
    }
}
