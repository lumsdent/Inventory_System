using System;
using System.Collections;
using UnityEngine;

namespace Assets.Inventory
{
    public class DemoScript : MonoBehaviour
    {
        public InventoryManager inventoryManager;
        public Item[] itemsToPickup;

        public void PickItem(int id)
        {
            bool result = inventoryManager.AddItem(itemsToPickup[id]);
            Debug.Log(result ? "Added an item" : "Inventory is full");
        }

        public void SelectItem()
        {
            Item item = inventoryManager.GetSelectedItem();
            Debug.Log(item);
        }
    }
}