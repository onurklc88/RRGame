using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    private int _currentHealth;
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
    public void TakeDamage(int damageValue)
    {
        
        if(_currentHealth <= 0)
        {
            OnEnemyDie?.Invoke();
            //Vfx Sound and Pool
        }
        else
        {
            _currentHealth -= damageValue;
        }
        Debug.Log("current Health" + _currentHealth);
    }

}
