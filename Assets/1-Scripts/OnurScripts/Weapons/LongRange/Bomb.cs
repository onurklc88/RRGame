using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Bomb : ThrowableWeapon
{
   [SerializeField] private CinemachineImpulseSource _impulseSource;
   
    protected override void OnTriggerEnter(Collider other)
    {
        CheckExplosionArea();
        WeaponAction();
        if (other.gameObject.layer == 3)
        {
            DropDecalOnGround();
        }
    }


    protected override void WeaponAction()
    {
        _impulseSource.GenerateImpulseWithForce(0.2f);
        gameObject.SetActive(false);
    }

    private void CheckExplosionArea()
    {
        Collider[] inSightRange = Physics.OverlapSphere(transform.position, 7f);

        if (inSightRange.Length <= 0) return;
       

        for (int i = 0; i < inSightRange.Length; i++)
        {
            if (inSightRange[i].transform.GetComponent<IDamageable>() != null)
                 inSightRange[i].transform.GetComponent<IDamageable>().TakeDamage(_damage); 
        }
       

    }

    private void DropDecalOnGround()
    {
        float minRadius = 1.0f;
        float maxRadius = 5.0f;

        int minNumberOfDecals = 2;
        int maxNumberOfDecals = 6;
        int numOfDecals = Random.Range(minNumberOfDecals, maxNumberOfDecals);


        for (int i = 0; i < numOfDecals; i++)
        {
            var decal = ObjectPool.GetPooledObject(2);
            float spreadRadius = Random.Range(minRadius, maxRadius);
            var posOffset = spreadRadius * Random.insideUnitCircle;

            decal.SetActive(true);

            var newPos = transform.position + new Vector3(posOffset.x, 0, posOffset.y);
            newPos.y = 0;
            decal.transform.position = newPos;
            decal.transform.DOScale(Random.Range(3.0f, 5.0f), 0.5f);
        }
    }
}
