using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProperties", menuName = "ScriptableObjects/Enemy", order = 1)]

public class EnemyProperties : ScriptableObject
{
    [Header("Speed Settings")]


    public float WalkSpeed;
    public float SprintSpeed;
    [Header("Agression Settings")]


    public float AttackArea;
    public float ChaseArea;
    [Header("Damage Settings")]


    public int LightAttackDamage;
    public int HeavyAttackDamage;
    [Header("Health Settings")]

    public int EnemyHealth;

}
