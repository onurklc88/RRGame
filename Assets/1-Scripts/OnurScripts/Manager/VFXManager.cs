using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFXManager : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] _dashVfx;
    private void OnEnable()
    {
        EventLibrary.OnCharacterDash.AddListener(ShowDashTrail);
    }

    private void OnDisable()
    {
        EventLibrary.OnCharacterDash.RemoveListener(ShowDashTrail);
    }


    private void ShowDashTrail()
    {
        StartCoroutine(DefaultDashVFXRoutine(0.75f));
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
