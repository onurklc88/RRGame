using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLook : MonoBehaviour
{
    public Player _player;
    public UpgradeSave _upgradeSave;

    private void Start()
    {
        _player = SaveInfo._player;
        _upgradeSave = SaveInfo._upgradeSave;
    }
}
