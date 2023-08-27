

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

    public CreatureAttackState Attack()
    {
        return new CreatureAttackState();
    }
}
   

