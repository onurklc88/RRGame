using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexterityUpgrade : Upgrade
{

    public override void DoUpgrade()
    {
        if (Check())
        {
            Debug.Log("Dexterity");
            DropCost();
            Level++;
            SetLevelImage();
            SetLevelUpgrade();
        }
    }

    public override void SetLevelUpgrade()
    {
        //Set Upgrade Settings.
    }

    public override bool Check()
    {
        isActive = CheckCost() && CheckLevel();
        return isActive;
    }
}
