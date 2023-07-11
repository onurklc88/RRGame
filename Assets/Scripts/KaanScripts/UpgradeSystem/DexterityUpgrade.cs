using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexterityUpgrade : Upgrade
{
    [SerializeField] private float decreaseChargeSpeed = 0.2f;

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
        SaveInfo._upgradeSave.dexterityChargeSpeed -= decreaseChargeSpeed;
        SaveInfo._upgradeSave.Save();
    }

    public override bool Check()
    {
        isActive = CheckCost() && CheckLevel();
        return isActive;
    }
}
