using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveHandler : MonoBehaviour
{
    private const float MaxDissolveAmount = 1;
    private const float DissolveSpeedMultiplier = 0.05f;
    [SerializeField] private Material _dissolveMaterial;
    private List<Material> _dissolveMaterials;
    [SerializeField] [ColorUsage(true, true)] private Color _dissolveColor;
    [SerializeField] [Range(0.1f, 1)] private float _dissolveSpeed;
    [SerializeField] [Range(0.01f, 0.1f)] private float _dissolveRefreshRate;

    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshes;
    [SerializeField] private bool _debugAcceptInput;

    private float _dissolveAmount = 0;

    private void Start()
    {
        _dissolveMaterials = new();
        _skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            var mesh = _skinnedMeshes[i];
            _dissolveMaterials.Add(Instantiate(_dissolveMaterial));

            var mat = _dissolveMaterials[i];
            mat.SetTexture("_MainTexture", mesh.material.GetTexture("_BaseMap"));
            mat.SetColor("_DissolveColor", _dissolveColor);
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

    public void ReverseDissolve()
    {
        StartCoroutine(ReverseDissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            // set color for debug
            var mat = _dissolveMaterials[i];
            mat.SetColor("_DissolveColor", _dissolveColor);

            _skinnedMeshes[i].material = _dissolveMaterials[i];
        }

        while (_dissolveAmount < MaxDissolveAmount)
        {
            float dissolveSpeed = _dissolveSpeed * DissolveSpeedMultiplier;
            _dissolveAmount += dissolveSpeed;
            for (int i = 0; i < _skinnedMeshes.Length; i++)
            {
                _skinnedMeshes[i].material.SetFloat("_DissolveAmount", _dissolveAmount);
            }

            yield return new WaitForSeconds(_dissolveRefreshRate);
        }
    }

    private IEnumerator ReverseDissolveRoutine()
    {
        for (int i = 0; i < _skinnedMeshes.Length; i++)
        {
            // set color for debug
            var mat = _dissolveMaterials[i];
            mat.SetColor("_DissolveColor", _dissolveColor);
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
}
