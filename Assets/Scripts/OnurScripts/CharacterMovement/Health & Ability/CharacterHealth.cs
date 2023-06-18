using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    private CharacterStateFactory _characterStateFactory = new CharacterStateFactory();
    public static event Action<CharacterBaseState> PlayerKnockbackEvent; 

    public void TakeHeal(int healValue)
    {

    }

    public void TakeDamage(int damageValue)
    {
        Debug.Log("Aww..its hurts");
        PlayerKnockbackEvent?.Invoke(_characterStateFactory.CharacterKnockbackState);
    }
}
