

public class CharacterStateFactory 
{
    public CharacterIdleState CharacterIdleState = new CharacterIdleState();
    public CharacterWalkState CharacterWalkState = new CharacterWalkState();
    public CharacterSlideState CharacterSlideState = new CharacterSlideState();
    public CharacterClimbState CharacterClimbState = new CharacterClimbState();
    public CharacterAttackState CharacterAttackState = new CharacterAttackState();
    public CharacterKnockbackState CharacterKnockbackState = new CharacterKnockbackState();
    //Attacks
    public LightAttack LightAttack = new LightAttack();
    public HeavyAttack HeavyAttack = new HeavyAttack();
    public LongRangeCombat LongRangeCombat = new LongRangeCombat();
}
