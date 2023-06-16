using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    public static event Action PlayerKnockbackEvent; 

    public void TakeHeal(int healValue)
    {

    }

    public void TakeDamage(int damageValue)
    {
        PlayerKnockbackEvent?.Invoke();
        Debug.Log("Aww..its hurts");
    }
}
