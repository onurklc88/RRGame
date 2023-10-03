using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTest : MonoBehaviour
{
    [SerializeField] private GameObject _grim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _grim.SetActive(false);
        }
    }

}
