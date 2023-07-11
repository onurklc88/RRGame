using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUpgrade : Upgrade
{
    //1.3x first level, every level increase 0.3x and 5th level max damage 2.5x

    [SerializeField] private float increaseMagic = 0.3f;

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
        Debug.Log(SaveInfo._upgradeSave.magicPower);
        SaveInfo._upgradeSave.magicPower += increaseMagic;
        Debug.Log(SaveInfo._upgradeSave.magicPower);
        SaveInfo._upgradeSave.Save();
    }

    public override bool Check()
    {
        isActive = CheckCost() && CheckLevel();
        return isActive;
    }
}
