using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteUpgrade : Upgrade
{
    [SerializeField] private float _increaseRunSpeed = 0.2f;
    [SerializeField] private float _decreaseDodgeDelay = 0.35f;

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
        SaveInfo.UpgradeSave.HasteRunSpeed += _increaseRunSpeed;
        SaveInfo.UpgradeSave.HasteDodgeDelay -= _decreaseDodgeDelay;
        SaveInfo.UpgradeSave.Save();
    }

    public override bool Check()
    {
        _isActive = CheckCost() && CheckLevel();
        return _isActive;
    }
}
