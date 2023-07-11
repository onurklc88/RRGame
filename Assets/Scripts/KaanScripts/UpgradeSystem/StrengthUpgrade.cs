using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthUpgrade : Upgrade
{
    //1.2x first level, every level increase 0.2x and 5th level max damage 2.0x

    [SerializeField] private float increaseDamage = 0.2f;
    
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
        SaveInfo._upgradeSave.strengthDamage += increaseDamage;
        SaveInfo._upgradeSave.Save();
    }

    public override bool Check()
    {
        isActive = CheckCost() && CheckLevel();
        return isActive;
    }
}
