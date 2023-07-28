using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLadderControl : MonoBehaviour
{
    [HideInInspector] public bool IsLadder;

    void Update()
    {
        if (IsLadder)
            LadderControl();
    }

    private void LadderControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * 6 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.up * -6 * Time.deltaTime);
        }

    }


}
