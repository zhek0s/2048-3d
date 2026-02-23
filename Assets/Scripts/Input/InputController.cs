using Assets.Scripts.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InputSystem
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private float launchForce = 10f;
        [SerializeField] private float xLimit = 2f;
        private Cube currentCube;
        private Rigidbody currentRb;
        private float targetX;
        private bool isDragging;

        public event Action OnCubeLaunched;

        public void SetCube(Cube cube)
        {
            currentCube = cube;
            if (cube != null)
            {
                currentRb = cube.gameObject.GetComponent<Rigidbody>();
                targetX = cube.transform.position.x;
            }
        }

        void Update()
        {
            if (currentCube == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUI()) return;

                isDragging = true;
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                float mouseX = Input.mousePosition.x;
                float t = Mathf.InverseLerp(0, Screen.width, mouseX);
                targetX = Mathf.Lerp(-xLimit, xLimit, t);
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                currentCube.Launch(launchForce);
                currentCube = null;
                currentRb = null;
                isDragging = false;
                OnCubeLaunched.Invoke();
            }
        }

        void FixedUpdate()
        {
            if (currentRb == null) return;

            if (isDragging)
            {
                Vector3 pos = currentRb.position;
                pos.x = Mathf.Clamp(targetX, -xLimit, xLimit);

                currentRb.MovePosition(pos);
            }
        }

        private bool IsPointerOverUI()
        {
#if UNITY_EDITOR
            return EventSystem.current.IsPointerOverGameObject();
#else
    if (Input.touchCount > 0)
    {
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }
    return false;
#endif
        }
    }
}