using UnityEngine;
using UnityEngine.UIElements;

public class SWeaponMovement : MonoBehaviour
{
    public SAnimationPlayer playerAnim;
   
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    float newRotationValue = 120;
    bool animateWeapon;
    private void Update()
    {
        Rotate(animateWeapon);
        if (playerAnim.hitAnimOn)
        {
            initialRotation = transform.rotation;
            initialPosition = transform.localPosition;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotationValue, transform.rotation.eulerAngles.z);
            animateWeapon = true;
            playerAnim.hitAnimOn = false;
        }
    }
    private void FixedUpdate()
    {
        
    }


    void Rotate(bool hitOn)
    {
        Debug.Log(hitOn);
        if (hitOn)
        {
            float moveSpeed = 15f;
            float rotationSpeed = 800; // Скорость вращения
            //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            Vector3 targetPoint = new Vector3(3f, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPoint, moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            if (transform.localPosition.x == targetPoint.x)
            {
                Debug.Log("iiiooo");
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z);
                transform.rotation = initialRotation;
                transform.localPosition = initialPosition;
                animateWeapon = false;
            }
        }
    }
}
