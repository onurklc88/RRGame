using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    [Inject]
    CharacterStateFactory _characterStateFactory;
    private float _currentHealth;
    private float _totalHealth = 2f;
    public void TakeHeal(int healValue)
    {

    }

   


    public void TakeDamage(float damageValue)
    {
        GetComponent<DamageFlash>().Flash();
        EventLibrary.OnPlayerTakeDamage.Invoke(_characterStateFactory.CharacterKnockbackState);
    }
}
