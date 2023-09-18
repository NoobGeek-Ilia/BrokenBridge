using TMPro;
using UnityEngine;

public class SMissMesseger : MonoBehaviour
{
    private void Update()
    {
        RiseUp();
    }
    private void RiseUp()
    {
        const float speed = 5f;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        FadeEffect();
    }
    private void FadeEffect()
    {
        const float fadeSpeed = 0.01f;
        TextMeshPro messageColor = GetComponent<TextMeshPro>();
        float fadeAmount = messageColor.color.a - fadeSpeed;
        messageColor.color = new Color(messageColor.color.r, messageColor.color.g, messageColor.color.b, fadeAmount);
        if (fadeAmount < 0.01)
            Destroy(gameObject);
    }
}
