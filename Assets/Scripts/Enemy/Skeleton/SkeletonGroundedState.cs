using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform player;
    public SkeletonGroundedState(Enemy enmeyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy)
        :
        base(enmeyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = Player.instance.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // �����ң����߰���������ң� ����Ҳ���Ա�֤��ⱳ������ ��
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.battleBackCheckDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }
    }
}
