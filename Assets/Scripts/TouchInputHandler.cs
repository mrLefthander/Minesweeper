using System;
using UnityEngine;

public class TouchInputHandler: IInputHandler
{
    private readonly Action<Vector2> OnPrimaryInput;
    private readonly Action<Vector2> OnSecondatyInput;
    private readonly bool enableDebug;
    private readonly Action<bool> OnDebugInput;
    private readonly CameraControlsAndroid cameraControls;

    private readonly float timeToLongTouch = 0.5f;
    private readonly float touchDeltaPositionThreshold = 10f;
    private readonly float sameTouchWorldPositionDistance = 3f;

    private float touchTime = 0f;
    private bool newTouch = false;
    private Vector2 touchZeroStartWorldPosition;

    public TouchInputHandler(Action<Vector2> OnPrimaryInput, Action<Vector2> OnSecondatyInput, bool enableDebug = false, Action<bool> OnDebugInput = null)
    {
        this.OnPrimaryInput = OnPrimaryInput;
        this.OnSecondatyInput = OnSecondatyInput;
        this.enableDebug = enableDebug;
        this.OnDebugInput = OnDebugInput;

        cameraControls = new CameraControlsAndroid();
    }

    public void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touchZero = Input.GetTouch(0);

            EnableDebug(enableDebug, OnDebugInput);

            if (Input.touchCount == 2)
            {
                Touch touchOne = Input.GetTouch(1);

                cameraControls.Zoom(CalculateZoomDistance(touchZero, touchOne));
                newTouch = false;
            }

            if (IsTouchStart(touchZero))
            {
                touchZeroStartWorldPosition = Camera.main.ScreenToWorldPoint(touchZero.position);
                touchTime = Time.time;
                newTouch = true;
            }

            if (IsPanning(touchZero))
            {
                Vector2 touchDeltaPosition = touchZero.deltaPosition;
                cameraControls.Pan(touchDeltaPosition);
                newTouch = false;
            }

            if (IsLongTouch(touchZero))
            {
                Vector2 touchZeroCurrentWorldPosition = Camera.main.ScreenToWorldPoint(touchZero.position);

                if (newTouch && IsOnSameWorldPosition(touchZeroCurrentWorldPosition))
                {
                    OnSecondatyInput(touchZeroStartWorldPosition);
                }
                newTouch = false;
            }

            if (IsShortTouch(touchZero))
            {
                OnPrimaryInput(touchZeroStartWorldPosition);
            }
        }
    }

    private static void EnableDebug(bool enableDebug, Action<bool> OnDebugInput)
    {
        if (enableDebug && OnDebugInput != null)
        {
            OnDebugInput(false);
            if (Input.touchCount == 4)
            {
                OnDebugInput(true);
            }
        }
    }

    private float CalculateZoomDistance(Touch touchZero, Touch touchOne)
    {
        Vector2 touchZeroPreviousPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePreviousPos = touchOne.position - touchOne.deltaPosition;

        float previousMagnitude = (touchZeroPreviousPos - touchOnePreviousPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        return currentMagnitude - previousMagnitude;
    }

    private bool IsOnSameWorldPosition(Vector2 touchZeroCurrentWorldPosition)
    {
        return Vector2.Distance(touchZeroStartWorldPosition, touchZeroCurrentWorldPosition) <= sameTouchWorldPositionDistance;
    }

    private bool IsShortTouch(Touch touchZero)
    {
        return touchTime > 0 && Time.time - touchTime < timeToLongTouch && touchZero.phase == TouchPhase.Ended && newTouch;
    }

    private bool IsLongTouch(Touch touchZero)
    {
        return touchTime > 0 && Time.time - touchTime >= timeToLongTouch && touchZero.phase != TouchPhase.Ended;
    }

    private static bool IsTouchStart(Touch touchZero)
    {
        return touchZero.phase == TouchPhase.Began;
    }

    private bool IsPanning(Touch touchZero)
    {
        return touchZero.phase != TouchPhase.Ended && touchZero.phase == TouchPhase.Moved && touchZero.deltaPosition.magnitude >= touchDeltaPositionThreshold;
    }
}