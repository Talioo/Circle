using System;
using System.Collections.Generic;
using Map.InputSystem;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class InputManager : ITickable, IInputSystem
    {
        private const float PointsPathTimeDelta = 0.1f;

        private readonly Camera _mainCamera;
        private readonly List<Vector3> _path = new List<Vector3>();

        private float _lastPointTime;

        public InputManager(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public event Action PathStartedEvent;
        public IReadOnlyList<Vector3> Path => _path;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchBegin(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                TouchMove(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                TouchEnd(Input.mousePosition);
            }
        }

        private void TouchMove(Vector3 mousePosition)
        {
            if (Time.time - _lastPointTime > PointsPathTimeDelta)
            {
                _lastPointTime = Time.time;
                _path.Add(ScreenToWorld(mousePosition));
            }
        }

        private void TouchBegin(Vector3 mousePosition)
        {
            _path.Clear();

            _lastPointTime = Time.time;
            _path.Add(ScreenToWorld(mousePosition));
            PathStartedEvent?.Invoke();
        }

        private void TouchEnd(Vector3 mousePosition)
        {
            _lastPointTime = 0;
            _path.Add(ScreenToWorld(mousePosition));
        }

        private Vector3 ScreenToWorld(Vector3 screenPosition)
        {
            return _mainCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}