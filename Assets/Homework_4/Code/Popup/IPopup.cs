using System;
using UnityEngine;

namespace MVx
{
    public interface IPopupView
    {
        string ID { get; }
        Transform Content { get; }
        void Hide();
        void Show();
        void UpdateUI();
        void Clear();

        event Action OnPopupOpened;
        event Action OnPopupClosed;
    }
}