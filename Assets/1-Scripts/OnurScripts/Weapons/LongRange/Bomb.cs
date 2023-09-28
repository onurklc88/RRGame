using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Bomb : ThrowableWeapon
{
   [SerializeField] private CinemachineImpulseSource _impulseSource;
   [SerializeField] private Rigidbody _rigidbody;
  
  

    protected override void OnCollisionEnter(Collision collision)
    {
       
        CheckExplosionArea();

        if (collision.gameObject.layer == 3)
            DropDecalOnGround(collision.contacts[0].point);

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

    private void DropDecalOnGround(Vector3 contactPoint)
    {
        float minRadius = 1.0f;
        float maxRadius = 8.0f;

        int minNumberOfDecals = 6;
        int maxNumberOfDecals = 15;
        int numOfDecalsToSpawn = Random.Range(minNumberOfDecals, maxNumberOfDecals);


        for (int i = 0; i < numOfDecalsToSpawn; i++)
        {
            var decal = DecalPool.GetPooledObject(1);
            float spreadRadius = Random.Range(minRadius, maxRadius);
            var posOffset = spreadRadius * Random.insideUnitCircle;

            decal.SetActive(true);

            var newPos = transform.position + new Vector3(posOffset.x, 0, posOffset.y);
            //newPos.y = -6.925f;
            newPos.y = contactPoint.y + 0.001f;
            decal.transform.position = newPos;
            decal.transform.DOScale(Random.Range(1.0f, 3.0f), 0.5f);
        }
    }
}
