using UnityEngine;

public class STouchDetection : MonoBehaviour
{
    public event OnTouchInput TouchEvent;
    public delegate void OnTouchInput(ActionTipe action);

    internal protected bool isTouching { get; private set; }
    internal protected TouchPhase touchPhase { get; private set; }

    private Vector2 tapPos;
    private Vector2 swipeDelta;
    private float deadZone = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TouchEvent?.Invoke(ActionTipe.Hit);
        }
        
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isTouching = true;
                tapPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchPhase = Input.GetTouch(0).phase;
                ResetTouch();
            }
            CheckTouch();
        }
    }

    private void CheckTouch()
    {
        swipeDelta = Vector2.zero;
        if (isTouching)
        {
            if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - tapPos;
        }
        if (swipeDelta.magnitude > deadZone)
        {
            if (TouchEvent != null)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    TouchEvent?.Invoke(swipeDelta.x > 0 ? ActionTipe.Right : ActionTipe.Left);
                else
                    TouchEvent?.Invoke(swipeDelta.y > 0 ? ActionTipe.Jump : ActionTipe.Hit);
            }
            ResetTouch();
        }
        else
        {
            //одинарный и двойной тач
        }
    }

    private void ResetTouch()
    {
        isTouching = false;
        tapPos = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
    public enum ActionTipe
    {
        None,
        Left,
        Right,
        Jump,
        Hit,
    }
}
