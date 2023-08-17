using UnityEngine;
public class SPlayerTouchController : MonoBehaviour
{
    public float swipeThreshold = 20f;
    public float doubleTapTimeThreshold = 0.2f; // ������������ �������� ����� �������� ���������

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float lastTapTime;

    private bool detectSwipe = false;
    internal protected bool jump = false;
    internal protected bool hit = false;
    internal protected float pos;
    public SPlatform platform;
    private float centerRoad;

    private void Start()
    {
        centerRoad = platform.GetPlatformPositionInfo().z;

    }
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
                    // ������� ������� ����������
                    Debug.Log("Double Tap");
                    // ���������� �������� ��� ������� ������� �����
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

                if (swipeDirection.y > swipeThreshold) // ����� �����
                {
                    Jump();
                }
                else if (swipeDirection.y < -swipeThreshold) // ����� ����
                {
                    Hit();
                }
                else if (swipeDirection.x > swipeThreshold) // ����� ������
                {
                    pos -= 1.3f;
                }
                else if (swipeDirection.x < -swipeThreshold) // ����� �����
                {
                    pos += 1.3f;
                }
            }
            pos = Mathf.Clamp(pos, centerRoad - 1.3f, centerRoad + 1.3f);
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

