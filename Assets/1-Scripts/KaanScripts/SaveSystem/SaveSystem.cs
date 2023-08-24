using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    protected object _value;

    public void Save()
    {
        string save = JsonUtility.ToJson(_value);

        PlayerPrefs.SetString(_value.ToString(), save);
    }
}
