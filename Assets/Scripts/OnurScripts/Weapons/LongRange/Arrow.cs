using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Arrow : ThrowableWeapon
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<IDamageable>() != null)
            _collidedObject = other.transform.GetComponent<IDamageable>();

        WeaponAction();
    }

   


    protected override void WeaponAction()
    {
        if(_collidedObject != null)
            _collidedObject.TakeDamage(_damage);

      
        EventLibrary.OnWeaponDestroy.Invoke(_impulseSource);
        
        gameObject.SetActive(false);
        

    }
}
