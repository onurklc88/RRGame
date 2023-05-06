using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    protected object Value;

    public void Save()
    {
        string save = JsonUtility.ToJson(Value);

        PlayerPrefs.SetString(Value.ToString(), save);
    }
}
