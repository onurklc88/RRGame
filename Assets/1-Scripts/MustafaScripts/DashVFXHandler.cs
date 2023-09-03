using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class DashVFXHandler : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] _dashVfx;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DefaultDashVFXRoutine(0.75f));
        }
    }

    private IEnumerator DefaultDashVFXRoutine(float secs)
    {
        foreach (var vfx in _dashVfx)
        {
            vfx.enabled = true;
        }

        yield return new WaitForSeconds(secs);

        foreach (var vfx in _dashVfx)
        {
            vfx.Clear();
            vfx.enabled = false;
        }

    }
}
