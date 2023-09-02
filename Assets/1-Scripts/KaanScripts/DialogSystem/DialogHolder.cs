using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog",menuName ="DialogData")]
public class DialogHolder : ScriptableObject
{
    [TextArea(4,10)]
    public string[] DialogTexts;
}
