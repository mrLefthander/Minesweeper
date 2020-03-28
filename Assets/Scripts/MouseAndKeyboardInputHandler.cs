#if UNITY_STANDALONE || UNITY_WEBGL

using System;
using UnityEngine;

public class MouseAndKeyboardInputHandler : MonoBehaviour, IInputHandler
{
    public void HandleInput(Action<Vector2> OnPrimaryInput, Action<Vector2> OnSecondatyInput, bool enableDebug = false, Action<bool> OnDebugInput = null)
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPrimaryInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnSecondatyInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        EnableDebug(enableDebug, OnDebugInput);
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
#endif