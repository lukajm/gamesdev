using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.UI {
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        private void Awake() {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDraggedItem = -1;

        public event Action<int> OnDescriptionRequested, OnStartDragging, OnItemActionRequested;
        public event Action<int, int> OnSwapItems;

        public void InitializeInventoryUI(int size) {
            for (int i = 0; i < size; i++) {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = Vector3.one;
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouse += HandleShowItemActions;
                uiItem.OnItemDroppedOn += HandleSwap;
            }
        }

        internal void ResetAllItems() {
            foreach (var item in listOfUIItems) {
                item.ResetData();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string Description) {
            itemDescription.SetDescription(itemImage, name, Description);
            //DeselectAllItems();
            //listOfUIItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity) {
            if (listOfUIItems.Count > itemIndex) {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI) {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleBeginDrag (UIInventoryItem inventoryItemUI) {

            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) return;
            currentlyDraggedItem = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity) {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI) {
            int index = listOfUIItems.IndexOf(inventoryItemUI);

            if (index == -1) {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag (UIInventoryItem inventoryItemUI) {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI) {
            int index = listOfUIItems.IndexOf(inventoryItemUI);

            if (index == -1) {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItem, index);
            HandleItemSelection(inventoryItemUI);
        }
        
        public void Show() {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection() {
            itemDescription.ResetDescription();
        }

        private void ResetDraggedItem() {
            mouseFollower.Toggle(false);
            currentlyDraggedItem = -1;
        }

        public void Hide() {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }
    }

}