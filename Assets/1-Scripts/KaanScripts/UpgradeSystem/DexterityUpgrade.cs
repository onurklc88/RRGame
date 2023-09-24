using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexterityUpgrade : Upgrade
{
    [SerializeField] private float _decreaseChargeSpeed = 0.2f;

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
        SaveInfo.UpgradeSave.DexterityChargeSpeed -= _decreaseChargeSpeed;
        SaveInfo.UpgradeSave.Save();
    }

    public override bool Check()
    {
        _isActive = false;
        if (CheckLevel())
        {
            _isActive = CheckCost();
        }
        return _isActive;
    }
}
