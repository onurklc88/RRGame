using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveHandler : MonoBehaviour
{
    private const float MaxDissolveAmount = 1;
    private const float DissolveSpeedMultiplier = 0.05f;
    private List<Material> _materials;
    [SerializeField] [ColorUsage(true, true)] private Color _damageColor;
    [SerializeField] [Range(0.1f, 1)] private float _dissolveSpeed;
    [SerializeField] [Range(0.01f, 0.1f)] private float _dissolveRefreshRate;

    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshes;
    [SerializeField] private bool _debugAcceptInput;

    private float _dissolveAmount = 0;
    private bool _pulsing;

    private void Start()
    {
        _materials = new();
        _skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            var mesh = _skinnedMeshes[i];
            _materials.Add(mesh.materials[0]);

            var mat = _materials[i];
            mat.SetColor("_Emission", _damageColor);
        }

    }

    private void Update()
    {
        if (_debugAcceptInput)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Dissolve();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                ReverseDissolve();
            }
        }
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    public void Pulse(float speed)
    {
        if (!_pulsing)
        {
            for (int i = 0; i < _skinnedMeshes.Length; i++)
            {
                // set color for debug
                var mat = _materials[i];
                mat.EnableKeyword("DAMAGE_PULSE");
                mat.SetFloat("_PulseSpeed", speed);
            }

            return;
        }

        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            // set color for debug
            var mat = _materials[i];
            mat.SetFloat("_PulseSpeed", speed);
        }
    }

    public void ReverseDissolve()
    {
        StartCoroutine(ReverseDissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            // set color for debug
            var mat = _materials[i];
            mat.EnableKeyword("TCP2_DISSOLVE");
            mat.SetColor("_Emission", _damageColor);
        }

        while (_dissolveAmount < MaxDissolveAmount)
        {
            float dissolveSpeed = _dissolveSpeed * DissolveSpeedMultiplier;
            _dissolveAmount += dissolveSpeed;
            for (int i = 0; i < _skinnedMeshes.Length; i++)
            {
                _materials[i].SetFloat("_DissolveAmount", _dissolveAmount);
            }

            yield return new WaitForSeconds(_dissolveRefreshRate);
        }
    }

    private IEnumerator ReverseDissolveRoutine()
    {
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            // set color for debug
            var mat = _materials[i];
            mat.SetColor("_DissolveColor", _damageColor);
        }

        while (_dissolveAmount > 0)
        {
            float dissolveSpeed = _dissolveSpeed * DissolveSpeedMultiplier;
            _dissolveAmount -= dissolveSpeed;
            for (int i = 0; i < _skinnedMeshes.Length; i++)
            {
                _skinnedMeshes[i].material.SetFloat("_DissolveAmount", _dissolveAmount);
            }

            yield return new WaitForSeconds(_dissolveRefreshRate);
        }
    }

    private void OnDestroy()
    {
        foreach (var mat in _materials)
        {
            Destroy(mat);
        }
    }
}
