using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveInfo
{
    public static Player _player;
    public static UpgradeSave _upgradeSave;


    public static void Init()
    {
        _player = Load<Player>();
        _upgradeSave = Load<UpgradeSave>();
    }


    public static TClass Load<TClass>() where TClass : new()
    {
        TClass tClass;
        string save = PlayerPrefs.GetString(typeof(TClass).ToString());

        if(save.Length != 0) // First save of game
        {
            tClass = JsonUtility.FromJson<TClass>(save);
        }
        else
        {
            tClass = new TClass();
        }

        return tClass;

    }

}
