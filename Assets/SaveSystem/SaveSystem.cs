using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.dat";

    //Abstraction methods for calling from presentation layer
    void Save()
    {
        var state = LoadFromFile();
        SaveGame(state);
        SaveToFile(state);
    }

    void Load()
    {
        var state = LoadFromFile();
        LoadGame(state);
    }

    //specific implementation for saving/loading using a file
    void SaveToFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            new BinaryFormatter().Serialize(stream, state); 
        }
    }

    Dictionary<string, object> LoadFromFile()
    {
        if(!File.Exists(SavePath))
        {
            //TODO Resolve later
            return null;
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            return (Dictionary<string, object>)new BinaryFormatter().Deserialize(stream);
        }
    }

    //Combine all SaveableEntity into a single dictionary for persistance
   void SaveGame(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveEntity();
        }
    }

    void LoadGame(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadEntity(savedState);
            }    
        }
    }
}