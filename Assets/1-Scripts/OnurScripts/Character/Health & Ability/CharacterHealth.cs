using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    [Inject]
    CharacterStateFactory _characterStateFactory;

    DamageFlash _damageFlash;
    private float _currentHealth;
    private float _totalHealth = 2f;

    private void Awake()
    {
        _damageFlash = GetComponentInChildren<DamageFlash>();
    }
    public void TakeHeal(int healValue)
    {

    }

   


    public void TakeDamage(float damageValue)
    {
        _damageFlash.Flash();
        EventLibrary.OnPlayerTakeDamage.Invoke(_characterStateFactory.CharacterKnockbackState);
    }
}
