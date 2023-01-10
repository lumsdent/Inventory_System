using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItem : MonoBehaviour, ISaveable
{
    public string stuff;
    public void Load(object obj)
    {
        SaveObject saveData = (SaveObject)obj;
        stuff = saveData.stuff;
    }

    public object Save()
    {
        return new SaveObject()
        {
            stuff = this.stuff
        };
    }

    [Serializable]
    private struct SaveObject
    {
        //insert the stuff I want to save
        public string stuff;
    }

}
