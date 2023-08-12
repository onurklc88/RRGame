using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Arrow : ThrowableWeapon
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    

    private void OnEnable()
    {
      
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<IDamageable>() != null)
            _collidedObject = other.transform.GetComponent<IDamageable>();

        WeaponAction();
    }

    protected override void WeaponAction()
    {
        
        if(_collidedObject != null)
        {
             Debug.Log("Detected");
            _collidedObject.TakeDamage(_damage);
        }
        else
        {
            Debug.Log("Null");
        }
           

        _impulseSource.GenerateImpulse(1f);
         gameObject.SetActive(false);
    }
}
