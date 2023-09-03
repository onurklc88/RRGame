using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] _dashVfx;

    [SerializeField] private Volume _postFxVolume;

    private Vignette _vignette;
    private ChromaticAberration _chromaticAberration;

    private void Awake()
    {
        _postFxVolume.profile.TryGet(out _vignette);
        _postFxVolume.profile.TryGet(out _chromaticAberration);
    }


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
        float step = secs * 0.5f;

        _vignette.active = true;
        _chromaticAberration.active = true;
        
        foreach (var vfx in _dashVfx)
        {
            vfx.enabled = true;
        }

        yield return new WaitForSeconds(step);

        _vignette.active = false;
        _chromaticAberration.active = false;

        yield return new WaitForSeconds(step);

        foreach (var vfx in _dashVfx)
        {
            vfx.Clear();
            vfx.enabled = false;
        }

    }
}
