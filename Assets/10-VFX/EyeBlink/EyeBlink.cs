using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeBlink : MonoBehaviour
{
    [SerializeField] private GameObject _postFxPlane;
    [SerializeField] private DepthOfField _dof;
    [SerializeField] private Material _eyeBlinkMat;
    [SerializeField] private bool _isAnimating = false;

    private void OnEnable()
    {
        EventLibrary.OnPlayerDead.AddListener(Blink);
    }
    private void OnDisable()
    {
        EventLibrary.OnPlayerDead.RemoveListener(Blink);
    }


    public void Init(GameObject postFxPlane, DepthOfField dof)
    {
        _postFxPlane = postFxPlane;
        _dof = dof;
        _eyeBlinkMat = _postFxPlane.GetComponent<MeshRenderer>().material;
    }

    public void Blink()
    {
        if (_isAnimating)
        {
            return;
        }

        StartCoroutine(BlinkAnimation(Mathf.Clamp(3, 1, 4)));
    }

    private IEnumerator BlinkAnimation(float speed)
    {
        _postFxPlane.SetActive(true);
        _dof.active = true;
        float dofStartValue = 0;


        float alphaMultiplier = 0;
        while (alphaMultiplier < 1.0f)
        {
            yield return new WaitForSeconds(0.01f);
            alphaMultiplier += 0.01f;
            _eyeBlinkMat.SetFloat("_AlphaMultiplier", alphaMultiplier);
            dofStartValue = alphaMultiplier.Remap(0, 1, 20, 65);
            _dof.focalLength.value = dofStartValue;
            
        }

        float i = (1.5f * Mathf.PI) / speed;
        while (true)
        {
            Debug.Log($"i -> {i}");
            float blinkVal = Mathf.Sin(i * speed);
            i += 0.01f;

            Debug.Log($"blinkVal->{blinkVal}");
            blinkVal = blinkVal.Remap(-1, 1, 0.2f, 1.0f);
            Debug.Log($"blinkVal_remapped->{blinkVal}");

            _dof.focalLength.value = blinkVal.Remap(0, 1, dofStartValue, 110);


            _eyeBlinkMat.SetFloat("_NonVisibleArea", blinkVal);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnDestroy()
    {
        Destroy(_eyeBlinkMat);
    }
}
