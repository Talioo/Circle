using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map.InputSystem
{
    public interface IInputSystem
    {
        event Action PathStartedEvent;
        IReadOnlyList<Vector3> Path { get; }
    }
}