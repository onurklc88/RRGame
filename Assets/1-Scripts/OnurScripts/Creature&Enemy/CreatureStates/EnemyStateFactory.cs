

public class EnemyStateFactory
{
    

    public CreatureChaseState Chase = new CreatureChaseState();
    public CreatureDamageState Damage = new CreatureDamageState();
    public CreatureDeathState Death = new CreatureDeathState();
    public CreatureIdleState Idle()
    {
        return new CreatureIdleState();
    }

    public CreatureWalkState Walk()
    {
        return new CreatureWalkState();
    }
   
    public CreatureDashAttackState DashAttack()
    {
        return new CreatureDashAttackState();
    }

    public CreatureMeleeAttack MeleeAttack()
    {
        return new CreatureMeleeAttack();
    }
}
   

