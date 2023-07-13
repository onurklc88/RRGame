using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   // public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    private float _currentHealth;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    private void Start()
    {
        _currentHealth = _enemyProperties.EnemyHealth;
    }
    public void TakeDamage(float damageValue)
    {
        
        if(_currentHealth <= 0)
        {
          
            //Vfx Sound and Pool
        }
        else
        {
            _currentHealth -= damageValue;
        }
        Debug.Log("current Health" + _currentHealth);
    }

}
