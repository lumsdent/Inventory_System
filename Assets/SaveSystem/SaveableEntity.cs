using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveableEntity : MonoBehaviour
{

    [SerializeField] private string id;

    public string Id => id;

    
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }

    public object SaveEntity()
    {
        var state = new Dictionary<string, object>();
        foreach(var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.Save();
        }
        return state;
    }

    public void LoadEntity(object state)
    {
        var stateDictionary = (Dictionary<string, object>) state;
        foreach(var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();
            if( stateDictionary.TryGetValue(typeName, out object savedState))
            {
                saveable.Load(savedState);
            }

        }
    }
}
