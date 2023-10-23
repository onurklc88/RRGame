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
        _currentHealth = _totalHealth;
    }
    public void TakeHeal(int healValue)
    {

    }

   


    public void TakeDamage(float damageValue, DamageType.Damage currentDamageType)
    {
        _currentHealth -= damageValue;
        Debug.Log("Vava: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Debug.Log("Character Dead");
            //EventLibrary.OnPlayerDead.Invoke();
        }
        else
        {
            _damageFlash.Flash();
            _characterStateFactory.CurrentDamageType = currentDamageType;
            EventLibrary.OnPlayerTakeDamage.Invoke(_characterStateFactory.CharacterKnockbackState);
        }
       
    }
}
