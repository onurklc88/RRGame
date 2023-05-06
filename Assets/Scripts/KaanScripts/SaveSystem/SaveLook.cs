using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLook : MonoBehaviour
{
    public Player _player;

    private void Start()
    {
        _player = SaveInfo._player;

    }
}
