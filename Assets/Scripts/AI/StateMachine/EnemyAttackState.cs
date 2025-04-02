using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
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
        if (manager.DistanceToTarget() >= manager.attackDistance)
        {
            manager.SwitchState(manager.AgroState);
            return;
        }
    }
}
