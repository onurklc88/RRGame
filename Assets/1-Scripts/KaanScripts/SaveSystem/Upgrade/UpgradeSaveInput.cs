using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSaveInput : SaveSystem
{
    public float StrengthDamage;

    public float DexterityChargeSpeed;

    public float HasteRunSpeed;
    public float HasteDodgeDelay;

    public float MagicPower;

    public UpgradeSaveInput()
    {
        //If play firts time, set these values

        StrengthDamage = 0;

        DexterityChargeSpeed = 1.5f;

        HasteRunSpeed = 10;  
        HasteDodgeDelay = 2; //animation time + dodgedelay = dodgetime, example anim time = .5f

        MagicPower = 5;
    }

}
