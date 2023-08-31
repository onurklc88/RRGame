using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   // public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    private float _currentHealth;
    private bool _isDecalSet;
    private GameObject _splashDecal;
    private DamageFlash _damageFlash;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    private void Start()
    {
        _currentHealth = _enemyProperties.EnemyHealth;
        _damageFlash = GetComponent<DamageFlash>();
    }
    public void TakeDamage(float damageValue)
    {
        _damageFlash.Flash();

        if(_currentHealth <= 0)
        {
            _splashDecal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 1f);
            EventLibrary.ResetPooledObject.Invoke(_splashDecal);

            //Vfx Sound and Pool
        }
        else
        {
            _currentHealth -= damageValue;
           // Debug.Log("Current HEalth: " + _currentHealth);
            if (!_isDecalSet)
                 SetSplashDecal();
            else if(_currentHealth < 2f)
                _splashDecal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 10f);

        }
       
    }

    private void SetSplashDecal()
    {
        _isDecalSet = true;
        _splashDecal = ObjectPool.GetPooledObject(2);
        _splashDecal.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _splashDecal.transform.SetParent(transform);
        _splashDecal.transform.position = transform.position;
        _splashDecal.SetActive(true);
        _splashDecal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 5f);
    }

}
