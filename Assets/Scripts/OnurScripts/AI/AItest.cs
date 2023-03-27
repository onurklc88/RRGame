using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItest : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damageValue)
    {
        Debug.Log("I got hit");
    }

}
