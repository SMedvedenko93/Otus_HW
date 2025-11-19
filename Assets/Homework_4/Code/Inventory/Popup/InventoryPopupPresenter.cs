using System;
using Zenject;

namespace MVx
{
    public class InventoryPopupPresenter: IInitializable, IDisposable
    {
        private InventorySystem _inventorySystem;
        private IPopupView _view;
        private readonly InventoryItemView.Factory _itemFactory;
        private readonly DiContainer _container;

        public InventoryPopupPresenter(IPopupView view, InventorySystem inventorySystem, InventoryItemView.Factory itemFactory, DiContainer container)
        {
            _inventorySystem = inventorySystem;
            _view = view;
            _itemFactory = itemFactory;
            _container = container;
        }

        public void Initialize()
        {
            _view.OnPopupOpened += UpdateUI;
            CreateInventoryItems();
        }

        public void UpdateUI()
        {
            Clear();

            for (var i = 0; i < _inventorySystem.GetItems().Count; i++)
            {
                var instanceView = _itemFactory.Create(_inventorySystem.GetItems()[i], _view.Content);
                var instancePresenter = _container.Instantiate<InventoryItemPresenter>(new object[] { instanceView });
            }
        }

        void Clear()
        {
            _view.Clear();
        }

        public void Dispose()
        {
            _view.OnPopupOpened -= UpdateUI;
        }

        private void CreateInventoryItems()
        {

        }
    }
}