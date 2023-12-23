using UnityEngine;

public class SRotateObject : MonoBehaviour
{
    [SerializeField] int rotateSpeed = 2;
    private void FixedUpdate()
    {
        RotateObject();
    }
    void RotateObject()
    {
        
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
