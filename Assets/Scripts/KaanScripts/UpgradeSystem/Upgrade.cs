using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    
    [SerializeField] protected string upgradeName;
    [SerializeField] protected List<GameObject> levelImage = new List<GameObject>();
    [SerializeField] protected int level = 0;
    [SerializeField] protected int maxLevel = 5;



    protected bool isActive;
    protected List<int> costList = new List<int>(5) { 400,600,800,1000,1500 };

    public int Level
    {
        get
        {
            level = PlayerPrefs.GetInt(upgradeName + "Level");
            return level;
        }
        set
        {
            level = value;
            PlayerPrefs.SetInt(upgradeName+"Level", level);
        }
    }

    public virtual void Start()
    {
        SetLevelImage();
    }

    public abstract void DoUpgrade();
    public abstract void SetLevelUpgrade();
    public abstract bool Check();

    

    protected void DropCost() { EconomyManager.instance.Money -= costList[Level]; }
    protected bool CheckCost() { return costList[Level] <= EconomyManager.instance.Money; }
    protected bool CheckLevel() { return Level < maxLevel; }
    protected void SetLevelImage()
    {
        if (Level > 0)
        {
            for (int i = 0; i < Level; i++)
            {
                levelImage[i].SetActive(true);
            }
        }
    }


}
