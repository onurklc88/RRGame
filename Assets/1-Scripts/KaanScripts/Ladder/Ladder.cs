using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static ToonyColorsPro.ShaderGenerator.Enums;
using System;

public class Ladder : MonoBehaviour
{
    public Transform EnterPoint;

    [Space]
    //Ladder Exit Points
    public Transform ExitPointDown;
    public Transform ExitPointUp;

    [Space]
    //Ladder Down Up Limit
    public Transform DownExitLimit;
    public Transform UpExitLimit;

    [Space]
    public Vector3 Rot;

    
}
