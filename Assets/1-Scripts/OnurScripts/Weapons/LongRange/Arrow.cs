using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Arrow : ThrowableWeapon
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    

   
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<IDamageable>() != null)
        {
            EventLibrary.OnWeaponChargeUpdated.Invoke(true);
            _collidedObject = other.transform.GetComponent<IDamageable>();
        }
           

        WeaponAction();
    }

    protected override void WeaponAction()
    {
        
        if(_collidedObject != null)
        {
            _collidedObject.TakeDamage(_damage);
            _collidedObject = null;
        }
            
       _impulseSource.GenerateImpulse(1f);
        gameObject.SetActive(false);
      
    }
}
