using System;
using UnityEngine;

namespace ShootEmUpZenject
{
    public interface IInputService
    {
        Vector2 GetMovementInput();
        event Action OnFirePressed;
    }
}