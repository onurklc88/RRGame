using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteUpgrade : Upgrade
{
    [SerializeField] private float increaseRunSpeed = 0.2f;
    [SerializeField] private float decreaseDodgeDelay = 0.35f;

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
        SaveInfo._upgradeSave.hasteRunSpeed += increaseRunSpeed;
        SaveInfo._upgradeSave.hasteDodgeDelay -= decreaseDodgeDelay;
        SaveInfo._upgradeSave.Save();
    }

    public override bool Check()
    {
        isActive = CheckCost() && CheckLevel();
        return isActive;
    }
}
