using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class ThrowableWeapon : MonoBehaviour
{
    public int WeaponID;
    [SerializeField] protected  Image _weaponImage;
    [SerializeField] protected float _damage;
    
    protected IDamageable _collidedObject;

   
    protected void UnlockWeapon()
    {
        //UI
    }

    protected abstract void WeaponAction();
  

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }



}
