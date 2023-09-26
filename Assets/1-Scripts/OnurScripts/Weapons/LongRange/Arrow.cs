using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Arrow : ThrowableWeapon
{
    private const int MaxDecals = 20;
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    [SerializeField][Range(1, 10)] private int _decalGenerationLatency;

    private Vector3 _prevPos;
    private int _frameCounter = 0;
    private int _decalCounter = 0;

    private void OnEnable()
    {
        _prevPos = transform.position;
        _frameCounter = 0;
        _decalCounter = 0;
        //StartCoroutine(ParticleDropRoutine(0.15f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private void FixedUpdate()
    {
        _frameCounter++;
        if (_decalCounter < MaxDecals)
        {
            if (_frameCounter == _decalGenerationLatency)
            {
                _frameCounter = 0;
                _decalCounter++;
                var currentPos = transform.position;
                var negativeMoveDirection = (_prevPos - currentPos).normalized;
                var particle = ObjectPool.GetPooledObject(2);
                currentPos.y = -6.787f;
                //particle.transform.position = currentPos + negativeMoveDirection + new Vector3(0, -0.5f, 0);
                particle.transform.position = currentPos + negativeMoveDirection;
                particle.SetActive(true);
            }
        }
        
        _prevPos = transform.position;
    }

    //private IEnumerator ParticleDropRoutine(float secs)
    //{

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(secs);
    //        var currentPos = transform.position;
    //        var negativeMoveDirection = (_prevPos - currentPos).normalized;
    //        var particle = ObjectPool.GetPooledObject(3);
    //        currentPos.y = 0.2f;
    //        //particle.transform.position = currentPos + negativeMoveDirection + new Vector3(0, -0.5f, 0);
    //        particle.transform.position = currentPos + negativeMoveDirection;
    //        particle.SetActive(true);
    //        _prevPos = transform.position;
    //    }
    //}

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
            _collidedObject.TakeDamage(_damage, DamageType.Damage.Arrow);
            _collidedObject = null;
        }
            
       _impulseSource.GenerateImpulse(1f);
        gameObject.SetActive(false);
      
    }
}
