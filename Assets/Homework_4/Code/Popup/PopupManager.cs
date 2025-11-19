using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MVx
{
    public class PopupManager : IPopupManager, IInitializable
    {
        private readonly Dictionary<string, IPopupView> _popups = new();
        [Inject] private DiContainer _container;
        [Inject] private readonly UIConfig _config;
        [Inject(Optional = true, Id = "Popups")] private Transform _popupRoot;

        public void Initialize()
        {
            foreach (var prefab in _config.popups)
            {
                var instance = _container.InstantiatePrefab(prefab, _popupRoot);
                if (instance.TryGetComponent<IPopupView>(out var popup))
                {
                    _popups[popup.ID] = popup;
                    popup.Hide();
                }
            }
        }

        public void ShowPopup(string ID)
        {
            if (_popups.TryGetValue(ID, out var popup))
            {
                popup.Show();
            }
        }
    }
}