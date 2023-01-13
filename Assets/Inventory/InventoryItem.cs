using Assets.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public Item item;
    public Image image;
    public TextMeshProUGUI countText;

    [HideInInspector]
    public Transform parentAfterDrag;
    [HideInInspector]
    public int stackCount = 1;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.sprite;
        RefreshCountText();
    }

    public void RefreshCountText()
    {
        countText.text = stackCount.ToString();
        bool textActive = stackCount > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        countText.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        countText.raycastTarget = true ;
    }

}
