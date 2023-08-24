using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLook : MonoBehaviour
{
    public Player Player;
    public UpgradeSave UpgradeSave;

    private void Start()
    {
        Player = SaveInfo.Player;
        UpgradeSave = SaveInfo.UpgradeSave;
    }
}
