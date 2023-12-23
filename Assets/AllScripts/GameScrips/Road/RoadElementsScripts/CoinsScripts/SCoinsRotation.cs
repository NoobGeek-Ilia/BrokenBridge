using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCoinsRotation : MonoBehaviour
{
    int spinSpeed = 200;
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
    }
}
