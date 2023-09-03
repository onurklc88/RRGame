using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   // public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private GameObject _character;
    private float _currentHealth;
    private bool _isDecalSet;
   
    private GameObject _splashDecal;
    private DamageFlash _damageFlash;
    private Creature _creature;
    [Inject]
    EnemyStateFactory _enemyStateFactory;

    private void Awake()
    {
        _currentHealth = _enemyProperties.EnemyHealth;
        _creature = GetComponent<Creature>();
        _damageFlash = GetComponent<DamageFlash>();
    }

   
    public void TakeDamage(float damageValue)
    {
        _damageFlash.Flash();

        if(_currentHealth <= 0.5f)
        {
            //  _splashDecal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 1f);
            // EventLibrary.ResetPooledObject.Invoke(_splashDecal);
            _creature.SwitchState(_enemyStateFactory.Death);
            Debug.Log("A");
            //Vfx Sound and Pool
        }
        else
        {
            
            _currentHealth -= damageValue;
            _creature.PlayerCharacter = _character;
           _creature.SwitchState(_enemyStateFactory.Damage);
            Debug.Log("B");
            /*
             if (!_isDecalSet)
                 SetSplashDecal();
            else if(_currentHealth < 2f)
                _splashDecal.GetComponent<MeshRenderer>().materials[0].SetFloat("_PulseSpeed", 10f);
            */
        }
       // Debug.Log("current health: " + _currentHealth);
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
