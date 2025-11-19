using System;

namespace MVx
{
    public class InventoryItemPresenter : IDisposable
    {
        private readonly IInventoryItemView _view;

        public InventoryItemPresenter(IInventoryItemView view)
        {
            _view = view;
            _view.OnBuyButtonClick += BuyItem;
            Initialize();
        }

        private void BuyItem()
        {
            //BUY ITEM
        }

        public void Dispose()
        {
            _view.OnBuyButtonClick -= BuyItem;
        }

        public void Initialize()
        {
            _view.UpdateUI();
        }
    }
}