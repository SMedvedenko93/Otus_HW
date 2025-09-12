using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameUpdateListener
    {
        public float HorizontalDirection { get; private set; }
        public event Action OnFirePressed;
        public event Action<float> OnHorizontalMovement;

        public void CustomUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFirePressed?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }

            if (this.HorizontalDirection != 0)
                OnHorizontalMovement?.Invoke(this.HorizontalDirection);
        }
    }
}