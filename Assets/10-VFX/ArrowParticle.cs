using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParticle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            DropDecalOnGround();
            gameObject.SetActive(false);
        }
    }
    private void DropDecalOnGround()
    {
        float minRadius = 1.0f;
        float maxRadius = 3.0f;
        int randDecal = Random.Range(7, 11);
        var decal = ObjectPool.GetPooledObject(randDecal);
        float spreadRadius = Random.Range(minRadius, maxRadius);
        var posOffset = spreadRadius * Random.insideUnitCircle;

        decal.SetActive(true);

        var newPos = transform.position + new Vector3(posOffset.x, 0, posOffset.y);
        newPos.y = -6.925f;
        decal.transform.position = newPos;
        decal.transform.DOScale(Random.Range(1.0f, 2.0f), 0.25f);
    }
}
