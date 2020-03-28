using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    void HandleInput(Action<Vector2> OnPrimaryInput, Action<Vector2> OnSecondatyInput, bool enableDebug = false, Action<bool> OnDebugInput = null);
}
