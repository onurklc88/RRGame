using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.VFX;

public class Bomb : ThrowableWeapon
{
   [SerializeField] private CinemachineImpulseSource _impulseSource;
   [SerializeField] private Rigidbody _rigidbody;

    private VisualEffect _vfx;

    protected override void OnCollisionEnter(Collision collision)
    {
       
        CheckExplosionArea();

        if (collision.gameObject.layer == 3)
        {
            ExplosionEffect();
        }

        WeaponAction();
    }



    protected override void WeaponAction()
    {
        _impulseSource.GenerateImpulseWithForce(0.2f);
        _rigidbody.isKinematic = true;
        EventLibrary.ResetPooledObject.Invoke(gameObject);
    }

    private void CheckExplosionArea()
    {
        Collider[] inSightRange = Physics.OverlapSphere(transform.position, 7f);

        if (inSightRange.Length <= 0) return;
       

        for (int i = 0; i < inSightRange.Length; i++)
        {
            if (inSightRange[i].transform.GetComponent<IDamageable>() != null)
                 inSightRange[i].transform.GetComponent<IDamageable>().TakeDamage(_damage, DamageType.Damage.Bomb); 
        }
       

    }

    private void ExplosionEffect()
    {
        var vfxGo = ObjectPool.GetPooledObject(12);
        vfxGo.SetActive(true);
        var vfx = vfxGo.GetComponent<VisualEffect>();
        vfx.gameObject.SetActive(true);
        vfx.transform.position = transform.position + new Vector3(0, 4, 0);
        vfx.SendEvent("ManualPlay");
    }

}
