using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach to Game Object
public class SaveableEntity : MonoBehaviour
{

    [SerializeField] private string id;

    public string Id => id;

    //creates a universal id to identify data in database
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }

    //Save load all ISaveable data on Game Object
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
