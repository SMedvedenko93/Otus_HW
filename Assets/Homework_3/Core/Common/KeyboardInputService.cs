using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class KeyboardInputService : IInputService, ITickable
    {
        public event Action OnFirePressed;

        public Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), 0);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFirePressed?.Invoke();
            }   
        }
    }
}