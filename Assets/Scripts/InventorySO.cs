using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Inventory.Model {
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size {get; private set;} = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize() {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i ++) {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (IsInventoryFull())
                return quantity;

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem
                    {
                        item = item,
                        quantity = quantity
                    };
                    InformAboutChange();
                    return 0;
                }
            }

            return quantity;
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        public void RemoveItem(int itemIndex, int amount) {
            if (inventoryItems.Count > itemIndex) {
                if (inventoryItems[itemIndex].IsEmpty) return;

                int remainder = inventoryItems[itemIndex].quantity - amount;

                if (remainder <= 0) inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                //else inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuant
                InformAboutChange();
            }
        }

        public void AddItem(InventoryItem item) {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState() {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++) {
                if (inventoryItems[i].IsEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex) {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2) {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }    

        private void InformAboutChange() {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem {

        public int quantity;
        public ItemSO item;

        public bool IsEmpty => item == null;

        public Sprite ItemImage => item != null ? item.ItemImage : null;

        public static InventoryItem GetEmptyItem() => new InventoryItem {
            item = null, quantity = 0,
        };
        
    }
}
