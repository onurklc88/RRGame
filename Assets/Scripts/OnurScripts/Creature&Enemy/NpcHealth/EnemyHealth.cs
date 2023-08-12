using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   // public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    private float _currentHealth;
    private bool _decalSet;
    GameObject decal;
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
            
            if(_currentHealth == 2.5f)
            {

                decal = ObjectPool.GetPooledObject(2);
                decal.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                decal.transform.SetParent(transform);
                decal.transform.position = transform.position;
                decal.SetActive(true);
                Debug.Log("A");
                decal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 5f);
            }
            else if(_currentHealth < 2)
            {
                Debug.Log("B");
                decal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 10f);
            }
            
        }
        Debug.Log("current Health" + _currentHealth);
    }

}
