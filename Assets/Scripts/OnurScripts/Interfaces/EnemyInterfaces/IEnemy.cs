
public interface IEnemy
{
    void IdleState();
    void PatrolState();
    void ChaseState();
    void AttackState();
    void DieState();
}
