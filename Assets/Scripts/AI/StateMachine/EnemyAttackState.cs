using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.SetSpeed(0);
        manager.animator.SetBool("isAttack", true);
        manager.agent.acceleration = 1000;
        if (manager.controller != null)
        {
            manager.controller.SetHoldAttack();
        }
    }
    public override void ExitState(EnemyStateManager manager)
    {
        manager.agent.acceleration = 8;
    }
    public override void UpdateState(EnemyStateManager manager)
    {
        bool canSeePlayer = manager.CanSeePlayer();
        float distanceToPlayer = manager.DistanceToPlayer();

        // ���� ����� ��� ������� ����� � ������ � Agro ��� Search
        if (distanceToPlayer > manager.attackDistance && canSeePlayer)
        {
            manager.SwitchState(manager.AgroState);
            return;
        }

        if (distanceToPlayer > manager.attackDistance && !canSeePlayer && manager.lastKnownPosition.HasValue)
        {
            manager.SwitchState(manager.SearchState);
            return;
        }

        if (distanceToPlayer > manager.attackDistance && !canSeePlayer && !manager.isAgroFromInfection && !manager.isTakeDamage)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }

        // �����: ���� ����� �� ����� ���� �� ������� ���������� � ������ � SearchState
        if (!canSeePlayer)
        {
            manager.SwitchState(manager.SearchState);
            return;
        }

        // ��������� � ������
        Vector3 direction = (manager.player.position - manager.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, lookRotation, Time.deltaTime * 30f);
        }
    }

}
