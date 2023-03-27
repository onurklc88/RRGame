using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProperties", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterProperties : ScriptableObject
{
    public float WalkSpeed;
    public float AttackArea;

}
