using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private TMP_Text MoneyText; //Temporary

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
            MoneyText.text = _money.ToString();
            AddMoney();
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

    private void AddMoney()
    {
        MoneyText.DOComplete();

        Sequence seq = DOTween.Sequence();

        //seq.Append(MoneyText.DOFade(1, 1));
        seq.Append(MoneyText.transform.DOScale(.2f, .3f).SetRelative().SetLoops(2, LoopType.Yoyo));
        //seq.Append(MoneyText.DOFade(0, 1));
    }

}
