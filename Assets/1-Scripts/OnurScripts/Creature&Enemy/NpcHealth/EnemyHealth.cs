using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   // public static event Action OnEnemyDie;
    [SerializeField] private EnemyProperties _enemyProperties;
    //[SerializeField] private GameObject _character;
    private float _currentHealth;
    private bool _isDecalSet;
   
    private GameObject _splashDecal;
    private DamageFlash _damageFlash;
    private Creature _creature;
    [Inject]
    EnemyStateFactory _enemyStateFactory;
    [Inject]
    CharacterStateManager _character;
    private void Awake()
    {
        _currentHealth = _enemyProperties.EnemyHealth;
        _creature = GetComponent<Creature>();
        _damageFlash = GetComponent<DamageFlash>();
    }

   
    public void TakeDamage(float damageValue, DamageType.Damage currentDamage)
    {
        _damageFlash.Flash();

        if(_currentHealth <= 0.5)
        {
            
            _creature.SwitchState(_enemyStateFactory.Death);
           //Vfx Sound and Pool
        }
        else
        {
            
            _currentHealth -= damageValue;
            _creature.PlayerCharacter = _character.transform.gameObject;
            _enemyStateFactory.Damage.CurrentDamageType = currentDamage;
            _creature.SwitchState(_enemyStateFactory.Damage);
           
           
        }
     
    }

   
}
