using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    
    [SerializeField] protected string _upgradeName;
    [SerializeField] protected List<GameObject> _levelImage = new List<GameObject>();
    [SerializeField] protected int _level = 0;
    [SerializeField] protected int _maxLevel = 5;



    protected bool _isActive;
    protected List<int> _costList = new List<int>(5) { 400,600,800,1000,1500 };

    public int Level
    {
        get
        {
            _level = PlayerPrefs.GetInt(_upgradeName + "Level");
            return _level;
        }
        set
        {
            _level = value;
            PlayerPrefs.SetInt(_upgradeName+"Level", _level);
        }
    }

    public virtual void Start()
    {
        SetLevelImage();
    }

    public abstract void DoUpgrade();
    public abstract void SetLevelUpgrade();
    public abstract bool Check();

    

    protected void DropCost() { EconomyManager.instance.Money -= _costList[Level]; }
    protected bool CheckCost() { return _costList[Level] <= EconomyManager.instance.Money; }
    protected bool CheckLevel() { return Level < _maxLevel; }
    protected void SetLevelImage()
    {
        if (Level > 0)
        {
            for (int i = 0; i < Level; i++)
            {
                _levelImage[i].SetActive(true);
            }
        }
    }


}
