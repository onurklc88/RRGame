using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Bomb : ThrowableWeapon
{
   [SerializeField] private CinemachineImpulseSource _impulseSource;
   private IDamageable _damageableObject;
  
    protected override void OnTriggerEnter(Collider other)
    {
        CheckExplosionArea();
        WeaponAction();
       
    }

    protected override void WeaponAction()
    {
        _impulseSource.GenerateImpulseWithForce(0.5f);
        gameObject.SetActive(false);
    }

    private void CheckExplosionArea()
    {
        Collider[] inSightRange = Physics.OverlapSphere(transform.position, 7f);

        if (inSightRange.Length <= 0) return;
       

        for (int i = 0; i < inSightRange.Length; i++)
        {
            if (inSightRange[i].transform.GetComponent<IDamageable>() != null)
            {
                _damageableObject = inSightRange[i].transform.GetComponent<IDamageable>();
                _damageableObject.TakeDamage(_damage);
            }
        }

        if(_damageableObject != null)
            EventLibrary.OnWeaponChargeUpdated.Invoke(true);

        _damageableObject = null;

    }
}
