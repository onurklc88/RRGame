using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveInfo
{
    public static Player Player;
    public static UpgradeSave UpgradeSave;
   

    public static void Init()
    {
        Player = Load<Player>();
        UpgradeSave = Load<UpgradeSave>();
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
