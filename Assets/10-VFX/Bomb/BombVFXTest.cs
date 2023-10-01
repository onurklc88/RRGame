using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BombVFXTest : MonoBehaviour
{
    private VisualEffect _vfx;
    private new MeshRenderer renderer;

    private void OnEnable()
    {
        if (renderer == null)
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }
        renderer.enabled = true;
        StartCoroutine(DisableRenderer());
    }

    private IEnumerator DisableRenderer()
    {
        yield return new WaitForSeconds(0.2f);
        renderer.enabled = false;
    }

    void Start()
    {
        _vfx = GetComponent<VisualEffect>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _vfx.SendEvent("ManualPlay");
        }
    }
}
