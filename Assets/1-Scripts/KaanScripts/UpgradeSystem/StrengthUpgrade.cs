using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthUpgrade : Upgrade
{
    //1.2x first level, every level increase 0.2x and 5th level max damage 2.0x

    [SerializeField] private float _increaseDamage = 0.2f;
    
    public override void DoUpgrade()
    {
        if (Check())
        {
            DropCost();
            Level++;
            SetLevelImage();
            SetLevelUpgrade();
        }
    }

    public override void SetLevelUpgrade()
    {
        SaveInfo.UpgradeSave.StrengthDamage += _increaseDamage;
        SaveInfo.UpgradeSave.Save();


    }

    public override bool Check()
    {
        _isActive = CheckCost() && CheckLevel();
        return _isActive;
    }
}
