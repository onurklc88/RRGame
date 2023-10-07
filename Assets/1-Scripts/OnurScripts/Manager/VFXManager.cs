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
    [SerializeField] private GameObject _postFxPlane;

    [SerializeField]private Vignette _vignette;
    [SerializeField]private ChromaticAberration _chromaticAberration;
    [SerializeField]private DepthOfField _dof;

    private void Awake()
    {
        _postFxVolume.profile.TryGet(out _vignette);
        _postFxVolume.profile.TryGet(out _chromaticAberration);
        _postFxVolume.profile.TryGet(out _dof);

        _postFxPlane.SetActive(true);
        GetComponent<EyeBlink>().Init(_postFxPlane, _dof);
        _postFxPlane.SetActive(false);
    }


    private void OnEnable()
    {
        EventLibrary.OnCharacterDash.AddListener(ShowDashTrail);
    }

    private void OnDisable()
    {
        EventLibrary.OnCharacterDash.RemoveListener(ShowDashTrail);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GetComponent<EyeBlink>().Blink(3);
        }
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
