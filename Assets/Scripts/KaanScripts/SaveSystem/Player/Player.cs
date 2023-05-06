using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player : PlayerSaveInput
{
    public Player()
    {
        base.Value = this;
    }
}
