using System.Collections.Generic;
using UnityEngine;

namespace Map.InputSystem
{
    public interface IInputSystem
    {
        IReadOnlyList<Vector3> Path { get; }
    }
}