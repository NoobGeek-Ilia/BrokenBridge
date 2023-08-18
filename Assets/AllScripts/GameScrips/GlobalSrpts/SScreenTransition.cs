using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SScreenTransition : MonoBehaviour
{
    public float fadeDuration = 1f; // Длительность затухания и появления
    public Image panel; // Ссылка на панель, которая должна затухать
    public SPlayerMovement playerMovement;
    public SLastElevator lastElevator;
    public StateMonitor monitor;

    private void Awake()
    {
        Application.targetFrameRate = 1000;
    }

    private void Update()
    {
        if (playerMovement.playerOnTargetPlatform)
        {
            FadeOutIn();
        }
    }

    // Затухание и появление экрана
    public void FadeOutIn()
    {
        StartCoroutine(Fade(0, 1f, () =>
        {
            StartCoroutine(Fade(1f, 0, null));
        }));
    }

    // Затухание экрана
    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1f, null));
    }

    // Появление экрана
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0, null));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, System.Action callback)
    {
        Color panelColor = panel.color;
        panelColor.a = startAlpha;
        panel.color = panelColor;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            panelColor.a = currentAlpha;
            panel.color = panelColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelColor.a = endAlpha;
        panel.color = panelColor;

        // Вызов коллбэка, если он указан
        callback?.Invoke();
    }
}