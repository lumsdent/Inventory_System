using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Inventory
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public Sprite sprite;
        public bool stackable;
        public int maxStack;
        public bool consumable;


    }
}