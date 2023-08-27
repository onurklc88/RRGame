using UnityEngine;

public class CharacterStateFactory 
{

    public CharacterIdleState CharacterIdleState = new CharacterIdleState();
    public CharacterWalkState CharacterWalkState = new CharacterWalkState();
    public CharacterSlideState CharacterSlideState = new CharacterSlideState();
    public CharacterClimbState CharacterClimbState = new CharacterClimbState();
    public CharacterAttackState CharacterAttackState = new CharacterAttackState();
    public CharacterKnockbackState CharacterKnockbackState = new CharacterKnockbackState();
    //AttackType
    public enum CombatType
    {
        Melee,
        LongRange,
        None
    }
    public CombatType CurrentCombatType = CombatType.None;
    public LightAttack LightAttack = new LightAttack();
    public HeavyAttack HeavyAttack = new HeavyAttack();
    public ThrowArrow ThrowArrow = new ThrowArrow();
    public ThrowBomb ThrowBomb = new ThrowBomb();

    //variables
    public Vector3 KnockBackPosition;

    //methods
    public void GetKnockBackPosition(Vector3 tempKnockBackPos)
    {
        KnockBackPosition = tempKnockBackPos;
        Debug.Log("KnockBackPosIsReal: " + KnockBackPosition);
    }
    
}
