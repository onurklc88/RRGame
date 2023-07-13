using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Bomb : Weapon
{
   [SerializeField] private CinemachineImpulseSource _impulseSource;


    protected override void OnTriggerEnter(Collider other)
    {
        CheckExplosionArea();
        WeaponAction();
       
    }

    protected override void WeaponAction()
    {
       
        EventLibrary.OnWeaponDestroy.Invoke(_impulseSource);
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
                inSightRange[i].transform.GetComponent<IDamageable>().TakeDamage(_damage);
            }
        }
    }
}
