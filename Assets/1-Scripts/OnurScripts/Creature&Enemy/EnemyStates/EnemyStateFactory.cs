

public class EnemyStateFactory
{
    IState _context;
   

    public CreatureIdleState Idle()
    {
        return new CreatureIdleState();
    }

    public CreatureWalkState Walk()
    {
        return new CreatureWalkState();
    }

    public CreatureChaseState Chase()
    {
        return new CreatureChaseState();
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
   

