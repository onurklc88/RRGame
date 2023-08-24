using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    private int _money;

    public int SetMoney;
    
    public int Money
    {
        get
        {
            //money = PlayerPrefs.GetInt("Money");
            return _money;
        }
        set
        {
            _money = value;
            //PlayerPrefs.SetInt("Money", money);
        }
    }

    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        Money = SetMoney;
    }

}
