using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _flashColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color _secondaryFlashColor;

    private Material _flashMat;

    private void Start()
    {
        _flashMat = GetComponentInChildren<Renderer>().materials[^1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(FlashRoutine(0.1f));
        }   
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine(0.1f));
    }

    private IEnumerator FlashRoutine(float secs)
    {
        var color = new Color();
        color.a = 1;
        // make the material visible by setting color's alpha to 1
        _flashMat.SetColor("_BaseColor", color);
        _flashMat.SetColor("_EmissionColor", _flashColor);

        yield return new WaitForSeconds(secs);
        _flashMat.SetColor("_EmissionColor", _secondaryFlashColor);
        yield return new WaitForSeconds(secs);
        _flashMat.SetColor("_EmissionColor", _flashColor);
        yield return new WaitForSeconds(secs * 0.5f);

        // make the material invisible by setting color's alpha to 0
        color.a = 0;
        _flashMat.SetColor("_BaseColor", color);
    }

    private void OnDestroy()
    {
        Destroy(_flashMat);
    }
}
