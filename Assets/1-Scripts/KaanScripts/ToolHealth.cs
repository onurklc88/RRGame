using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health += value;
        }
    }
    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        //GetComponent<DamageFlash>().Flash();

        if( _health <= 0)
        {
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            transform.GetChild(1).GetComponent<BreakObject>().BreakIt();

            Health = 1;
        }
        
    }

 
}
