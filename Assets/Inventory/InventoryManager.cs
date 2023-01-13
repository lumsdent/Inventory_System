using Assets.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] slots;

    public GameObject itemPrefab;

    int selectedSlot = -1;
    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 9) { ChangeSelectedSlot(number - 1); }
        }
    }
    void ChangeSelectedSlot(int inputValue)
    {
        if(selectedSlot >= 0)
        {
            slots[selectedSlot].Deselect();
        }
        slots[inputValue].Select();
        selectedSlot = inputValue;

    }
    public bool AddItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null &&
                inventoryItem.item == item && 
                inventoryItem.stackCount < item.maxStack)
            {
                Debug.Log("Adding same item here");
                inventoryItem.stackCount++;
                inventoryItem.RefreshCountText();
                return true;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (!inventoryItem)
            {
                SpawnItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnItem(Item item, InventorySlot itemSlot)
    {
        GameObject newItemGO = Instantiate(itemPrefab, itemSlot.transform);
        InventoryItem newItem = newItemGO.GetComponent<InventoryItem>();
        newItem.InitializeItem(item);
    }

    public Item GetSelectedItem()
    {
        InventorySlot slot = slots[selectedSlot];
        InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
        if(inventoryItem != null)
        {
            if(inventoryItem.item.consumable) { 
                inventoryItem.stackCount--; 
                if(inventoryItem.stackCount <= 0)
                {
                    Destroy(inventoryItem.gameObject);
                }
                else
                {
                    inventoryItem.RefreshCountText(); }
                }
            return inventoryItem.item;
        }
        return null;
    }
}
