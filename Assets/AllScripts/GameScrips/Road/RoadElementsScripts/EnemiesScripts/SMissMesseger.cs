using TMPro;
using UnityEngine;

public class SMissMesseger : MonoBehaviour
{
    private const float speed = 5f;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        FadeEffect();
    }
    private void FadeEffect()
    {
        const float fadeSpeed = 0.03f;
        TextMeshPro messageColor = GetComponent<TextMeshPro>();
        float fadeAmount = messageColor.color.a - fadeSpeed;

        messageColor.color = new Color(messageColor.color.r, messageColor.color.g, messageColor.color.b, fadeAmount);
        if (fadeAmount < 0.01)
            Destroy(gameObject);
    }
}
