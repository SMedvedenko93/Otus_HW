using System.Collections.Generic;
using UnityEngine;

namespace MVx
{
    [CreateAssetMenu(menuName = "Game/Configs/InventoryBase")]
    public class InventoryBase : ScriptableObject
    {
        public List<InventoryItem> items;
    }
}