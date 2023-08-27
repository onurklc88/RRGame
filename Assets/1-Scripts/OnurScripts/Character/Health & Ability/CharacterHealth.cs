using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    [Inject]
    CharacterStateFactory _characterStateFactory;
  

    public void TakeHeal(int healValue)
    {

    }

    public void TakeDamage(float damageValue)
    {
       EventLibrary.OnPlayerTakeDamage.Invoke(_characterStateFactory.CharacterKnockbackState);
    }
}
