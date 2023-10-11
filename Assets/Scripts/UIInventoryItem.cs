using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Inventory.UI {
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouse;

        private bool empty = true;

        public void Awake () {
            ResetData();
        }

        public void ResetData()
        {
            if (itemImage != null)
            {
                itemImage.gameObject.SetActive(false);
            }

            empty = true;
        }

       public void SetData(Sprite sprite, int quantity) {

            if (itemImage == null)
            {
                Debug.LogWarning("ItemImage is null or destroyed.");
                return;
            }
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            empty = false;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (empty) {
                return;
            }

            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData) {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData) {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData pointerData) {

            if (pointerData.button == PointerEventData.InputButton.Right) {
                OnRightMouse?.Invoke(this);
            }

            else {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnDrag(PointerEventData eventData) {
        }


    }
}
