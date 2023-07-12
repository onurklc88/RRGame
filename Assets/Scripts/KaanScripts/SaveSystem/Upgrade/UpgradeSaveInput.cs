using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSaveInput : SaveSystem
{
    public float strengthDamage;

    public float dexterityChargeSpeed;

    public float hasteRunSpeed;
    public float hasteDodgeDelay;

    public float magicPower;

    public UpgradeSaveInput()
    {
        //If play firts time, set these values

        strengthDamage = 5;

        dexterityChargeSpeed = 1.5f;

        hasteRunSpeed = 10;  
        hasteDodgeDelay = 2; //animation time + dodgedelay = dodgetime, example anim time = .5f

        magicPower = 5;
    }

}
