using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeSave : UpgradeSaveInput
{
    public UpgradeSave()
    {
        base.Value = this;
    }
    
}
