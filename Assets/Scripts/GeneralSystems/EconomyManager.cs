using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    private int money;

    public int setMoney;
    
    public int Money
    {
        get
        {
            //money = PlayerPrefs.GetInt("Money");
            return money;
        }
        set
        {
            money = value;
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
        Money = setMoney;
    }

}
