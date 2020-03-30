using System;
using UnityEngine;

public class MouseAndKeyboardInputHandler: IInputHandler
{
    private readonly Action<Vector2> OnPrimaryInput;
    private readonly Action<Vector2> OnSecondatyInput;
    private readonly bool enableDebug;
    private readonly Action<bool> OnDebugInput;

    public MouseAndKeyboardInputHandler(Action<Vector2> OnPrimaryInput, Action<Vector2> OnSecondatyInput, bool enableDebug = false, Action<bool> OnDebugInput = null)
    {
        this.OnPrimaryInput = OnPrimaryInput;
        this.OnSecondatyInput = OnSecondatyInput;
        this.enableDebug = enableDebug;
        this.OnDebugInput = OnDebugInput;
    }

    public void HandleInput()
    {
        MouseControls();

        EnableDebug(enableDebug, OnDebugInput);
    }

    private void MouseControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPrimaryInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnSecondatyInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void EnableDebug(bool enableDebug, Action<bool> OnDebugInput)
    {
        if (enableDebug && OnDebugInput != null)
        {
            if (Input.GetKeyDown(KeyCode.D))
                OnDebugInput(true);

            if (Input.GetKeyUp(KeyCode.D))
                OnDebugInput(false);
        }
    }

}