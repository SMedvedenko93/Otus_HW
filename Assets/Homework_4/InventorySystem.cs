using System.Collections.Generic;
using Zenject;

namespace MVx
{
    public class InventorySystem: IInitializable
    {
        private InventoryBase _inventoryBase;
        private List<InventoryItem> items = new();

        public InventorySystem(InventoryBase inventoryBase)
        {
            _inventoryBase = inventoryBase;
        }

        public void Initialize()
        {
            items = _inventoryBase.items;
        }

        public List<InventoryItem> GetItems()
        {
            return items;
        }


    }
}