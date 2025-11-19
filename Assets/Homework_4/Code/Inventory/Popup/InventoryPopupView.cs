using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVx
{
    public class InventoryPopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private string _id;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _content;

        public event Action OnPopupOpened;
        public event Action OnPopupClosed;

        public Transform Content => _content;

        public string ID => _id;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(ClosePopup);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(ClosePopup);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            OnPopupOpened?.Invoke();
            gameObject.SetActive(true);
        }

        private void ClosePopup()
        {
            Hide();
        }

        public void UpdateUI()
        {

        }

        public void Clear()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
        }

    }
}

