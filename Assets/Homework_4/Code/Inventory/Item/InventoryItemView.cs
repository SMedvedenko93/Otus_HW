using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVx
{
    public class InventoryItemView : MonoBehaviour, IInventoryItemView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _button;
        private Transform _parent;
        private InventoryItem _inventoryItem;

        public event Action OnBuyButtonClick;

        [Inject]
        void Construct(InventoryItem inventoryItem, Transform parent)
        {
            _inventoryItem = inventoryItem;
            _parent = parent;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(BuyButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(BuyButtonClick);
        }

        private void BuyButtonClick()
        {
            Debug.Log("Spend : " + _inventoryItem.Price);
            OnBuyButtonClick?.Invoke();
        }

        public void UpdateUI()
        {
            _icon.sprite = _inventoryItem.Sprite;
            _price.SetText("{0}", _inventoryItem.Price);
            transform.SetParent(_parent);
        }

        public class Factory : PlaceholderFactory<InventoryItem, Transform, InventoryItemView> { }
    }

    public interface IInventoryItemView
    {
        void UpdateUI();
        event Action OnBuyButtonClick;
    }
}
