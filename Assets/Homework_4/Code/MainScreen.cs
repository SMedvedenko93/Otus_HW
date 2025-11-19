using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVx
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private Button _openPopupButton;
        private IPopupManager _popupManager;

        [Inject]
        public void Construct(IPopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        private void OnEnable()
        {
            _openPopupButton.onClick.AddListener(OpenPopup);
        }

        private void OnDestroy()
        {
            _openPopupButton.onClick.RemoveListener(OpenPopup);
        }

        void OpenPopup()
        {
            _popupManager.ShowPopup(PopupNames.INVENTORY_POPUP);
        }
    }
}
