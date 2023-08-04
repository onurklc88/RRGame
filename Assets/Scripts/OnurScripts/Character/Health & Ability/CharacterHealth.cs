using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    private CharacterStateFactory _characterStateFactory = new CharacterStateFactory();
  

    public void TakeHeal(int healValue)
    {

    }

    public void TakeDamage(float damageValue)
    {
       EventLibrary.OnPlayerTakeDamage.Invoke(_characterStateFactory.CharacterKnockbackState);
    }
}
