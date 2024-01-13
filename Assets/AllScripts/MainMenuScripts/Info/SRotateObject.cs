using UnityEngine;

public class SRotateObject : MonoBehaviour
{
    [SerializeField] private int rotateSpeed = 2;
    private void FixedUpdate()
    {
        RotateObject();
    }
    private void RotateObject()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
