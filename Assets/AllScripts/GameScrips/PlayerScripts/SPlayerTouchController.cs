using UnityEngine;
public class SPlayerTouchController : MonoBehaviour
{
    public float swipeThreshold = 50f;
    public float doubleTapTimeThreshold = 0.2f; // Максимальный интервал между двойными нажатиями

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float lastTapTime;

    private bool detectSwipe = false;
    internal protected bool jump = false;
    internal protected bool hit = false;
    internal protected float pos = 2.47f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
                detectSwipe = true;

                if ((Time.time - lastTapTime) < doubleTapTimeThreshold)
                {
                    // Двойное нажатие обнаружено
                    Debug.Log("Double Tap");
                    // Выполняйте действия при двойном нажатии здесь
                }
                lastTapTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                detectSwipe = true;
            }
        }

        if (detectSwipe)
        {
            float swipeDistance = Vector2.Distance(fingerDownPosition, fingerUpPosition);

            if (swipeDistance > swipeThreshold)
            {
                Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

                if (swipeDirection.y > swipeThreshold) // Свайп вверх
                {
                    Jump();
                }
                else if (swipeDirection.y < -swipeThreshold) // Свайп вниз
                {
                    Hit();
                }
                else if (swipeDirection.x > swipeThreshold) // Свайп вправо
                {
                    pos -= 1;
                }
                else if (swipeDirection.x < -swipeThreshold) // Свайп влево
                {
                    pos += 1;
                }
            }
            pos = Mathf.Clamp(pos, 1.47f, 3.47f);
            fingerDownPosition = fingerUpPosition;
            detectSwipe = false;
        }
    }

    private void Jump()
    {
        jump = true;
    }

    private void Hit()
    {
        hit = true;
    }
}

