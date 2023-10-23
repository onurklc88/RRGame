using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProperties", menuName = "ScriptableObjects/Enemy", order = 1)]

public class CreatureProperties : ScriptableObject
{
    [Header("Speed Settings")]


    public float WalkSpeed;
    [Header("Agression Settings")]


    public float AgressionRange;
    public float AttackTime;
    public float AttackDistance;
    [Header("Damage Settings")]

    public DamageType.Damage AttackType; 
    public float LightAttackDamage;
    public int HeavyAttackDamage;
    [Header("Health Settings")]

    public int EnemyHealth;

}
