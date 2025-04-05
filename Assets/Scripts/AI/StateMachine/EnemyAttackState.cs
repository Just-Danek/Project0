using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.animator.SetBool("isAttack", true);
        manager.SetSpeed(0);
        Debug.Log("������ � Attack");
    }
    public override void ExitState(EnemyStateManager manager)
    {
        Debug.Log("������� �� Attack");
    }
    public override void UpdateState(EnemyStateManager manager)
    {
        Debug.Log("�����");
    }
}