using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    object Save();

    void Load(object obj);
}
