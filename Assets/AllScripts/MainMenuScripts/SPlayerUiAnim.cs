using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerUiAnim : MonoBehaviour
{
    float spinSpeed = 60;
    void Update()
    {
        transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
    }
}
