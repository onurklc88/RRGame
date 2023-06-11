

public class StateFactory
{
    IState _context;


    public IdleState Idle()
    {
        return new IdleState();
    }

    public WalkState Walk()
    {
        return new WalkState();
    }

    public ChaseState Chase()
    {
        return new ChaseState();
    }

    public AttackState Attack()
    {
        return new AttackState();
    }
}
   

