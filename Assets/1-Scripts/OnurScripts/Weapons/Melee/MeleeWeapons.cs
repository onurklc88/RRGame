using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/MeleeWeapon", order = 1)]
public class MeleeWeapons : ScriptableObject
{
    public Image SwordImage;
    public DamageType.Damage DamageType;
    public float Damage;
    public float Range;
    public float SwingTime;
    public int SwingCount;
}
